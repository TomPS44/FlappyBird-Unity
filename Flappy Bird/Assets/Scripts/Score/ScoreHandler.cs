using System;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour
{
    
    public int playerScore;
    public int bestScore;

    public bool newBestScore;

    public TextMeshProUGUI scoreText;

    [SerializeField] private PipeController pipeController;


    private string bestScorekey = "BestScore";

    void Awake() 
    {
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        DisplayScore();

        playerScore = 0;

        bestScore = PlayerPrefs.GetInt(bestScorekey);
        
        newBestScore = false;
    }

    [ContextMenu("DisplayScore")]
    public void DisplayScore()
    {
        // well... I don't know why this shit doesn't work :(

        string scoreString;

        if (playerScore > 99)
        {
            scoreString = playerScore.ToString();
        }
        else if (playerScore > 9)
        {
            scoreString = "0" + playerScore.ToString();
        }
        else
        {
            scoreString = "00" + playerScore.ToString();
        }

        scoreText.text = scoreString;
    }

    public void ResetScore()
    {
        playerScore = 0;

        newBestScore = false;

        scoreText.color = new Color(scoreText.color.r, scoreText.color.g, scoreText.color.b, 1f);
    }
    public void AddScore()
    {
        playerScore++;
    }

    public void FadeScore()
    {
        StartCoroutine(UIHelper.Fade(scoreText, 0.75f));
    }

    public void SetBestScore()
    {
        if (playerScore > bestScore)
        {
            bestScore = playerScore;

            newBestScore = true;
        }

        PlayerPrefs.SetInt(bestScorekey, bestScore);
    }

    [ContextMenu("IncreaseDifficultyOnce")]
    private void UselessIncreaseDifficulty()
    {
        pipeController.IncreaseDifficulty();
    }

    public void IncreaseDifficulty()
    {
        if (playerScore < 20f || playerScore > 120f)
            return;


        if (playerScore == 120f)
        {
            pipeController.IncreaseDifficulty();
        }
        else if (playerScore == 100f)
        {
            pipeController.IncreaseDifficulty();
        }
        else if (playerScore == 80f)
        {
            pipeController.IncreaseDifficulty();
        }
        else if (playerScore == 65f)
        {
            pipeController.IncreaseDifficulty();
        }
        else if (playerScore == 50f)
        {
            pipeController.IncreaseDifficulty();
        }
        else if (playerScore == 35f)
        {
            pipeController.IncreaseDifficulty();
        }
        else if (playerScore == 20f)
        {
            pipeController.IncreaseDifficulty();
        }
    }
    public void ResetDifficulty()
    {
        pipeController.ResetSpeeds();
    }
    
}
