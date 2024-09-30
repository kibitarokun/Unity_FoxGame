using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    public float moveX = 0.0f; //xの移動値
    public float moveY = 0.0f; //yの移動値
    public float times = 0.0f; //移動時間
    public float wait = 0.0f; //停止時間
    public bool isMoveWhenOn = false;　//乗った時に動く仕組み
    public bool isCanMove = true;  //trueの時に動く
    Vector3 startPos;　//初期位置
    Vector3 endPos;　//終了位置
    bool isReverse = false;　//反転フラグ

    float movep = 0;　//移動補完値
    
    void Start()
    {
        startPos = transform.position;　//初期位置の設定
        endPos = new Vector2(startPos.x + moveX, startPos.y + moveY);　//移動後の位置の設定

        if (isMoveWhenOn)
        {
            isCanMove = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isCanMove)
        {
            //移動補完値の算出
            float distance = Vector2.Distance(startPos, endPos);　//移動距離
            float ds = distance / times;　//１秒あたりの移動距離
            float df = ds * Time.deltaTime;　//１フレームあたりの移動距離
            movep += df / distance;　//移動補完値

            if (isReverse)　//反転
            {
                transform.position = Vector2.Lerp(endPos,startPos, movep);　//始点,終点,移動補完値（0～1）
            }
            else
            {
                transform.position = Vector2.Lerp(startPos, endPos, movep);　//正位置
            }

            if(movep >= 1.0f)　//距離補完値が1に達したら
            {
                movep = 0.0f;　//リセット
                isReverse = !isReverse;　//反転フラグが立つ
                isCanMove = false;　//移動停止

                //if (isMoveWhenOn == false)
                //{
                    Invoke("Move", wait);　//停止時間後に、動き出す
                //}
            }
        }
    }

    public void Move()
    {
        isCanMove = true;
    }

    //public void Stop()
    //{
    //    isCanMove = false;
    //}

    //接触開始
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(transform);

            if (isMoveWhenOn)
            {
                isCanMove = true;
            }
        }
    }
    //接触    終了
    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null);
        }
    }

    //移動範囲表示
    void OnDrawGizmosSelected()
    {
        Vector2 fromPos;
        if (startPos == Vector3.zero)
        {
            fromPos = transform.position;
        }
        else
        {
            fromPos = startPos;
        }
        Gizmos.DrawLine(fromPos, new Vector2(fromPos.x + moveX, fromPos.y + moveY));
        Vector2 size = GetComponent<SpriteRenderer>().size;
        Gizmos.DrawWireCube(fromPos, new Vector2(size.x, size.y));
        Vector2 toPos = new Vector3(fromPos.x + moveX, fromPos.y + moveY);
        Gizmos.DrawWireCube(toPos, new Vector2(size.x, size.y));
    }
}
    