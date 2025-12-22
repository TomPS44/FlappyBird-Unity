using System.Collections;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    private GameManager gameManager;

    [Header("Lerp Collections")]
    [SerializeField] private CustomCollection[] flappyImageParameters = new CustomCollection[2];
    [SerializeField] private CustomCollection[] birdImageParameters = new CustomCollection[2];
    [SerializeField] private CustomCollection[] startButtonParameters = new CustomCollection[2];


    [Header("Variables")]
    [SerializeField] private int sineMovementFactor;
    public bool canStart;
    private bool isLoadingStarting;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        ResetUIs();
        canStart = false;
        isLoadingStarting = false;

        StartCoroutine(SpawnMenuElements());
        MoveUIs();
    }



    private void ResetUIs()
    {
        // GameObject objectToReset;

        flappyImageParameters[0].image.gameObject.SetActive(true);
        flappyImageParameters[0].image.gameObject.GetComponent<RectTransform>().anchoredPosition3D = flappyImageParameters[1].targetPos;

        birdImageParameters[0].image.gameObject.SetActive(true);
        birdImageParameters[0].image.gameObject.GetComponent<RectTransform>().anchoredPosition3D = birdImageParameters[1].targetPos;

        startButtonParameters[0].button.gameObject.SetActive(true);
        startButtonParameters[0].button.gameObject.GetComponent<RectTransform>().anchoredPosition3D = startButtonParameters[1].targetPos;

        // objectToReset = birdImageParameters[0].image.gameObject;
        // objectToReset.SetActive(true);
        // objectToReset.GetComponent<RectTransform>().anchoredPosition3D = birdImageParameters[1].targetPos;

        // objectToReset = startButtonParameters[0].button.gameObject;
        // objectToReset.SetActive(true);
        // objectToReset.GetComponent<RectTransform>().anchoredPosition3D = startButtonParameters[1].targetPos;
    }

    private IEnumerator SpawnMenuElements()
    {
        // // ChangeSinePosition(flappyImageParameters[0].targetPos);
        // // ChangeSinePosition(birdImageParameters[0].targetPos);

        
        UIHelper.CallCustomLerp(flappyImageParameters[0].image, ChangeSinePosition(flappyImageParameters[0].targetPos), flappyImageParameters[0].speed, flappyImageParameters[0].animCurve);

        yield return new WaitForSeconds(0.25f);

        UIHelper.CallCustomLerp(birdImageParameters[0].image, ChangeSinePosition(birdImageParameters[0].targetPos), birdImageParameters[0].speed, flappyImageParameters[0].animCurve);


        yield return new WaitForSeconds(0.75f);

        UIHelper.CallCustomLerp(startButtonParameters[0].button, ChangeSinePosition(startButtonParameters[0].targetPos), startButtonParameters[0].speed, startButtonParameters[0].animCurve);

        // UIHelper.CallCustomLerp(flappyImageParameters[0]);
        // UIHelper.CallCustomLerp(birdImageParameters[0]);

        yield return new WaitForSeconds(1f);

        canStart = true;
    }

    private IEnumerator MoveWithSine(GameObject objectToMove, float moveFactor, bool isBackwards)
    {
        while (!isLoadingStarting)
        {
            float currentTime = Time.time;
            Vector3 targetPos = objectToMove.transform.position;

            if (isBackwards)
            {
                targetPos.y += Mathf.Sin(currentTime) * Time.deltaTime * moveFactor;
            }
            else
            {
                targetPos.y -= Mathf.Sin(currentTime) * Time.deltaTime * moveFactor;
            }
            

            objectToMove.transform.position = targetPos;

            yield return null;
        }
    }

    private void MoveUIs()
    {
        StartCoroutine(MoveWithSine(flappyImageParameters[0].image.gameObject,sineMovementFactor, true));
        StartCoroutine(MoveWithSine(birdImageParameters[0].image.gameObject, sineMovementFactor, false));
        StartCoroutine(MoveWithSine(startButtonParameters[0].button.gameObject, 15f, false));
        
    }

    private Vector3 ChangeSinePosition(Vector3 initialTargetPos)
    {
        Vector3 trueTargetPos = initialTargetPos;

        trueTargetPos.y += Mathf.Sin(Time.time) * Time.deltaTime * sineMovementFactor;

        return trueTargetPos;
    }
}
