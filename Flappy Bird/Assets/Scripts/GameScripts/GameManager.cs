using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject fadingScreen;
    private Canvas fadingScreenCanvas;
    private Image blackScreen;


    private bool isLoadingScene = false;

    void Awake() 
    {
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(fadingScreen);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        fadingScreenCanvas = fadingScreen.GetComponentInChildren<Canvas>();
        blackScreen = fadingScreenCanvas.GetComponentInChildren<Image>();

        // makes the black screen turn visible;
        blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, 1f);
        // and slownly makes it turn invisible and disable the block raycasts thing
        yield return StartCoroutine(UIHelper.Fade(blackScreen, 0.5f));
        blackScreen.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    private IEnumerator LoadScene(string sceneName)
    {
        isLoadingScene = true;

        yield return StartCoroutine(UIHelper.FadeToApparent(blackScreen, 1f));
        blackScreen.GetComponent<CanvasGroup>().blocksRaycasts = true;

        // yield return new WaitForSeconds(0.1f);

        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

        yield return new WaitForSeconds(0.2f);

        yield return StartCoroutine(UIHelper.Fade(blackScreen, 0.5f));
        blackScreen.GetComponent<CanvasGroup>().blocksRaycasts = false;

        isLoadingScene = false;
    }

    public void StartGame()
    {
        if (isLoadingScene) return;

        if (!GameObject.Find("MenuController").GetComponent<MainMenuController>().canStart) return;

        StartCoroutine(LoadScene("GameScene"));
    }
    public void LoadMainMenu()
    {
        if (isLoadingScene) return;

        StartCoroutine(LoadScene("MainMenuScene"));
    }


    
}
