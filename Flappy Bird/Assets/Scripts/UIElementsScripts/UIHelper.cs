using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Unity.VisualScripting;
using System;
using TMPro;
using UnityEngineInternal;

public class UIHelper : MonoBehaviour
{
    public static UIHelper Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Plus d'un instance pas bien :( :(,   /!\\");
            return;
        }

        Instance = this;
    }

    #region LerpFunctions


    // -------------------------------------------------------------------------------
    //                            Lerp functions for images
    // -------------------------------------------------------------------------------


    public static void CallCustomLerp(CustomCollection parameters)
    {
        switch (parameters.collectionType)
        {
            case CustomCollection.CollectionType.OnlyImage: // Calls custom lerp si CustomCollections est OnlyImage
                Instance.StartCoroutine(Instance.CustomLerp(parameters.image, parameters.targetPos, parameters.speed));
                break;
            case CustomCollection.CollectionType.ImagePlusCurve: // Calls custom lerp si CustomCollections est ImagePlusCurve
                Instance.StartCoroutine(Instance.CustomLerp(parameters.image, parameters.targetPos, parameters.speed, parameters.animCurve));
                break;
            case CustomCollection.CollectionType.OnlyButton: // Calls custom lerp si CustomCollections est OnlyButton
                Instance.StartCoroutine(Instance.CustomLerp(parameters.button, parameters.targetPos, parameters.speed));
                break;
            case CustomCollection.CollectionType.ButtonPlusCurve: // Calls custom lerp si CustomCollections est ButtonPlusCurve
                Instance.StartCoroutine(Instance.CustomLerp(parameters.button, parameters.targetPos, parameters.speed, parameters.animCurve));
                break;

        }
    }



    /// <summary>
    /// Calls the coroutine CustomLerp, which lerp an image or a button just by specifying a few parameters
    /// </summary>
    /// <param name="image">The UI element which will be lerped</param>
    /// <param name="targetPos">The target position of the UI element</param>
    /// <param name="speed">The speed at which the UI element will move</param>
    public static void CallCustomLerp(Image image, Vector3 targetPos, float speed)
    {
        UIHelper.Instance.StartCoroutine(UIHelper.Instance.CustomLerp(image, targetPos, speed));
    }
    IEnumerator CustomLerp(Image image, Vector3 targetPos, float speed)
    {
        float current = 0;
        float target = 1;

        RectTransform rt = image.rectTransform;
        Vector3 startPos = rt.anchoredPosition3D;

        while (current < 1f)
        {
            current = Mathf.MoveTowards(current, target, speed * Time.deltaTime);

            rt.anchoredPosition3D = Vector3.Lerp(startPos, targetPos, current);

            yield return null;
        }

        rt.anchoredPosition3D = targetPos;
    }

    /// <summary>
    /// Calls the coroutine CustomLerp, which lerp an image or a button just by specifying a few parameters
    /// </summary>
    /// <param name="image">The UI element which will be lerped</param>
    /// <param name="targetPos">The target position of the UI element</param>
    /// <param name="speed">The speed at which the UI element will move</param>
    /// <param name="curve">The AnimationCurve which will affect the speed</param>
    public static void CallCustomLerp(Image image, Vector3 targetPos, float speed, AnimationCurve curve)
    {
        UIHelper.Instance.StartCoroutine(UIHelper.Instance.CustomLerp(image, targetPos, speed, curve));
    }
    IEnumerator CustomLerp(Image image, Vector3 targetPos, float speed, AnimationCurve curve)
    {
        float current = 0;
        float target = 1;

        RectTransform rt = image.rectTransform;
        Vector3 startPos = rt.anchoredPosition3D;

        while (current < 1f)
        {
            current = Mathf.MoveTowards(current, target, speed * Time.deltaTime);

            rt.anchoredPosition3D = Vector3.Lerp(startPos, targetPos, curve.Evaluate(current));

            yield return null;
        }

        rt.anchoredPosition3D = targetPos;
    }


    // -------------------------------------------------------------------------------
    //                         Lerp functions for buttons
    // -------------------------------------------------------------------------------


    /// <summary>
    /// Calls the coroutine CustomLerp, which lerp an image or a button just by specifying a few parameters
    /// </summary>
    /// <param name="image">The UI element which will be lerped</param>
    /// <param name="targetPos">The target position of the UI element</param>
    /// <param name="speed">The speed at which the UI element will move</param>
    public static void CallCustomLerp(Button button, Vector3 targetPos, float speed)
    {
        UIHelper.Instance.StartCoroutine(UIHelper.Instance.CustomLerp(button, targetPos, speed));
    }
    IEnumerator CustomLerp(Button button, Vector3 targetPos, float speed)
    {
        float current = 0;
        float target = 1;

        RectTransform rt = button.GetComponent<RectTransform>();
        Vector3 startPos = rt.anchoredPosition3D;

        while (current < 1f)
        {
            current = Mathf.MoveTowards(current, target, speed * Time.deltaTime);

            rt.anchoredPosition3D = Vector3.Lerp(startPos, targetPos, current);

            yield return null;
        }

        rt.anchoredPosition3D = targetPos;
    }

    /// <summary>
    /// Calls the coroutine CustomLerp, which lerp an Image or a button just by specifying a few parameters
    /// </summary>
    /// <param name="image">The UI element which will be lerped</param>
    /// <param name="targetPos">The target position of the UI element</param>
    /// <param name="speed">The speed at which the UI element will move</param>
    /// <param name="curve">The AnimationCurve which will affect the speed</param>
    public static void CallCustomLerp(Button button, Vector3 targetPos, float speed, AnimationCurve curve)
    {
        UIHelper.Instance.StartCoroutine(UIHelper.Instance.CustomLerp(button, targetPos, speed, curve));
    }
    IEnumerator CustomLerp(Button button, Vector3 targetPos, float speed, AnimationCurve curve)
    {
        float current = 0;
        float target = 1;

        RectTransform rt = button.GetComponent<RectTransform>();
        Vector3 startPos = rt.anchoredPosition3D;

        while (current < 1f)
        {
            current = Mathf.MoveTowards(current, target, speed * Time.deltaTime);

            rt.anchoredPosition3D = Vector3.Lerp(startPos, targetPos, curve.Evaluate(current));

            yield return null;
        }

        rt.anchoredPosition3D = targetPos;
    }

    #endregion

    #region FadeFunctions

    // -------------------------------------------------------------------------------
    //                             Fade functions 
    // --------------------------------------------------------------------------------

    public static void CallFade(GameObject gameObjectToFade, float speed, bool isChildren)
    {
        UIHelper.Instance.StartCoroutine(UIHelper.Instance.Fade(gameObjectToFade, speed, isChildren));
    }
    public static void CallFade(Image imageToFade, float speed)
    {
        UIHelper.Instance.StartCoroutine(UIHelper.Fade(imageToFade, speed));
    }
    /*
    public static void CallFade(TextMeshProUGUI textToFade, float speed)
    {
        UIHelper.Instance.StartCoroutine(UIHelper.Instance.Fade(textToFade, speed));
    }
    */

    private IEnumerator Fade(GameObject gameObjectToFade, float speed, bool isChildren)
    {
        float current = 1f;
        float target = 0f;

        SpriteRenderer sp = isChildren ? gameObjectToFade.GetComponentInChildren<SpriteRenderer>() : gameObjectToFade.GetComponent<SpriteRenderer>();

        // sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 1f);

        while (current > 0.05f)
        {
            current = Mathf.MoveTowards(current, target, speed * Time.deltaTime);

            sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, current);


            yield return null;
        }

        yield break;
    }
    public static IEnumerator Fade(Image imageToFade, float speed)
    {
        float current = 1f;
        float target = 0f;

        // sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 1f);

        while (current > 0.05f)
        {
            current = Mathf.MoveTowards(current, target, speed * Time.deltaTime);

            imageToFade.color = new Color(imageToFade.color.r, imageToFade.color.g, imageToFade.color.b, current);


            yield return null;
        }

        yield break;
    }

    public static IEnumerator Fade(TextMeshProUGUI textToFade, float speed)
    {
        float current = 1f;
        float target = 0f;

        // sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 1f);

        while (current > 0.01f)
        {
            current = Mathf.MoveTowards(current, target, speed * Time.deltaTime);

            textToFade.color = new Color(textToFade.color.r, textToFade.color.g, textToFade.color.b, current);


            yield return null;
        }

        yield break;
    }

    public static void CallFadeToApparent(GameObject gameObjectToFade, float speed, bool isChildren)
    {
        UIHelper.Instance.StartCoroutine(UIHelper.Instance.FadeToApparent(gameObjectToFade, speed, isChildren));
    }
    
    public static void CallFadeToApparent(Image imageToFade, float speed)
    {
        UIHelper.Instance.StartCoroutine(UIHelper.FadeToApparent(imageToFade, speed));
    }
    
    /*
    public static void CallFadeToTransparent(TextMeshProUGUI textToFade, float speed)
    {
        UIHelper.Instance.StartCoroutine(UIHelper.Instance.FadeToApparent(textToFade, speed));
    }
    */

    private IEnumerator FadeToApparent(GameObject gameObjectToFade, float speed, bool isChildren)
    {
        float current = 0f;
        float target = 1f;

        SpriteRenderer sp = isChildren ? gameObjectToFade.GetComponentInChildren<SpriteRenderer>() : gameObjectToFade.GetComponent<SpriteRenderer>();

        // sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 1f);

        while (current < 0.95f)
        {
            current = Mathf.MoveTowards(current, target, speed * Time.deltaTime);

            sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, current);


            yield return null;
        }

        yield break;
    }
    public static IEnumerator FadeToApparent(Image imageToFade, float speed)
    {
        float current = 0f;
        float target = 1f;

        // sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 1f);

        while (current < 0.95f)
        {
            current = Mathf.MoveTowards(current, target, speed * Time.deltaTime);

            imageToFade.color = new Color(imageToFade.color.r, imageToFade.color.g, imageToFade.color.b, current);


            yield return null;
        }

        yield break;
    }
    public static IEnumerator FadeToApparent(TextMeshProUGUI textToFade, float speed)
    {
        float current = 0f;
        float target = 1f;

        // sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 1f);

        while (current < 0.99f)
        {
            current = Mathf.MoveTowards(current, target, speed * Time.deltaTime);

            textToFade.color = new Color(textToFade.color.r, textToFade.color.g, textToFade.color.b, current);


            yield return null;
        }

        yield break;
    }


    #endregion

    #region ScoreFunctions

    // public static IEnumerator CustomIncreaseScore(TextMeshProUGUI scoreText, int scoreToDisplay)
    // {
    //     float current = 0;

    //     while (Convert.ToInt32(scoreText.text) < scoreToDisplay)
    //     {
    //         current = Mathf.Lerp(current, scoreToDisplay, scoreToDisplay * (scoreToDisplay / 2f) * Time.deltaTime);

    //         UIHelper.DisplayTemporaryScore(scoreText, (int)current);

    //         yield return null;
    //     }

    //     yield break;
    // }

    public static IEnumerator CustomIncreaseScore(TextMeshProUGUI scoreText, int scoreToDisplay)
    {
        float elapsed = 0f;
        float startScore = 0f;

        // Durée adaptative
        float baseDuration = 0.5f;
        float durationPerPoint = 0.02f;
        float duration = Mathf.Clamp(baseDuration + scoreToDisplay * durationPerPoint, baseDuration, 4.5f);

        AnimationCurve curve = new AnimationCurve(
            new Keyframe(0f, 0f, 2f, 2f),      
            new Keyframe(0.6f, 0.85f, 0f, 0f), 
            new Keyframe(0.75f, 0.92f, 0f, 0f), 
            new Keyframe(0.85f, 0.96f, 0f, 0f),
            new Keyframe(0.92f, 0.98f, 0f, 0f),
            new Keyframe(0.97f, 0.995f, 0f, 0f),
            new Keyframe(1f, 1f, 0f, 0f)        
        );

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            float curveValue = curve.Evaluate(t);

            int currentScore = Mathf.RoundToInt(Mathf.Lerp(startScore, scoreToDisplay, curveValue));
            DisplayTemporaryScore(scoreText, currentScore);  /*scoreText.text = currentScore.ToString();*/ 

            yield return null;
        }

        // S'assurer que le score final est bien affiché
        DisplayTemporaryScore(scoreText, scoreToDisplay);
    }


    public static void DisplayTemporaryScore(TextMeshProUGUI scoreText, int playerScore)
    {
        string scoreString;

        scoreText.fontSize = playerScore > 999 ? 40f : 50f;

        if (playerScore > 99)
        {
            scoreString = playerScore.ToString();
        }
        else if (playerScore > 9)
        {
            scoreString = "0" + playerScore.ToString();
        }
        else
        {
            scoreString = "00" + playerScore.ToString();
        }

        scoreText.text = scoreString;
    }




    #endregion


}
