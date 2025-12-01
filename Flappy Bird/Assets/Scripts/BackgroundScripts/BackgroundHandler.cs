using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BackgroundHandler : MonoBehaviour
{
    private GameController gameController;
    private GameObject scripts;

    [SerializeField] private PipeController pipeController;
    [SerializeField] private GameObject[] backgrounds = new GameObject[4];
    private GameObject[] backgrounds2 = new GameObject[4];


    private GameObject[] movingBackgrounds;
    private GameObject[] trailingBackgrounds;
    // private Vector3[] initialPositions;



    void Start()
    {
        scripts = GameObject.FindWithTag("Scripts");
        gameController = scripts.GetComponent<GameController>(); 

        // SetInitialPositions();
        InstantiateSecondBackgrounds();

        movingBackgrounds = backgrounds;
        trailingBackgrounds = backgrounds2;
    }

    void Update()
    {
        if (!gameController.gameIsPlaying || gameController.gameIsFinished) return;

        SetBackgroundArrays();
        SetTrailingBackgroundPosition(movingBackgrounds, trailingBackgrounds);
        MoveBackgrounds();
    }

    private void MoveBackgrounds()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            GameObject background = movingBackgrounds[i];

            float zPosition = background.transform.position.z <= 0 ? 1 : background.transform.position.z; 
            float spaceMoved = pipeController.moveSpeed * Time.deltaTime / zPosition;

            Vector3 targetPos = new Vector3(background.transform.position.x - spaceMoved, 
                                            background.transform.position.y, 
                                            background.transform.position.z);

            background.transform.position = Vector3.MoveTowards(background.transform.position, 
                                                                targetPos,
                                                                spaceMoved);

            /*
            movingBackgrounds[i].transform.position = new Vector3(RoundTo(movingBackgrounds[i].transform.position.x, 1),
                                                                  movingBackgrounds[i].transform.position.y,
                                                                  movingBackgrounds[i].transform.position.z);
            */

        }
    }

    private void SetTrailingBackgroundPosition(GameObject[] movingBackgrounds, GameObject[] trailingBackgrounds)
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            GameObject correspondingBackground = movingBackgrounds[i];
            SpriteRenderer backgroundSP = correspondingBackground.GetComponent<SpriteRenderer>();

            float backgroundWidth = backgroundSP.sprite.rect.width / backgroundSP.sprite.pixelsPerUnit; //backgroundSP.bounds.size.x;

            // float distanceToAdd = Mathf.Abs(backgroundSP.bounds.extents.x - correspondingBackground.transform.position.x) * 2;
            float xPosition = correspondingBackground.transform.position.x + backgroundWidth;

            Vector3 targetPos = new Vector3(/*-xPosition - distanceToAdd * 2*/ xPosition,
                                            correspondingBackground.transform.position.y, 
                                            correspondingBackground.transform.position.z);  

            trailingBackgrounds[i].transform.position = targetPos;      
        }
    }

    private void InstantiateSecondBackgrounds()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            /*
            Vector3 spawnPos = new Vector3(backgrounds[i].transform.position.x, 
                                           backgrounds[i].transform.position.y,
                                           backgrounds[i].transform.position.z);
            */

            backgrounds2[i] = Instantiate(backgrounds[i], backgrounds[i].transform.position, Quaternion.identity);
        }
    }

    private void SetBackgroundArrays()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            SpriteRenderer sp = movingBackgrounds[i].GetComponent<SpriteRenderer>();
        
            if (movingBackgrounds[i].transform.position.x < -20f)
            {
                GameObject tempObject = movingBackgrounds[i];

                tempObject.transform.position = new Vector3(20f, tempObject.transform.position.y, tempObject.transform.position.z);

                movingBackgrounds[i] = trailingBackgrounds[i];
                trailingBackgrounds[i] = tempObject;
            }
        }
    }

    /*
    private void SetInitialPositions()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            initialPositions[i] = backgrounds[i].transform.position;
        }
    }
    

    public void ResetBackgrounds()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            movingBackgrounds[i].transform.position = initialPositions[i];
        }
    }
    */






    /*
    private float RoundTo(float value, int decimals)
    {
        float factor = Mathf.Pow(10, decimals);
        return Mathf.Round(value * factor) / factor;
    }
    */

}
