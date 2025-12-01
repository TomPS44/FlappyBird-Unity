using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class BirdMovement : MonoBehaviour
{
    [Header("Other Scripts References")]
    [SerializeField] private GameController gameController; 
    private bool isPlaying;
    private bool isFinished;

    [HideInInspector] public GameObject bird;
    [HideInInspector] public Rigidbody2D birdRB;

    [Header("IdleLerp")]
    [SerializeField] private AnimationCurve curve;

    private Vector3 startVector;
    private Vector3 endVector;

    private float speed = 0.5f;
    private float current, target;

    [Header("Bird Movement")]
    public float flapForce;
    [SerializeField] private float gravityForce;

    public Animator animator;
    

    void Start()
    {
        startVector = new Vector3(0f, 0.25f, 0f);
        endVector = new Vector3(0f, -0.25f, 0f);
    }

    
    void Update()
    {
        // -----------------------------------------------
        //        handles constant update variables
        // -----------------------------------------------

        isPlaying = gameController.gameIsPlaying;
        isFinished = gameController.gameIsFinished;
        if (isPlaying) birdRB.gravityScale = gravityForce;



        // -----------------------------------------------


        // -----------------------------------------------
        //             handles start idle
        // -----------------------------------------------
        

        current = Mathf.MoveTowards(current, target, speed * Time.deltaTime);

        if (!isPlaying && !isFinished)
        {
            if (bird.transform.position == startVector) target = 1f;

            if (bird.transform.position == endVector) target = 0f;
        }

        if (!isPlaying && !isFinished)
        {
            bird.transform.position = Vector3.Lerp(startVector,
                                                   endVector,
                                                   curve.Evaluate(current));
        }

        if (isFinished && isPlaying)
        {
            animator.speed = 0f;
        }
        if (!isFinished)
        {
            animator.speed = 1f;
        }

        // -----------------------------------------------



        // -----------------------------------------------
        //       handles player's input and movement
        // -----------------------------------------------


        if (isPlaying && Input.GetMouseButtonDown(0) && !isFinished)
            birdRB.linearVelocity = new Vector3(0f, flapForce, 0f);

        // -----------------------------------------------


    }
}
