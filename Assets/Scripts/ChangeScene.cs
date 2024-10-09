using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName; //読み込むシーン

    //ボタンを押した時の効果音はシーンが変わると無効になるらしいのでSoundManager.csから切り離し、
    //ボタンに直接ついているこのスクリプトに追記することで音声が再生されるようになった。
    private AudioSource audioSource;
    public AudioClip seButton;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //シーンを読み込む
    public void Load()
    {
        SceneManager.LoadScene(sceneName);
    }

    //各ステージでボタンを押した時の効果音再生
    public void ButtonClick()
    {
            audioSource = SoundManager.soundManager.GetComponent<AudioSource>();
            audioSource.PlayOneShot(seButton);       
    }
}
