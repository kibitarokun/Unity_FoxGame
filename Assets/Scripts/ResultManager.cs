using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultManager : MonoBehaviour
{    
    public GameObject scoreText;
    public TextMeshProUGUI highScoreText;
    int score = GameManager.totalScore;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.GetComponent<TextMeshProUGUI>().text = score.ToString();

        //high scoreの更新
        if (PlayerPrefs.GetInt("HighScore") < score)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }

        //hith scoreを表示
        HighScore();
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    //high scoreを表示するメソッド
    public void HighScore()
    {
        highScoreText.text = "High Score : " + PlayerPrefs.GetInt("HighScore");
    }

    //high scoreをリセットするメソッド
    public void ResetScore()
    {
        PlayerPrefs.SetInt("HighScore", 0);
        HighScore(); 
    }

    //ゲーム終了
    public void EndClicked()
    {
        Application.Quit(); //アプリ終了
    }
}
