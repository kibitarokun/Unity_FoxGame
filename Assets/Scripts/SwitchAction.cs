using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchAction : MonoBehaviour
{
    //�X�C�b�`�������ƃ_�C���ł����ǂ���K���o������d�g��
    public GameObject targetItem;
    public Sprite imageOn;
    public Sprite imageOff;
    bool on = false; //�X�C�b�`�̏�ԁitrue:������Ă���@false:�@������Ă��Ȃ��j

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = imageOff;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")�@//�v���C���[�ƐڐG�����
        {
            on = true;�@//�X�C�b�`��ON�ɂȂ�
            GetComponent<SpriteRenderer>().sprite = imageOn;
            //ItemManager�X�N���v�g�̎��상�\�b�h������
            ItemManager ItemM = targetItem.GetComponent<ItemManager>();�@
            ItemM.ActiveK();
        }
    }
}
