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

        //high score�̍X�V
        if (PlayerPrefs.GetInt("HighScore") < score)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }

        //hith score��\��
        HighScore();
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    //high score��\�����郁�\�b�h
    public void HighScore()
    {
        highScoreText.text = "High Score : " + PlayerPrefs.GetInt("HighScore");
    }

    //high score�����Z�b�g���郁�\�b�h
    public void ResetScore()
    {
        PlayerPrefs.SetInt("HighScore", 0);
        HighScore(); 
    }

    //�Q�[���I��
    public void EndClicked()
    {
        Application.Quit(); //�A�v���I��
    }
}
