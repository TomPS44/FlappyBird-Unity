using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static System.Runtime.CompilerServices.RuntimeHelpers;


public class UIController : MonoBehaviour
{
    [Header("Scripts References")]
    [SerializeField] private GameController gameController;
    [SerializeField] private ScoreHandler scoreHandler;

    [Header("Animations and GameObjects stuff")]
    [SerializeField] private Image[] medals = new Image[4];
    [SerializeField] private CustomCollection[] medalsParameters = new CustomCollection[2];

    [Space]

    [SerializeField] private CustomCollection[] gameParameters = new CustomCollection[2];
    [SerializeField] private CustomCollection[] overParameters = new CustomCollection[2];

    [Space]

    [SerializeField] private CustomCollection[] mainFrameParameters = new CustomCollection[2];

    [Space]

    [SerializeField] private CustomCollection[] playAgainbuttonParameters = new CustomCollection[2];

    [Space]

    [SerializeField] private CustomCollection[] mainMenuButtonParameters = new CustomCollection[2];

    [Space]

    [Header("Idles State Images")]
    [SerializeField] private CustomCollection[] getParameters = new CustomCollection[2];
    public CustomCollection[] readyParameters = new CustomCollection[2];

    [Space]

    [SerializeField] private CustomCollection[] tutorialImageParameters = new CustomCollection[2];

    [SerializeField] private float sineMovementFactor;

    public bool isGameReady;

    [Header("Score Stuff")]

    [SerializeField] private GameObject tempPlayerScore;
    private TextMeshProUGUI tempPlayerScoreText;
    [SerializeField] private GameObject tempBestScore;
    private TextMeshProUGUI tempBestScoreText;
    [SerializeField] private Image newBestScoreImage;

    [HideInInspector] public bool fadePipes;



    private bool isRestarting;


    private void Start()
    {
        fadePipes = false;

        gameController.OnPlayerLose += StartSpawnLossScreen;


        tempPlayerScoreText = tempPlayerScore.GetComponent<TextMeshProUGUI>();
        tempBestScoreText = tempBestScore.GetComponent<TextMeshProUGUI>(); 
        
        ResetTempTexts();
        ResetUIs();

        isRestarting = false;  
        isGameReady = false;
    }


    private void StartSpawnLossScreen()
    {
        StartCoroutine(SpawnLossScreen());
    }
    public void StartDespawnLossScreen()
    {
        StartCoroutine(DespawnLossScreen());
    }

    private IEnumerator SpawnLossScreen()
    {
        UIHelper.CallCustomLerp(gameParameters[0]);
        UIHelper.CallCustomLerp(overParameters[0]);

        yield return new WaitForSeconds(1f);

        UIHelper.CallCustomLerp(mainFrameParameters[0]);

        yield return new WaitForSeconds(1f);

        StartCoroutine(UIHelper.FadeToApparent(tempPlayerScoreText, 0.75f));
        yield return StartCoroutine(UIHelper.FadeToApparent(tempBestScoreText, 0.75f));


        StartCoroutine(UIHelper.CustomIncreaseScore(tempPlayerScoreText, scoreHandler.playerScore));
        yield return StartCoroutine(UIHelper.CustomIncreaseScore(tempBestScoreText, scoreHandler.bestScore));

        if (scoreHandler.newBestScore)
        {
            StartCoroutine(UIHelper.FadeToApparent(newBestScoreImage, 1f));
        }
        // DO NOT FORGET TO DISPLAY THE SCORE
        // /!\ /!\ /!\ /!\ /!\

        yield return StartCoroutine(SpawnMedal());

        UIHelper.CallCustomLerp(mainMenuButtonParameters[0]);

        yield return new WaitForSeconds(0.25f);

        UIHelper.CallCustomLerp(playAgainbuttonParameters[0]);
    }
    
