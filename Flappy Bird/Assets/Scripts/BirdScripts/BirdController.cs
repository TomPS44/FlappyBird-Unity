using UnityEngine;

public class BirdController : MonoBehaviour
{
    [SerializeField] private GameObject scripts;
    private GameController gameController;
    [SerializeField] private Animator animator;

    void Start()
    {
        scripts = GameObject.FindWithTag("Scripts");
        gameController = scripts.GetComponent<GameController>();
        animator = gameController.bird.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Death"))
        {
            gameController.gameIsFinished = true;
            // animator.SetBool("isFinished", true);

            gameController.OnPlayerLose?.Invoke();
        }
    }
}
