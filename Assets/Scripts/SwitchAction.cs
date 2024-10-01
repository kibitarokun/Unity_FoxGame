using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchAction : MonoBehaviour
{
    //�X�C�b�`�������ƃ_�C���ł����ǂ���K���o������
    public GameObject targetItem;
    public Sprite imageOn;
    public Sprite imageOff;

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
            GetComponent<SpriteRenderer>().sprite = imageOn; //�X�C�b�`���쓮����

            //ItemManager�X�N���v�g�̎��상�\�b�h������
            ItemManager ItemM = targetItem.GetComponent<ItemManager>();�@
            ItemM.ActiveK();
        }
    }
}