    public IEnumerator DespawnLossScreen()
    {
        if (isRestarting) yield break;

        isRestarting = true;

        UIHelper.CallCustomLerp(gameParameters[1]);
        UIHelper.CallCustomLerp(overParameters[1]);

        yield return new WaitForSeconds(0.25f);

        UIHelper.CallCustomLerp(playAgainbuttonParameters[1]);
        UIHelper.CallCustomLerp(mainMenuButtonParameters[1]);

        yield return new WaitForSeconds(0.25f);

        StartCoroutine(UIHelper.Fade(tempPlayerScoreText, 1.25f));
        yield return StartCoroutine(UIHelper.Fade(tempBestScoreText, 1.25f));
        yield return StartCoroutine(ResetMedal());

        if (scoreHandler.newBestScore)
        {
            yield return StartCoroutine(UIHelper.Fade(newBestScoreImage, 1.25f));
        }


        tempPlayerScoreText.text = "000";
        tempBestScoreText.text = "000";
        

        // DO NOT FORGET TO RESET THE SCORE
        // /!\ /!\ /!\ /!\ /!\

        UIHelper.CallCustomLerp(mainFrameParameters[1]);

        fadePipes = true;
        yield return new WaitForSeconds(1f / 60f);
        fadePipes = false;

        yield return new WaitForSeconds(2f);

        gameController.RestartGame();

        isRestarting = false;
    }


    public IEnumerator SpawnMedal()
    {
        Image medal;

        // assigns an image to the medal, depending on the score (if statements going from gold medal to no medal)
        if (scoreHandler.playerScore >= 100)
        {
            medal = medals[0];
        }
        else if (scoreHandler.playerScore >= 50)
        {
            medal = medals[1];
        }
        else if (scoreHandler.playerScore >= 25)
        {
            medal = medals[2];
        }
        else
        {
            medal = medals[3];
        }
        

        medalsParameters[0].image = medal;
        medalsParameters[1].image = medal;

        UIHelper.CallCustomLerp(medalsParameters[0]);

        yield return new WaitForSeconds(scoreHandler.playerScore >= 20 ? 1f : 0.25f);
    }
    public IEnumerator ResetMedal()
    {
        UIHelper.CallCustomLerp(medalsParameters[1]);

        yield return new WaitForSeconds(scoreHandler.playerScore >= 20 ? 1f : 0f);
    }

    public void ResetUIs()
    {
        // reset the position of every UI element (  so that I don't have to do it myself :)  )
        gameParameters[0].image.rectTransform.anchoredPosition3D = gameParameters[1].targetPos;
        gameParameters[0].image.gameObject.SetActive(true);
        overParameters[0].image.rectTransform.anchoredPosition3D = overParameters[1].targetPos;
        overParameters[0].image.gameObject.SetActive(true);

        getParameters[0].image.rectTransform.anchoredPosition3D = getParameters[1].targetPos;
        getParameters[0].image.gameObject.SetActive(true);
        readyParameters[0].image.rectTransform.anchoredPosition3D = readyParameters[1].targetPos;
        readyParameters[0].image.gameObject.SetActive(true);

        tutorialImageParameters[0].image.rectTransform.anchoredPosition3D = tutorialImageParameters[0].targetPos;
        tutorialImageParameters[0].image.gameObject.SetActive(true);

        mainFrameParameters[0].image.rectTransform.anchoredPosition3D = mainFrameParameters[1].targetPos;
        mainFrameParameters[0].image.gameObject.SetActive(true);

        Color imageColor = newBestScoreImage.color;
        newBestScoreImage.color = new Color(imageColor.r, imageColor.g, imageColor.b, 0f);
        newBestScoreImage.gameObject.SetActive(true);

        RectTransform rt = playAgainbuttonParameters[0].button.gameObject.GetComponent<RectTransform>();
        rt.anchoredPosition3D = playAgainbuttonParameters[1].targetPos;
        playAgainbuttonParameters[0].button.gameObject.SetActive(true);

        rt = mainMenuButtonParameters[0].button.gameObject.GetComponent<RectTransform>();
        rt.anchoredPosition3D = mainMenuButtonParameters[1].targetPos;
        mainMenuButtonParameters[0].button.gameObject.SetActive(true);


        // reset also the medals, but the code is a bit longer since there are three 
        for (int i = 0; i < medals.Length; i++)
        {
            // sets the medal active ( in case I forgot to do it :) )
            medals[i].gameObject.SetActive(true);

            // and assigns an image to the parameters of the medals (to not cause an error), to then reset the position
            medalsParameters[0].image = medals[i];
            medalsParameters[0].image.rectTransform.anchoredPosition3D = medalsParameters[1].targetPos;
        }
        
    }

