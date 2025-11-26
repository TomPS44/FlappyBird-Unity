using UnityEngine;

public class BirdController : MonoBehaviour
{
    [SerializeField] private GameObject scripts;
    private GameController gameController;
    private UIController UIController;
    private ScoreHandler scoreHandler;
    [SerializeField] private Animator animator;

    void Start()
    {
        scripts = GameObject.FindWithTag("Scripts");
        gameController = scripts.GetComponent<GameController>();
        UIController = GameObject.Find("UIScripts").GetComponent<UIController>();
        scoreHandler = GameObject.Find("UIScripts").GetComponent<ScoreHandler>();
        animator = gameController.bird.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (UIController.fadePipes)
        {
            UIHelper.CallFade(this.gameObject, 0.5f, true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Death"))
        {
            if (gameController.gameIsFinished) return;
            
            gameController.gameIsFinished = true;
            // animator.SetBool("isFinished", true);

            scoreHandler.FadeScore();

            gameController.OnPlayerLose?.Invoke();
        }

        if (collision.CompareTag("Score"))
        {
            scoreHandler.AddScore();
            scoreHandler.DisplayScore();
        }
    }
}
