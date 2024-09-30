using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_ground : MonoBehaviour
{
    public float speed = 3.0f;�@//�ړ��X�s�[�h
    public bool toRight;�@//true�͉E�����Afalse�͍�����
    public LayerMask groundLayer;�@//�n�ʔ���

    // Start is called before the first frame update
    void Start()
    {
        if (toRight) //�E������true��������
        {
            transform.localScale = new Vector2(5,5); //�E����
        }
    }
   
    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        //�n�ʂɐڂ��Ă��邩����
        bool onGround = Physics2D.CircleCast(transform.position, 0.5f, 
                                             Vector2.down, 0.5f, groundLayer);

        if (onGround)�@//�n�ʂɐڂ��Ă���Έړ�����
        {
            Rigidbody2D rbody = GetComponent<Rigidbody2D>();

            if (toRight)�@
            {
                rbody.velocity = new Vector2(speed, rbody.velocity.y);�@//�E�ړ�
            }
            else�@
            {
                rbody.velocity = new Vector2(-speed, rbody.velocity.y);�@//���ړ�
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //���̃R���C�_�[�ɐڐG�������ɂ͔��]����
        toRight = !toRight;

        if (toRight)
        {
            transform.localScale = new Vector2(5,5);�@//�E����
        }
        else
        {
            transform.localScale = new Vector2(-5, 5);�@//������
        }
    }
}
