using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class SwitchAction : MonoBehaviour
{
    //スイッチを押すとダイヤでかたどったKが出現する
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
        if (collision.gameObject.tag == "Player")　//プレイヤーと接触すると
        {
            GetComponent<SpriteRenderer>().sprite = imageOn; //スイッチが作動する       

            //効果音発生
            SoundManager.soundManager.SEPlay(SEType.Switch);

            //ItemManagerスクリプトの自作メソッドが発動
            ItemManager ItemM = targetItem.GetComponent<ItemManager>();
            ItemM.ActiveK();

            //一度触れたら用済みなのでこのスクリプトは消す（音が鳴らなくなる）
            Destroy(this);
        }
    }
}
