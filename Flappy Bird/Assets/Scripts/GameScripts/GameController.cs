using System;
using UnityEditor.SearchService;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private PipeController pipeController;
    [SerializeField] private BirdMovement birdMovement;

    public AnimationClip animClip;

    [HideInInspector] public GameObject bird;
    private Animator animator;



    public bool gameIsPlaying;
    public bool gameIsFinished;


    public Action OnPlayerLose;


    public int playerScore;

    void Start()
    {
        gameIsPlaying = false;
        gameIsFinished = false;

        playerScore = 0;

        birdMovement.bird = Instantiate(bird, Vector3.zero, Quaternion.identity);
    }

    
    void Update()
    {
        if (!gameIsPlaying && Input.GetMouseButtonDown(0) && !gameIsFinished)
        {
            StartGame();
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
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
        // birdMovement.birdRB.freezeRotation = true;
        rb.linearVelocity = new Vector3(0f, birdMovement.flapForce, 0f);

        // if (pipeSpawningCounter > 0) return; 
        pipeController.StartWaitForSpawn();
    }

    public void RestartGame()
    {
        Destroy(birdMovement.bird);

        gameIsPlaying = false;
        gameIsFinished = false;

        birdMovement.bird = Instantiate(bird, Vector3.zero, Quaternion.identity);

        // animator = bird.GetComponentInChildren<Animator>();
        // animator.SetBool("isFinished", false);
    }
}
