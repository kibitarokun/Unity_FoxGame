using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//タイトル画面でゲームのサウンドをON/OFFできるボタン用のスクリプト

public class AudioController : MonoBehaviour
{　
    bool isMuted = false;　//ミュートのオンオフの切り替え
    public TextMeshProUGUI textBox;　//ボタンのテキスト用の変数

    void Start()
    {
        if (isMuted)　//ミュートなら
        {
            textBox.text = "SOUND ON";　//ON表示
        }
        else　//音が出ているなら
        {
            textBox.text = "SOUND OFF";　//OFF表示
        }
    }
    public void ToggleMute()　//ボタンのクリックイベントに登録する
    {
        isMuted = !isMuted;　//クリックごとに切り替わる

        if (isMuted)　
        {
            AudioListener.volume = 0; //音をミュートにする
            textBox.text = "SOUND ON";　
        }
        else
        {
            AudioListener.volume = 1; //音を通常に戻す
            textBox.text = "SOUND OFF";
        }
    }
}

//備忘録）
//AudioListenerのボリュームを調整することでゲーム全体のサウンドのオンオフを行った。
//ボタンのテキストは音が出ている時は"SOUND ON"に、ミュートの時は"SOUND OFF"が表示されるように
//TextMeshProUGUIのtextを置き換えた。
