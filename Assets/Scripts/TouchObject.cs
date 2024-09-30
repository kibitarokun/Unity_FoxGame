using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchObject : MonoBehaviour
{
    //ブロック持ち上げて置く
    public GameObject gameObj;　//プレイヤー
    public GameObject target;　//プレイヤーの前方にある目印（ブロックを持ち運ぶ位置）
    public bool isRelease;　//ブロックを放すフラグ

    // Start is called before the first frame update
    void Start()
    {
        isRelease = true;
    }

    // Update is called once per frame
    void Update()
    {
        //スペースキーでブロックを放す
        if (Input.GetKeyDown(KeyCode.Space))　
        {
            if (!isRelease)
            {
                transform.parent = null;
                isRelease = true;
            }
        }
    }

    //プレイヤーが左右からブロックに接すると持ち運べるようにする
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")　//プレイヤーと接すると
        {
            //targetの場所がブロックのポジションになる
            this.transform.position = new Vector2(target.transform.position.x, target.transform.position.y+0.2f);
            transform.SetParent(gameObj.transform);　//プレイヤーの子になる
            isRelease = false;　//放すフラグをfalseにする
        }
    }
}
