using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VirtualPad : MonoBehaviour
{
    public float MaxLength = 70; //タブが動く最大距離
    public bool is4DPad = false; //上下左右に動かすフラグ
    GameObject player; //操作するプレイヤーのGameObject
    Vector2 defPos; //タブの初期座標
    Vector2 downPos; //タッチ位置

    //★追加:このパッドを操作している指(タッチ)のID。マウス操作時は-1のまま
    int activeFingerId = -1;

    //★追加:Render Modeが「Screen Space - Camera」の場合、当たり判定の計算に
    //Canvasの Render Camera が必要になるためキャッシュしておく
    Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        //プレイヤーを取得
        player = GameObject.FindGameObjectWithTag("Player");
        //タブの初期座標
        defPos = GetComponent<RectTransform>().localPosition;
        //★追加:親をたどってこのUIが乗っているCanvasを取得
        canvas = GetComponentInParent<Canvas>();
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    //ダウンイベント
    public void PadDown()
    {
        //★追加:このパッドの上で押された指のfingerIdを特定して記録する
        //(マルチタッチ時にInput.mousePositionが他の指の座標を返してしまう対策)
        activeFingerId = GetTouchIdOnThisPad();

        //マウスポイントのスクリーン座標
        downPos = GetPointerPosition();
    }
    //ドラッグイベント
    public void PadDrag()
    {
        //マウスポイントのスクリーン座標
        //★変更:Input.mousePositionではなく、PadDownで特定した指の座標を使う
        Vector2 mousePosition = GetPointerPosition();
        //新しいタブの位置を求める
        Vector2 newTabPos = mousePosition - downPos;//マウスダウン位置からの移動差分
        if(is4DPad == false)
        {
            newTabPos.y = 0; //横スクロールの場合はY軸を0にする
        }
        //移動ベクトルを計算する
        Vector2 axis = newTabPos.normalized; //ベクトルを正規化する
        //2点の距離を求める
        //★修正:defPosとnewTabPos(移動差分)は座標系が違うので比較できていなかった。
        //newTabPos自体が「ダウン位置からの移動量」なので、その大きさをそのまま使う
        float len = newTabPos.magnitude;
        if(len > MaxLength )
        {
            //限界距離を超えたので限界座標を設定する
            newTabPos.x = axis.x * MaxLength;
            newTabPos.y = axis.y * MaxLength;
        }
        //タブを移動させる
        //★修正:初期位置(defPos)を基準に移動差分(newTabPos)を加算する
        GetComponent<RectTransform>().localPosition = defPos + newTabPos;
        //プレイヤーを移動させる
        PlayerController plcnt = player.GetComponent<PlayerController>();
        plcnt.SetAxis(axis.x, axis.y);
        //Padが下に行ったらキャラクターは伏せをする
        if (axis.y == -1) //Input.GetAxisRawの値が-1.0(下)になったら
        {
            plcnt.SetDown();
        }
    }
    //アップイベント
    public void PadUp()
    {
        //★追加:指を離したので担当していたfingerIdをリセットする
        activeFingerId = -1;

        //タブの位置の初期化
        GetComponent<RectTransform>().localPosition = defPos;
        //プレイヤーを停止させる
        PlayerController plcnt = player.GetComponent< PlayerController>();
        plcnt.SetAxis(0, 0);
    }

    //★追加:このパッドの矩形内で今まさに押し始めた(Began)タッチのfingerIdを探す
    //見つからなければ-1を返す(=マウス操作とみなす)
    int GetTouchIdOnThisPad()
    {
        RectTransform rt = GetComponent<RectTransform>();

        //★修正:Render ModeがOverlay以外(Camera/World Space)の場合は
        //判定にCanvasのRender Cameraを渡さないと正しく判定できない
        Camera cam = null;
        if (canvas != null && canvas.renderMode != RenderMode.ScreenSpaceOverlay)
        {
            cam = canvas.worldCamera;
        }

        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch t = Input.GetTouch(i);
            if (t.phase == TouchPhase.Began &&
                RectTransformUtility.RectangleContainsScreenPoint(rt, t.position, cam))
            {
                return t.fingerId;
            }
        }
        return -1;
    }

    //★追加:activeFingerIdで指定した指の現在位置を返す。
    //タッチが見つからない場合(離された/エディタ実行など)はInput.mousePositionにフォールバックする
    Vector2 GetPointerPosition()
    {
        if (activeFingerId >= 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch t = Input.GetTouch(i);
                if (t.fingerId == activeFingerId)
                {
                    return t.position;
                }
            }
        }
        return Input.mousePosition;
    }
}
