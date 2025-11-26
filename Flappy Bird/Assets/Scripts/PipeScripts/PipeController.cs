using System.Collections;
using UnityEngine;

public class PipeController : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    [HideInInspector] public bool isPlaying;
    private bool isFinished;

    [Header("Pipes References")]
    [SerializeField] private GameObject topPipe;
    [SerializeField] private GameObject bottomPipe;

    private GameObject temporaryBottomPipe;

    [Header("Pipe Movement Variables")]
    public float moveSpeed;
    
    void Update()
    {
        isPlaying = gameController.gameIsPlaying;
        isFinished = gameController.gameIsFinished;
    }

    private void SpawnPipe()
    {
        // cr�e le spawn point du top pipe
        Vector3 temporarySpawnPoint = new Vector3(10f, Random.Range(1f, -3f), 0f);

        // spawn the top pipe
        temporaryBottomPipe = Instantiate(bottomPipe, temporarySpawnPoint, Quaternion.identity);

        // get le spawn point du bottom pipe
        float yDifference = temporaryBottomPipe.transform.position.y + Random.Range(2.5f, 3.5f);
        temporarySpawnPoint = new Vector3(10f, yDifference, 0f);

        // spawn the bottom pipe 
        Instantiate(topPipe, temporarySpawnPoint, Quaternion.identity);
    }


    public void StartWaitForSpawn()
    {
        // gameController.pipeSpawningCounter++;

        StartCoroutine(WaitForSpawn());
    }

    public IEnumerator WaitForSpawn()
    {
        if (!isFinished) SpawnPipe();

        yield return new WaitForSeconds(2f);
        if (!isFinished) yield return StartCoroutine(WaitForSpawn());
    }
}