    public void SpawnIdleImages()
    {
        isGameReady = false;

        UIHelper.CallFadeToApparent(tutorialImageParameters[0].image, 2f);


        Vector3 spawnPos;

        spawnPos = new Vector3(tutorialImageParameters[0].targetPos.x + Mathf.Sin(Time.time), tutorialImageParameters[0].targetPos.y, tutorialImageParameters[0].targetPos.z);
        UIHelper.CallCustomLerp(tutorialImageParameters[0].image, spawnPos, tutorialImageParameters[0].speed, tutorialImageParameters[0].animCurve);

        spawnPos = new Vector3(getParameters[0].targetPos.x + Mathf.Sin(Time.time), getParameters[0].targetPos.y, getParameters[0].targetPos.z);
        UIHelper.CallCustomLerp(getParameters[0].image, spawnPos, getParameters[0].speed, getParameters[0].animCurve);

        spawnPos = new Vector3(readyParameters[0].targetPos.x + Mathf.Sin(Time.time), readyParameters[0].targetPos.y, readyParameters[0].targetPos.z);
        UIHelper.CallCustomLerp(readyParameters[0].image, spawnPos, readyParameters[0].speed, readyParameters[0].animCurve);

        // it just straight up perfectionism, I don't even want to explain it 
        StartCoroutine(SetIsGameReady());
        
        
        /*
        UIHelper.CallCustomLerp(getParameters[0]);
        UIHelper.CallCustomLerp(readyParameters[0]);
        */

        IEnumerator SetIsGameReady()
        {
            yield return new WaitForSeconds(1f);
            isGameReady = true;
        }
    }

    public IEnumerator DespawnIdleImages()
    {
        UIHelper.CallCustomLerp(getParameters[1]);        
        UIHelper.CallCustomLerp(readyParameters[1]);
        UIHelper.CallFade(tutorialImageParameters[1].image, 1.5f);
        yield return new WaitForSeconds(1f);
        UIHelper.CallCustomLerp(tutorialImageParameters[1]);

        // yield return new WaitForSeconds(1f);

        // UIHelper.CallCustomLerp(getParameters[1]);        
        // UIHelper.CallCustomLerp(readyParameters[1]);
    }

    private IEnumerator MoveWithSine(Image imageToMove, bool isBackwards)
    {
        while (true)
        {
            if (gameController.gameIsPlaying) yield break;

            float currentTime = Time.time;
            Vector3 targetPos = imageToMove.transform.position;

            if (isBackwards)
            {
                targetPos.y += Mathf.Sin(currentTime) * Time.deltaTime * sineMovementFactor;
            }
            else
            {
                targetPos.y -= Mathf.Sin(currentTime) * Time.deltaTime * sineMovementFactor;
            }
            

            imageToMove.transform.position = targetPos;

            yield return null;
        }
    }

    public void MoveWithSineIdleImages()
    {
        StartCoroutine(MoveWithSine(getParameters[0].image, false));
        StartCoroutine(MoveWithSine(readyParameters[0].image, true));
        // StartCoroutine(MoveWithSine(tutorialImageParameters[0].image));
    }



    public void ResetTempTexts()
    {
        tempPlayerScoreText.color = new Color(tempPlayerScoreText.color.r,    
                                              tempPlayerScoreText.color.g,
                                              tempPlayerScoreText.color.b,
                                              0f);  

        tempBestScoreText.color = new Color(tempPlayerScoreText.color.r,    
                                              tempPlayerScoreText.color.g,
                                              tempPlayerScoreText.color.b,
                                              0f); 

        Image tutorialImageRef = tutorialImageParameters[0].image;
        tutorialImageParameters[0].image.color = new Color(tutorialImageRef.color.r, 
                                                           tutorialImageRef.color.g, 
                                                           tutorialImageRef.color.b, 
                                                           0f); 


        tempPlayerScoreText.fontSize = 50; 
        tempBestScoreText.fontSize = 50;
    }
}


