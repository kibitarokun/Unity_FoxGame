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

    bool down; //�L�����N�^�[�����܂���bool�^�̕ϐ�

    Animator animator;
    public string idleAnime = "PlayerIdle";
    public string runAnime = "PlayerRun";
    public string hurtAnime = "PlayerHurt";
    public string crouchAnime = "PlayerCrouch";
    public string climbAnime = "PlayerClimb";
    public string jumpAnime = "PlayerJump";
    string nowAnime = "";
    string oldAnime = "";

    //�X�R�A�ǉ�
    public int score = 0;

    //�Q�[���̏��
    public static string gameState = "playing";

    //�Q�b�g������̃A�C�e���̍ďo��
    [SerializeField] GameObject Item;�@//Prefab�A�C�e�����w��
    //SerializeField��t���邱�Ƃ�Inspector����A�N�Z�X��
    //(�����private��t����Α��N���X����̏��������s�ƂȂ� ���Z�L�����e�B�΍�)
    float time; //�A�C�e���o���܂ł̎���
    bool isSpawn;
    //�����_���ȏꏊ�͈̔�
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
        down = Input.GetKey(KeyCode.DownArrow);�@//�����L�[�����������Ă��邩�����m���Atrue/false�ŕԂ�

        if (axisH > 0.0f)
        {
            Debug.Log("�E�ړ�");
            transform.localScale = new Vector2(7, 7);
        }
        else if (axisH < 0.0f)
        {
            Debug.Log("���ړ�");
            transform.localScale = new Vector2(-7, 7);
        }

        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("�W�����v");
            Jump();
        }

        //���Ԃ̌o��
        time -= Time.deltaTime;

        //�A�C�e����rangeA��rangeB�͈͓̔��Ń����_���ȏꏊ�ɏo������
        float x = Random.Range(rangeA.position.x, rangeB.position.x);
        float y = Random.Range(rangeA.position.y, rangeB.position.y);
        float z = Random.Range(rangeA.position.z, rangeB.position.z);
        Vector3 pos = new Vector3(x, y, z);
      
        //1�b�o�߂���isSpawn��true�Ȃ�
        if(time <= 0 && isSpawn)
        {
            //�w�肵��Prefab�A�C�e���̎�������
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

        //�͂�����o�铮��
        //�͂��������m���邽�߂�Raycast�i�ǂ�����A�ǂ̕����ɁA�ǂꂭ�炢�̋����ŁA�Ώۃ��C���[�j
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, 2.0f, ladderLayer);

        if (hitInfo.collider != null)�@//�@�����͂�������������
        {
            if (axisV > 0)�@//�A�v���C���[������L�[����������
            {
                goClimb = true;
            }
        }
        else
        {
            goClimb = false;
        }

        if (goClimb)�@//goClimb��true�Ȃ�o��
        {
            Debug.Log("�o��");
            rbody.velocity = new Vector2(rbody.velocity.x, axisV * speed);
            rbody.gravityScale = 0;�@//�͂����̓r���œ������~�߂�
        }
        else
        {
            rbody.gravityScale = 2;�@//�o���Ă��Ȃ����̏d�͂͌��̒ʂ�
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

        //down��true�Ȃ�L�����N�^�[�����܂���
        if (down)
        {
            nowAnime = crouchAnime;
        }

        //goClimb��true�Ȃ�o��A�j���ɂ���
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

            //�Q�b�g�����A�C�e���̍폜
            Destroy(collision.gameObject);

            //�������������܂ł̎��Ԃ�ݒ�
            time = 1.0f;
            isSpawn = true;
        }
        
        //Damage�^�O���t�������̂ɐڐG����ƃX�R�A������
        else if (collision.gameObject.tag == "Damage") 
        {
            score--;
            animator.Play(hurtAnime);         
        }
    }
}