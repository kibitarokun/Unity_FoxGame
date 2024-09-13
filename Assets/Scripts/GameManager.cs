using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject mainImage;

    //時間制限追加
    public GameObject timeBar;
    public GameObject timeText;
    TimeController timeCnt;

    //スコア追加
    public GameObject scoreText;
    public static int totalScore; //合計スコア
    public int stageScore = 0;

    //ボタン追加
    public GameObject panel;
    public GameObject retryButton;
    public GameObject resultButton;

    Image titleImage;

    // Start is called before the first frame update
    void Start()
    {
        //スコアのリセット
        totalScore = 0;

        //タイトル画像とパネル非表示
        Invoke("InactiveImage", 1.0f);
        panel.SetActive(false);

        //時間制限追加
        timeCnt = GetComponent<TimeController>();
        if(timeCnt != null)
        {
            if(timeCnt.gameTime == 0.0f)
            {
                timeBar.SetActive(false);
            }
        }

        //スコア追加
        UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {
        totalScore += stageScore;
        stageScore = 0;
        UpdateScore();

        //ゲーム中
        if (PlayerController.gameState == "playing")
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            PlayerController playerCnt = player.GetComponent<PlayerController>();

            //時間制限追加
            //タイム更新
            if (timeCnt != null)
            {
                if (timeCnt.gameTime > 0.0f)
                {
                    int time = (int)timeCnt.displayTime;
                    timeText.GetComponent<TextMeshProUGUI>().text = time.ToString();
                    if(time == 0)
                    {
                        PlayerController.gameState = "gameend";

                        //ゲーム終了と共にキャラクターの動き停止
                        Animator anim = player.GetComponent<Animator>();
                        anim.enabled = false;
                    }
                }
            }  
            
            //スコア追加
            if (playerCnt.score != 0)
            {
                stageScore += playerCnt.score;
                playerCnt.score = 0;
                UpdateScore();
            }
        }
        
        //タイムアップしたらパネルを表示
        if (PlayerController.gameState == "gameend")
        {
            panel.SetActive(true);
        }
    }

    void InactiveImage()
    {
        mainImage.SetActive(false);
    }

    //スコア追加
    void UpdateScore()
    {
        int score = stageScore + totalScore;
        scoreText.GetComponent<TextMeshProUGUI>().text = score.ToString();
    }
}
