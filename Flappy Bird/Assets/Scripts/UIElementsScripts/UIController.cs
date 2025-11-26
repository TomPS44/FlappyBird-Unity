using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static System.Runtime.CompilerServices.RuntimeHelpers;


public class UIController : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    [SerializeField] private ScoreHandler scoreHandler;


    [SerializeField] private Image[] medals = new Image[3];
    [SerializeField] private CustomCollection[] medalsParameters = new CustomCollection[2];

    [Space]

    [SerializeField] private CustomCollection[] gameParameters = new CustomCollection[2];
    [SerializeField] private CustomCollection[] overParameters = new CustomCollection[2];

    [Space]

    [SerializeField] private CustomCollection[] getParameters = new CustomCollection[2];
    [SerializeField] private CustomCollection[] readyParameters = new CustomCollection[2];

    [Space]

    [SerializeField] private CustomCollection[] mainFrameParameters = new CustomCollection[2];

    [Space]

    [SerializeField] private CustomCollection[] imageNewParameters = new CustomCollection[2];

    [Space]

    [SerializeField] private CustomCollection[] playAgainbuttonParameters = new CustomCollection[2];

    [Space]

    [SerializeField] private CustomCollection[] mainMenuButtonParameters = new CustomCollection[2];

    [SerializeField] private GameObject tempPlayerScore;
    private TextMeshProUGUI tempPlayerScoreText;

    public bool fadePipes;


    private void Start()
    {
        fadePipes = false;

        gameController.OnPlayerLose += StartSpawnLossScreen;


        tempPlayerScoreText = tempPlayerScore.GetComponent<TextMeshProUGUI>();
        tempPlayerScoreText.color = new Color(tempPlayerScoreText.color.r,    
                                              tempPlayerScoreText.color.g,
                                              tempPlayerScoreText.color.b,
                                              0f);   
    }

    /*
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(Test());
        }
    }
    

    private IEnumerator Test()
    {
        UIHelper.CallCustomLerp(gameParameters[0]);
        UIHelper.CallCustomLerp(overParameters[0]);

        yield return new WaitForSeconds(1f);

        UIHelper.CallCustomLerp(mainFrameParameters[0]);

        yield return new WaitForSeconds(2f);

        UIHelper.CallCustomLerp(mainFrameParameters[1]);    

        yield return new WaitForSeconds(1f);

        UIHelper.CallCustomLerp(gameParameters[1]);
        UIHelper.CallCustomLerp(overParameters[1]);

    }
    */



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

        yield return new WaitForSeconds(1.5f);

        UIHelper.CallCustomLerp(mainFrameParameters[0]);

        yield return new WaitForSeconds(1.5f);

        yield return StartCoroutine(UIHelper.FadeToApparent(tempPlayerScoreText, 0.75f));


        yield return StartCoroutine(UIHelper.CustomIncreaseScore(tempPlayerScoreText, scoreHandler.playerScore));
        // DO NOT FORGET TO DISPLAY THE SCORE
        // /!\ /!\ /!\ /!\ /!\

        UIHelper.CallCustomLerp(mainMenuButtonParameters[0]);

        yield return new WaitForSeconds(0.25f);

        UIHelper.CallCustomLerp(playAgainbuttonParameters[0]);
    }
    
    public IEnumerator DespawnLossScreen()
    {
        UIHelper.CallCustomLerp(gameParameters[1]);
        UIHelper.CallCustomLerp(overParameters[1]);

        yield return new WaitForSeconds(0.5f);

        UIHelper.CallCustomLerp(playAgainbuttonParameters[1]);
        UIHelper.CallCustomLerp(mainMenuButtonParameters[1]);

        yield return new WaitForSeconds(0.5f);

        yield return StartCoroutine(UIHelper.Fade(tempPlayerScoreText, 1f));

        tempPlayerScoreText.text = "000";

        // DO NOT FORGET TO RESET THE SCORE
        // /!\ /!\ /!\ /!\ /!\

        UIHelper.CallCustomLerp(mainFrameParameters[1]);

        fadePipes = true;
        yield return new WaitForSeconds(1f / 60f);
        fadePipes = false;

        yield return new WaitForSeconds(2f);

        gameController.RestartGame();
    }








    public void ResetTempTexts()
    {
        tempPlayerScoreText.color = new Color(tempPlayerScoreText.color.r,    
                                              tempPlayerScoreText.color.g,
                                              tempPlayerScoreText.color.b,
                                              0f);  

        tempPlayerScoreText.fontSize = 50; 
    }
}


