using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//�^�C�g����ʂŃQ�[���̃T�E���h��ON/OFF�ł���{�^���p�̃X�N���v�g

public class AudioController : MonoBehaviour
{�@
    bool isMuted = false;�@//�~���[�g�̃I���I�t�̐؂�ւ�
    public TextMeshProUGUI textBox;�@//�{�^���̃e�L�X�g�p�̕ϐ�

    void Start()
    {
        if (isMuted)�@//�~���[�g�Ȃ�
        {
            textBox.text = "SOUND ON";�@//ON�\��
        }
        else�@//�����o�Ă���Ȃ�
        {
            textBox.text = "SOUND OFF";�@//OFF�\��
        }
    }
    public void ToggleMute()�@//�{�^���̃N���b�N�C�x���g�ɓo�^����
    {
        isMuted = !isMuted;�@//�N���b�N���Ƃɐ؂�ւ��

        if (isMuted)�@
        {
            AudioListener.volume = 0; //�����~���[�g�ɂ���
            textBox.text = "SOUND ON";�@
        }
        else
        {
            AudioListener.volume = 1; //����ʏ�ɖ߂�
            textBox.text = "SOUND OFF";
        }
    }
}

//���Y�^�j
//AudioListener�̃{�����[���𒲐����邱�ƂŃQ�[���S�̂̃T�E���h�̃I���I�t���s�����B
//�{�^���̃e�L�X�g�͉����o�Ă��鎞��"SOUND ON"�ɁA�~���[�g�̎���"SOUND OFF"���\�������悤��
//TextMeshProUGUI��text��u���������B
