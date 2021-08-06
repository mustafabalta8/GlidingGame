using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] Rocketman rocketman;

    [SerializeField] Text ScoreText;

    int score;


    void Update()
    {
        if (rocketman.HasLaunch == true && rocketman.gameOver == false)
        {
            score += (int)(rocketman.transform.position.z * Time.deltaTime * 10);
            ScoreText.text = score.ToString();
        }
        
       
    }
    public int GetScore()
    {
        return score;
    }
}
