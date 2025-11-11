using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
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
}


