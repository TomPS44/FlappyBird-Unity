using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class PipeController : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    [SerializeField] private ScoreHandler scoreHandler;
    [HideInInspector] public bool isPlaying;
    private bool isFinished;

    [Header("Pipes References")]
    [SerializeField] private GameObject topPipe;
    [SerializeField] private GameObject bottomPipe;

    private GameObject temporaryBottomPipe;

    [Header("Pipe Movement Variables")]
    public float moveSpeed;
    public float spawnSpeed;

    /*    same as the other comment below
    private int amount = 0;
    */

    void Start()
    {
        moveSpeed = 3f;
        spawnSpeed = 2f;
    }

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
        float yDifference = temporaryBottomPipe.transform.position.y + GetYPosPipe();
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

        yield return new WaitForSeconds(spawnSpeed);
        if (!isFinished) yield return StartCoroutine(WaitForSpawn());
    }


    public void IncreaseDifficulty()
    {
        moveSpeed += 0.5f * 1f/3f;
        spawnSpeed -= 0.3f * 1f/3f;

        /*   all that was jun a fun litle thing to do, it's useless :)
        amount++;

        string eeeeeee = "e";
        for (int i = 0; i < amount; i++)
        {
            eeeeeee += "e";   
        }

        Debug.Log($"Increasing sp{eeeeeee}d !!!!!!!!!!!");
        */
    }

    public void ResetSpeeds()
    {
        moveSpeed = 3f;
        spawnSpeed = 2f;
    }

    private float GetYPosPipe()
    {
        // return a random value between 2.25f and 3.5f, depending on the player score

        float minValue = 2.25f;
        float maxValue = 3.5f;
        int playerScore = scoreHandler.playerScore;

        // float minValueToClamp = minValue + 2f ;
        float maxValueToClamp = Mathf.Clamp(maxValue - playerScore * 0.005f + Random.Range(0f, 0.1f), 3f, maxValue);

        float offsetY = Random.Range(minValue, maxValueToClamp);

        Debug.Log(offsetY);

        return offsetY;
    }
}
