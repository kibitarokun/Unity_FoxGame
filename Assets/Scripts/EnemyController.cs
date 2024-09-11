using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    //�v���C���[���߂Â��ƒǂ�������d�g�݂ɂ���
    bool isActive = false;�@//true�ɂȂ�ƃv���C���[��ǂ�������
    public float speed = 0.5f; //�v���C��[��ǂ�������X�s�[�h
    public float reactionDistance = 4.0f;�@//�v���C���[�����m����͈�
    float axisH;�@
    float axisV;�@
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

        GameObject player = GameObject.FindGameObjectWithTag("Player");�@//�v���C���[��������
        if (player != null)�@//�v���C���[�����݂���
        {
            //�v���C���[�Ƃ̋��������߂�reactionDistance���ł���Δ�������
            float dist = Vector2.Distance(transform.position, player.transform.position);
            if (dist < reactionDistance)
            {
                isActive = true;
            }
            else
            {
                isActive= false;
            }

            //�ǐՊJ�n
            if (isActive)
            {
                //Atan2���\�b�h�Ńv���C���[�Ɍ������p�x�����߂�@
                float dx = player.transform.position.x - transform.position.x;
                float dy = player.transform.position.y - transform.position.y;
                float rad = Mathf.Atan2(dy, dx);

                //speed�̒l���������Đڋ߂���
                axisH = Mathf.Cos(rad) * speed;
                axisV = Mathf.Sin(rad) * speed;

                if (axisH > 0.0f)�@//�������v���X�̒l�Ȃ�
                {
                    transform.localScale = new Vector2(-3, 3); //�E����
                }
                else if (axisH < 0.0f)�@//�������}�C�i�X�̒l�Ȃ�
                {
                    transform.localScale = new Vector2(3, 3); //������
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
            //�ړ�
            rbody.velocity = new Vector2(axisH, axisV).normalized;
        }
        else
        {
            rbody.velocity = Vector2.zero;
        }
    }   
}
