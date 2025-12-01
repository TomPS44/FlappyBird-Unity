using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour
{
    
    public int playerScore;

    public TextMeshProUGUI scoreText;


    void Start()
    {
        DisplayScore();
    }

    [ContextMenu("DisplayScore")]
    public void DisplayScore()
    {
        // well... I don't knom why this shit doesn't work :(

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





    public void Display()
    {
        
    }

}
