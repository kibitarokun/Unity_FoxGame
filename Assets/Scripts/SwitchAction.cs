using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchAction : MonoBehaviour
{
    //スイッチを押すとダイヤでかたどったKが出現する仕組み
    public GameObject targetItem;
    public Sprite imageOn;
    public Sprite imageOff;
    bool on = false; //スイッチの状態（true:押されている　false:　押されていない）

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
        if (collision.gameObject.tag == "Player")　//プレイヤーと接触すると
        {
            on = true;　//スイッチがONになる
            GetComponent<SpriteRenderer>().sprite = imageOn;
            //ItemManagerスクリプトの自作メソッドが発動
            ItemManager ItemM = targetItem.GetComponent<ItemManager>();　
            ItemM.ActiveK();
        }
    }
}
