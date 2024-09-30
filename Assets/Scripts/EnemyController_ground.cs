using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_ground : MonoBehaviour
{
    public float speed = 3.0f;　//移動スピード
    public bool toRight;　//trueは右向き、falseは左向き
    public LayerMask groundLayer;　//地面判定

    // Start is called before the first frame update
    void Start()
    {
        if (toRight) //右向きがtrueだったら
        {
            transform.localScale = new Vector2(5,5); //右向き
        }
    }
   
    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        //地面に接しているか判定
        bool onGround = Physics2D.CircleCast(transform.position, 0.5f, 
                                             Vector2.down, 0.5f, groundLayer);

        if (onGround)　//地面に接していれば移動する
        {
            Rigidbody2D rbody = GetComponent<Rigidbody2D>();

            if (toRight)　
            {
                rbody.velocity = new Vector2(speed, rbody.velocity.y);　//右移動
            }
            else　
            {
                rbody.velocity = new Vector2(-speed, rbody.velocity.y);　//左移動
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //他のコライダーに接触した時には反転する
        toRight = !toRight;

        if (toRight)
        {
            transform.localScale = new Vector2(5,5);　//右向き
        }
        else
        {
            transform.localScale = new Vector2(-5, 5);　//左向き
        }
    }
}
