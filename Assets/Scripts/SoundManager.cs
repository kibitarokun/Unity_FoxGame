using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BGMType　//BGMタイプ
{
    None,　//なし
    Title,　//タイトル画面
    InGame,　//ゲーム中
    Result,　//リザルト画面
}

public enum SEType //効果音
{
    Get,　//ダイヤをゲットした時
    Damage,　//ダメージを食らった時
    Switch, //スイッチをオンにした時
}

public class SoundManager : MonoBehaviour
{
    //AudioClip型の受け皿（変数）を作成
    public AudioClip bgmNone;　//BGMを停める用に無音の音声データを適用　
    public AudioClip bgmInTitle;
    public AudioClip bgmInGame;
    public AudioClip bgmInResult;

    public AudioClip seGet;
    public AudioClip seDamage;
    public AudioClip seSwitch;

    public static SoundManager soundManager; //最初のSoundManagerを保存する変数
    public static BGMType playingBGM = BGMType.None; //再生中BGM

    private void Awake()
    {
        //BGM再生
        if (soundManager == null)
        {
            soundManager = this; //static変数に自分を保存
            //シーンが変わってもゲームオブジェクトを破棄しない
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); //ゲームオブジェクトを破棄
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //BGM設定
    public void PlayBgm(BGMType type)
    {
        if (type != playingBGM)
        {
            playingBGM = type;
            AudioSource audio = GetComponent<AudioSource>();
            if (type == BGMType.Title)
            {
                audio.clip = bgmInTitle; //タイトルBGMをセット
            }
            else if (type == BGMType.InGame)
            {
                audio.clip = bgmInGame; //ゲーム中のBGMをセット
            }
            else if (type == BGMType.Result)
            {
                audio.clip = bgmInResult; //リザルトBGMをセット
            }
            audio.Play(); //再生
        }       
    }

    //BGM停止（無音の音声データを再生）
    public void StopBgm(BGMType type)
    {
        playingBGM = type;
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = bgmNone;
        audio.Play();
    }
    
    //効果音再生
    public void SEPlay(SEType type)
    {
        if(type == SEType.Get)
        {
            GetComponent<AudioSource>().PlayOneShot(seGet); //ダイヤをゲット
        }
        else if(type == SEType.Damage)
        {
            GetComponent<AudioSource>().PlayOneShot(seDamage); //ダメージを食らう
        }
        else if (type == SEType.Switch)
        {
            GetComponent<AudioSource>().PlayOneShot(seSwitch); //スイッチをオンにした時
        }
    }
}

//備忘録）
//ゲームエンド時にBGM停止+ボタンのオンクリックイベントで効果音再生させるのに苦労した。
//GetComponent<AudioSource>().Stop();だと全てのサウンドが停止してしまうので、
//AudioSourceを別に用意して、ボタンのオンクリックイベントに登録したが、Loadメソッドと併用するとうまく再生されなかった。
//解決策として、無音の音声データを再生させてBGMを停めないことにした。

