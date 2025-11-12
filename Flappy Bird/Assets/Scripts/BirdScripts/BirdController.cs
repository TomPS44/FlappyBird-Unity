using UnityEngine;

public class BirdController : MonoBehaviour
{
    [SerializeField] private GameObject scripts;
    private GameController gameController;
    private UIController UIController;
    [SerializeField] private Animator animator;

    void Start()
    {
        scripts = GameObject.FindWithTag("Scripts");
        gameController = scripts.GetComponent<GameController>();
        UIController = UIController = GameObject.Find("UIScripts").GetComponent<UIController>();
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
            gameController.gameIsFinished = true;
            // animator.SetBool("isFinished", true);

            gameController.OnPlayerLose?.Invoke();
        }
    }
}
