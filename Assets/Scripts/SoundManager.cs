using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BGMType�@//BGM�^�C�v
{
    None,�@//�Ȃ�
    Title,�@//�^�C�g�����
    InGame,�@//�Q�[����
    Result,�@//���U���g���
}

public enum SEType //���ʉ�
{
    Get,�@//�_�C�����Q�b�g������
    Damage,�@//�_���[�W��H�������
    Switch, //�X�C�b�`���I���ɂ�����
}

public class SoundManager : MonoBehaviour
{
    //AudioClip�^�̎󂯎M�i�ϐ��j���쐬
    public AudioClip bgmNone;�@//BGM���߂�p�ɖ����̉����f�[�^��K�p�@
    public AudioClip bgmInTitle;
    public AudioClip bgmInGame;
    public AudioClip bgmInResult;

    public AudioClip seGet;
    public AudioClip seDamage;
    public AudioClip seSwitch;

    public static SoundManager soundManager; //�ŏ���SoundManager��ۑ�����ϐ�
    public static BGMType playingBGM = BGMType.None; //�Đ���BGM

    private void Awake()
    {
        //BGM�Đ�
        if (soundManager == null)
        {
            soundManager = this; //static�ϐ��Ɏ�����ۑ�
            //�V�[�����ς���Ă��Q�[���I�u�W�F�N�g��j�����Ȃ�
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); //�Q�[���I�u�W�F�N�g��j��
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

    //BGM�ݒ�
    public void PlayBgm(BGMType type)
    {
        if (type != playingBGM)
        {
            playingBGM = type;
            AudioSource audio = GetComponent<AudioSource>();
            if (type == BGMType.Title)
            {
                audio.clip = bgmInTitle; //�^�C�g��BGM���Z�b�g
            }
            else if (type == BGMType.InGame)
            {
                audio.clip = bgmInGame; //�Q�[������BGM���Z�b�g
            }
            else if (type == BGMType.Result)
            {
                audio.clip = bgmInResult; //���U���gBGM���Z�b�g
            }
            audio.Play(); //�Đ�
        }       
    }

    //BGM��~�i�����̉����f�[�^���Đ��j
    public void StopBgm(BGMType type)
    {
        playingBGM = type;
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = bgmNone;
        audio.Play();
    }
    
    //���ʉ��Đ�
    public void SEPlay(SEType type)
    {
        if(type == SEType.Get)
        {
            GetComponent<AudioSource>().PlayOneShot(seGet); //�_�C�����Q�b�g
        }
        else if(type == SEType.Damage)
        {
            GetComponent<AudioSource>().PlayOneShot(seDamage); //�_���[�W��H�炤
        }
        else if (type == SEType.Switch)
        {
            GetComponent<AudioSource>().PlayOneShot(seSwitch); //�X�C�b�`���I���ɂ�����
        }
    }
}

//���Y�^�j
//�Q�[���G���h����BGM��~+�{�^���̃I���N���b�N�C�x���g�Ō��ʉ��Đ�������̂ɋ�J�����B
//GetComponent<AudioSource>().Stop();���ƑS�ẴT�E���h����~���Ă��܂��̂ŁA
//AudioSource��ʂɗp�ӂ��āA�{�^���̃I���N���b�N�C�x���g�ɓo�^�������ALoad���\�b�h�ƕ��p����Ƃ��܂��Đ�����Ȃ������B
//������Ƃ��āA�����̉����f�[�^���Đ�������BGM���߂Ȃ����Ƃɂ����B

