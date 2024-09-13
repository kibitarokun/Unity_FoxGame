using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject mainImage;

    //���Ԑ����ǉ�
    public GameObject timeBar;
    public GameObject timeText;
    TimeController timeCnt;

    //�X�R�A�ǉ�
    public GameObject scoreText;
    public static int totalScore; //���v�X�R�A
    public int stageScore = 0;

    //�{�^���ǉ�
    public GameObject panel;
    public GameObject retryButton;
    public GameObject resultButton;

    Image titleImage;

    // Start is called before the first frame update
    void Start()
    {
        //�X�R�A�̃��Z�b�g
        totalScore = 0;

        //�^�C�g���摜�ƃp�l����\��
        Invoke("InactiveImage", 1.0f);
        panel.SetActive(false);

        //���Ԑ����ǉ�
        timeCnt = GetComponent<TimeController>();
        if(timeCnt != null)
        {
            if(timeCnt.gameTime == 0.0f)
            {
                timeBar.SetActive(false);
            }
        }

        //�X�R�A�ǉ�
        UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {
        totalScore += stageScore;
        stageScore = 0;
        UpdateScore();

        //�Q�[����
        if (PlayerController.gameState == "playing")
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            PlayerController playerCnt = player.GetComponent<PlayerController>();

            //���Ԑ����ǉ�
            //�^�C���X�V
            if (timeCnt != null)
            {
                if (timeCnt.gameTime > 0.0f)
                {
                    int time = (int)timeCnt.displayTime;
                    timeText.GetComponent<TextMeshProUGUI>().text = time.ToString();
                    if(time == 0)
                    {
                        PlayerController.gameState = "gameend";

                        //�Q�[���I���Ƌ��ɃL�����N�^�[�̓�����~
                        Animator anim = player.GetComponent<Animator>();
                        anim.enabled = false;
                    }
                }
            }  
            
            //�X�R�A�ǉ�
            if (playerCnt.score != 0)
            {
                stageScore += playerCnt.score;
                playerCnt.score = 0;
                UpdateScore();
            }
        }
        
        //�^�C���A�b�v������p�l����\��
        if (PlayerController.gameState == "gameend")
        {
            panel.SetActive(true);
        }
    }

    void InactiveImage()
    {
        mainImage.SetActive(false);
    }

    //�X�R�A�ǉ�
    void UpdateScore()
    {
        int score = stageScore + totalScore;
        scoreText.GetComponent<TextMeshProUGUI>().text = score.ToString();
    }
}
