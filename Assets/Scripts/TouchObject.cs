using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchObject : MonoBehaviour
{
    //�u���b�N�����グ�Ēu��
    public GameObject gameObj;�@//�v���C���[
    public GameObject target;�@//�v���C���[�̑O���ɂ���ڈ�i�u���b�N�������^�Ԉʒu�j
    public bool isRelease;�@//�u���b�N������t���O

    // Start is called before the first frame update
    void Start()
    {
        isRelease = true;
    }

    // Update is called once per frame
    void Update()
    {
        //�X�y�[�X�L�[�Ńu���b�N�����
        if (Input.GetKeyDown(KeyCode.Space))�@
        {
            if (!isRelease)
            {
                transform.parent = null;
                isRelease = true;
            }
        }
    }

    //�v���C���[�����E����u���b�N�ɐڂ���Ǝ����^�ׂ�悤�ɂ���
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")�@//�v���C���[�Ɛڂ����
        {
            //target�̏ꏊ���u���b�N�̃|�W�V�����ɂȂ�
            this.transform.position = new Vector2(target.transform.position.x, target.transform.position.y+0.2f);
            transform.SetParent(gameObj.transform);�@//�v���C���[�̎q�ɂȂ�
            isRelease = false;�@//�����t���O��false�ɂ���
        }
    }
}
