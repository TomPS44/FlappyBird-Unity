using System.Collections;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private CustomCollection[] flappyImageParameters = new CustomCollection[2];
    [SerializeField] private CustomCollection[] birdImageParameters = new CustomCollection[2];
    [SerializeField] private CustomCollection[] startButtonParameters = new CustomCollection[2];

    private bool canStart;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ResetUIs()
    {
        
    }

    private IEnumerator SpawnFlappyBirdImages()
    {
        UIHelper.CallCustomLerp(flappyImageParameters[0]);
        UIHelper.CallCustomLerp(birdImageParameters[0]);

        yield return new WaitForSeconds(1f);

        canStart = true;
    }
}
