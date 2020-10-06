using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    Text ScoreText;
    float NextScore = 0;

    void Start()
    {
        ScoreText = GetComponent<Text>();
    }

    void Update()
    {
        if (MainGame.instance.gameinstance.ScoreList.Count > 0)
        {
            NextScore += MainGame.instance.gameinstance.ScoreList[0];
            MainGame.instance.gameinstance.ScoreList.RemoveAt(0);
        }
        if (MainGame.instance.gameinstance.Score < NextScore)
        {
            MainGame.instance.gameinstance.Score = Mathf.Lerp(MainGame.instance.gameinstance.Score, NextScore + 0.1f, 0.1f);
        }
        ScoreText.text = "점수 : " + (int)MainGame.instance.gameinstance.Score; 
    }
}
