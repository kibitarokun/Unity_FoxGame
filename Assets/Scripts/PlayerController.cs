using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody;
    float axisH = 0.0f;
    float axisV = 0.0f;
    public float speed = 3.0f;

    public float jump = 9.0f;
    public LayerMask groundLayer;
    bool goJump = false;

    public float distancce;
    public LayerMask ladderLayer;
    bool goClimb = false;

    bool down; //キャラクターを屈ませるbool型の変数

    Animator animator;
    public string idleAnime = "PlayerIdle";
    public string runAnime = "PlayerRun";
    public string hurtAnime = "PlayerHurt";
    public string crouchAnime = "PlayerCrouch";
    public string climbAnime = "PlayerClimb";
    public string jumpAnime = "PlayerJump";
    string nowAnime = "";
    string oldAnime = "";

    //スコア追加
    public int score = 0;

    //ゲームの状態
    public static string gameState = "playing";

    //ゲットした後のアイテムの再出現
    [SerializeField] GameObject Item;　//Prefabアイテムを指定
    //SerializeFieldを付けることでInspectorからアクセス可
    //(さらにprivateを付ければ他クラスからの書き換え不可となる ※セキュリティ対策)
    float time; //アイテム出現までの時間
    bool isSpawn;
    //ランダムな場所の範囲
    [SerializeField] private Transform rangeA;
    [SerializeField] private Transform rangeB;


    // Start is called before the first frame update
    void Start()
    {
        rbody = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        nowAnime = idleAnime; 
        oldAnime = idleAnime;

        gameState = "playing";

        isSpawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameState != "playing")
        {
            return;
        }

        axisH = Input.GetAxisRaw("Horizontal");
        axisV = Input.GetAxisRaw("Vertical");
        down = Input.GetKey(KeyCode.DownArrow);　//下矢印キーを押し続けているかを検知し、true/falseで返す

        if (axisH > 0.0f)
        {
            Debug.Log("右移動");
            transform.localScale = new Vector2(7, 7);
        }
        else if (axisH < 0.0f)
        {
            Debug.Log("左移動");
            transform.localScale = new Vector2(-7, 7);
        }

        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("ジャンプ");
            Jump();
        }

        //時間の経過
        time -= Time.deltaTime;

        //アイテムはrangeAとrangeBの範囲内でランダムな場所に出現する
        float x = Random.Range(rangeA.position.x, rangeB.position.x);
        float y = Random.Range(rangeA.position.y, rangeB.position.y);
        float z = Random.Range(rangeA.position.z, rangeB.position.z);
        Vector3 pos = new Vector3(x, y, z);
      
        //1秒経過かつisSpawnがtrueなら
        if(time <= 0 && isSpawn)
        {
            //指定したPrefabアイテムの自動生成
            Instantiate(Item, pos, Quaternion.identity);
            isSpawn = false;
        }
    }

    void FixedUpdate()
    {
        if (gameState != "playing")
        {
            return;
        }

        bool onGround = Physics2D.CircleCast(transform.position, 0.2f, Vector2.down, 0.0f, groundLayer);
        if (onGround || axisH != 0)
        {
            rbody.velocity = new Vector2(axisH * speed, rbody.velocity.y);
        }
        if (onGround && goJump)
        {
            Vector2 jumpPw = new Vector2(0, jump);
            rbody.AddForce(jumpPw, ForceMode2D.Impulse);
            goJump = false;
        }

        //はしごを登る動作
        //はしごを検知するためのRaycast（どこから、どの方向に、どれくらいの距離で、対象レイヤー）
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, 2.0f, ladderLayer);

        if (hitInfo.collider != null)　//①もしはしごがあったら
        {
            if (axisV > 0)　//②プレイヤーが上矢印キーを押したら
            {
                goClimb = true;
            }
        }
        else
        {
            goClimb = false;
        }

        if (goClimb)　//goClimbがtrueなら登る
        {
            Debug.Log("登る");
            rbody.velocity = new Vector2(rbody.velocity.x, axisV * speed);
            rbody.gravityScale = 0;　//はしごの途中で動きを止める
        }
        else
        {
            rbody.gravityScale = 2;　//登っていない時の重力は元の通り
        }

        if (onGround)
        {
            if (axisH == 0)
            {
                nowAnime = idleAnime;
            }
            else
            {
                nowAnime = runAnime;
            }
        }
        else
        {
            nowAnime = jumpAnime;
        }

        //downがtrueならキャラクターを屈ませる
        if (down)
        {
            nowAnime = crouchAnime;
        }

        //goClimbがtrueなら登るアニメにする
        if (goClimb)
        {
            nowAnime = climbAnime;
        }

        if (nowAnime != oldAnime)
        {
            oldAnime = nowAnime;
            animator.Play(nowAnime);
        }

    }
    public void Jump()
    {
        goJump = true;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ScoreItem")
        {
            ItemData item = collision.gameObject.GetComponent<ItemData>();
            score = item.value;

            //ゲットしたアイテムの削除
            Destroy(collision.gameObject);

            //自動生成されるまでの時間を設定
            time = 1.0f;
            isSpawn = true;
        }
        
        //Damageタグが付いたものに接触するとスコアが減る
        else if (collision.gameObject.tag == "Damage") 
        {
            score--;
            animator.Play(hurtAnime);         
        }
    }
}