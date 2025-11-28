using System;
using System.Collections;
using System.Collections.Generic;
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

    [Header("GameObjects")]
    [SerializeField] private Image[] medals = new Image[4];
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

    [Space]

    [SerializeField] private GameObject tempPlayerScore;
    private TextMeshProUGUI tempPlayerScoreText;

    [HideInInspector] public bool fadePipes;


    private void Start()
    {
        fadePipes = false;

        gameController.OnPlayerLose += StartSpawnLossScreen;


        tempPlayerScoreText = tempPlayerScore.GetComponent<TextMeshProUGUI>();
        // tempPlayerScoreText.color = new Color(tempPlayerScoreText.color.r,    
        //                                       tempPlayerScoreText.color.g,
        //                                       tempPlayerScoreText.color.b,
        //                                       0f); 
        
        ResetTempTexts();
        ResetUIs();  
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

        yield return StartCoroutine(SpawnMedal());

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
        yield return StartCoroutine(ResetMedal());

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

    public IEnumerator SpawnMedal()
    {
        Image medal;

        // assigns an image to the medal, depending on the score (if statements going from gold medal to no medal)
        if (scoreHandler.playerScore >= 50)
        {
            medal = medals[0];
        }
        else if (scoreHandler.playerScore >= 35)
        {
            medal = medals[1];
        }
        else if (scoreHandler.playerScore >= 20)
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

        yield return new WaitForSeconds(1f);
    }
    public IEnumerator ResetMedal()
    {
        UIHelper.CallCustomLerp(medalsParameters[1]);

        yield return new WaitForSeconds(scoreHandler.playerScore > 20 ? 1f : 0f);
    }

    public void ResetUIs()
    {
        // reset the position of every UI element (  so that I don't have to do it myself :)  )
        gameParameters[0].image.rectTransform.anchoredPosition3D = gameParameters[1].targetPos;
        overParameters[0].image.rectTransform.anchoredPosition3D = overParameters[1].targetPos;
        mainFrameParameters[0].image.rectTransform.anchoredPosition3D = mainFrameParameters[1].targetPos;
        playAgainbuttonParameters[0].image.rectTransform.anchoredPosition3D = playAgainbuttonParameters[1].targetPos;
        mainFrameParameters[0].image.rectTransform.anchoredPosition3D = mainFrameParameters[1].targetPos;

        // reset also the medals, but the code is a bit longer since there are three 
        for (int i = 0; i < medals.Length; i++)
        {
            // sets the medal active ( in  case I forgot to do it :) )
            medals[i].gameObject.SetActive(true);
            // and assigns an image to the parameters of the medals (to not cause an error), to then reset the position
            medalsParameters[0].image = medals[i];
            medalsParameters[0].image.rectTransform.anchoredPosition3D = medalsParameters[1].targetPos;
        }
        
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


