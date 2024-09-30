using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    public float moveX = 0.0f; //x�̈ړ��l
    public float moveY = 0.0f; //y�̈ړ��l
    public float times = 0.0f; //�ړ�����
    public float wait = 0.0f; //��~����
    public bool isMoveWhenOn = false;�@//��������ɓ����d�g��
    public bool isCanMove = true;  //true�̎��ɓ���
    Vector3 startPos;�@//�����ʒu
    Vector3 endPos;�@//�I���ʒu
    bool isReverse = false;�@//���]�t���O

    float movep = 0;�@//�ړ��⊮�l
    
    void Start()
    {
        startPos = transform.position;�@//�����ʒu�̐ݒ�
        endPos = new Vector2(startPos.x + moveX, startPos.y + moveY);�@//�ړ���̈ʒu�̐ݒ�

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
            //�ړ��⊮�l�̎Z�o
            float distance = Vector2.Distance(startPos, endPos);�@//�ړ�����
            float ds = distance / times;�@//�P�b������̈ړ�����
            float df = ds * Time.deltaTime;�@//�P�t���[��������̈ړ�����
            movep += df / distance;�@//�ړ��⊮�l

            if (isReverse)�@//���]
            {
                transform.position = Vector2.Lerp(endPos,startPos, movep);�@//�n�_,�I�_,�ړ��⊮�l�i0�`1�j
            }
            else
            {
                transform.position = Vector2.Lerp(startPos, endPos, movep);�@//���ʒu
            }

            if(movep >= 1.0f)�@//�����⊮�l��1�ɒB������
            {
                movep = 0.0f;�@//���Z�b�g
                isReverse = !isReverse;�@//���]�t���O������
                isCanMove = false;�@//�ړ���~

                //if (isMoveWhenOn == false)
                //{
                    Invoke("Move", wait);�@//��~���Ԍ�ɁA�����o��
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

    //�ڐG�J�n
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
    //�ڐG    �I��
    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null);
        }
    }

    //�ړ��͈͕\��
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
    