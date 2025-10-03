using UnityEngine;

public class PipeMovement : MonoBehaviour
{
    private GameController gameController;
    private PipeController pipeController;
    [SerializeField] private GameObject scripts;
    private float moveSpeed;
    private bool isPlaying;
    private bool isFinished;

    private void Start()
    {
        scripts = GameObject.FindWithTag("Scripts");
        pipeController = scripts.GetComponent<PipeController>();
        gameController = scripts.GetComponent<GameController>();
    }

    void Update()
    {
        moveSpeed = pipeController.moveSpeed;
        isPlaying = gameController.gameIsPlaying;
        isFinished = gameController.gameIsFinished;

        MovePipe();

        DestroyPipe();
    }

    private void MovePipe()
    {
        if (!isPlaying || isFinished) return;

        Vector3 target = new Vector3(this.transform.position.x - (moveSpeed * Time.deltaTime),
                                         this.transform.position.y,
                                         0f);

        this.transform.position = Vector3.MoveTowards(this.transform.position, target, moveSpeed * Time.deltaTime);
    }

    private void DestroyPipe()
    {
        isPlaying = gameController.gameIsPlaying;

        if (this.transform.position.x < -12.5f || !isPlaying)
        {
            Destroy(this.gameObject);
        }
    }

}
