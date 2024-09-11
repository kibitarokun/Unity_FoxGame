using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    //プレイヤーが近づくと追いかける仕組みにする
    bool isActive = false;　//trueになるとプレイヤーを追いかける
    public float speed = 0.5f; //プレイやーを追いかけるスピード
    public float reactionDistance = 4.0f;　//プレイヤーを感知する範囲
    float axisH;　
    float axisV;　
    Rigidbody2D rbody;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        axisH = 0;
        axisV = 0;

        GameObject player = GameObject.FindGameObjectWithTag("Player");　//プレイヤーを見つける
        if (player != null)　//プレイヤーが存在する
        {
            //プレイヤーとの距離を求めてreactionDistance内であれば反応する
            float dist = Vector2.Distance(transform.position, player.transform.position);
            if (dist < reactionDistance)
            {
                isActive = true;
            }
            else
            {
                isActive= false;
            }

            //追跡開始
            if (isActive)
            {
                //Atan2メソッドでプレイヤーに向かう角度を求める　
                float dx = player.transform.position.x - transform.position.x;
                float dy = player.transform.position.y - transform.position.y;
                float rad = Mathf.Atan2(dy, dx);

                //speedの値分加速して接近する
                axisH = Mathf.Cos(rad) * speed;
                axisV = Mathf.Sin(rad) * speed;

                if (axisH > 0.0f)　//横軸がプラスの値なら
                {
                    transform.localScale = new Vector2(-3, 3); //右向き
                }
                else if (axisH < 0.0f)　//横軸がマイナスの値なら
                {
                    transform.localScale = new Vector2(3, 3); //左向き
                }
            }
            else
            {
                isActive = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (isActive)
        {
            //移動
            rbody.velocity = new Vector2(axisH, axisV).normalized;
        }
        else
        {
            rbody.velocity = Vector2.zero;
        }
    }   
}
