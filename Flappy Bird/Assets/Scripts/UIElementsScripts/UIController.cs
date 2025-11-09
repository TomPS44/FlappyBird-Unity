using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    [SerializeField] private Image[] medals = new Image[3];
    [SerializeField] private CustomCollection medalsParameters;

    [Space]

    [SerializeField] private Image[] gameOver = new Image[2];
    [SerializeField] private CustomCollection[] gameOverParameters = new CustomCollection[2];

    [Space]

    [SerializeField] private Image[] getReady = new Image[2];
    [SerializeField] private CustomCollection[] getReadyParameters = new CustomCollection[2];

    [Space]

    [SerializeField] private Image mainFrame;
    [SerializeField] private CustomCollection mainFrameParameters;

    [Space]

    [SerializeField] private Image imageNew;
    [SerializeField] private CustomCollection imageNewParameters;

    [Space]

    [SerializeField] private Button playAgain;
    [SerializeField] private CustomCollection playAgainParameters;

    [Space]

    [SerializeField] private Button mainMenu;
    [SerializeField] private CustomCollection mainMenuParameters;
}


