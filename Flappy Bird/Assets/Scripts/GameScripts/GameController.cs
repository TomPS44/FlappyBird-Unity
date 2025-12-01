using System;
using UnityEditor.SearchService;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private PipeController pipeController;
    [SerializeField] private BirdMovement birdMovement;
    [SerializeField] private UIController uiController;  
    [SerializeField] private ScoreHandler scoreHandler;
    // [SerializeField] private BackgroundHandler backgroundHandler;


    public AnimationClip animClip;

    [HideInInspector] public GameObject bird;
    private Animator animator;



    public bool gameIsPlaying;
    public bool gameIsFinished;


    public Action OnPlayerLose;


    void Start()
    {
        gameIsPlaying = false;
        gameIsFinished = false;

        birdMovement.bird = Instantiate(bird, Vector3.zero, Quaternion.identity);
        birdMovement.animator = birdMovement.bird.GetComponentInChildren<Animator>();

        
        scoreHandler.ResetScore();

        uiController.ResetTempTexts();
        uiController.ResetUIs();

        scoreHandler.scoreText.text = "";

        uiController.SpawnIdleImages();
        uiController.MoveWithSineIdleImages();
        
    }

    
    void Update()
    {
        if (!gameIsPlaying && Input.GetMouseButtonDown(0) && !gameIsFinished)
        {
            uiController.StartCoroutine(uiController.DespawnIdleImages());
            StartGame();
        }
    }

    public void StartGame()
    {
        gameIsPlaying = true;
        gameIsFinished = false;

        // ajoute le rb � l'oiseau et l'assigne � une valeur
        birdMovement.bird.AddComponent<Rigidbody2D>();
        Rigidbody2D rb = birdMovement.bird.GetComponent<Rigidbody2D>();

        // assigne birdRB de birdMovement au rb ci-dessus et flap
        birdMovement.birdRB = rb;
        rb.linearVelocity = new Vector3(0f, birdMovement.flapForce, 0f);

        // if (pipeSpawningCounter > 0) return; 
        pipeController.StartWaitForSpawn();

        scoreHandler.DisplayScore();
    }

    public void RestartGame()
    {
        Destroy(birdMovement.bird);

        gameIsPlaying = false;
        gameIsFinished = false;

        

        birdMovement.bird = Instantiate(bird, Vector3.zero, Quaternion.identity);
        birdMovement.animator = birdMovement.bird.GetComponentInChildren<Animator>();

        scoreHandler.ResetScore();

        uiController.ResetTempTexts();
        uiController.ResetUIs();

        scoreHandler.scoreText.text = "";

        uiController.SpawnIdleImages();
        uiController.MoveWithSineIdleImages();
        
        // backgroundHandler.ResetBackgrounds();

        // animator = bird.GetComponentInChildren<Animator>();
        // animator.SetBool("isFinished", false);
    }
}
