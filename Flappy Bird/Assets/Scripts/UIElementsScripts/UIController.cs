using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    // public CustomCollection parameters;

    // [SerializeField] private GameController gameController;


    [SerializeField] private Image[] medals = new Image[3];
    public CustomCollection customCollection;

    /*

    [SerializeField] private Image[] gameOver = new Image[2];
    [SerializeField] private Image[] getReady = new Image[2];

    [SerializeField] private Image mainFrame;
    [SerializeField] private Image imageNew;
    [SerializeField] private Button playAgain;
    [SerializeField] private Button mainMenu;


    private void Start()
    {
        parameters = new CustomCollection(medals[2], new Vector3(0f, 1f, 2f), 4.5f);

        UIHelper.CallCustomLerp(parameters);
    }

    */

    public void Test()
    {
        UIHelper.CallCustomLerp(customCollection);
    }

}


