using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName; //�ǂݍ��ރV�[��

    //�{�^�������������̌��ʉ��̓V�[�����ς��Ɩ����ɂȂ�炵���̂�SoundManager.cs����؂藣���A
    //�{�^���ɒ��ڂ��Ă��邱�̃X�N���v�g�ɒǋL���邱�Ƃŉ������Đ������悤�ɂȂ����B
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

    //�V�[����ǂݍ���
    public void Load()
    {
        SceneManager.LoadScene(sceneName);
    }

    //�e�X�e�[�W�Ń{�^�������������̌��ʉ��Đ�
    public void ButtonClick()
    {
            audioSource = SoundManager.soundManager.GetComponent<AudioSource>();
            audioSource.PlayOneShot(seButton);       
    }
}
