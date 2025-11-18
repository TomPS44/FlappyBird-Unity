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

    public static  IEnumerator Fade(TextMeshProUGUI textToFade, float speed)
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

    public static void CallFadeToTransparent(GameObject gameObjectToFade, float speed, bool isChildren)
    {
        UIHelper.Instance.StartCoroutine(UIHelper.Instance.FadeToApparent(gameObjectToFade, speed, isChildren));
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

    public static IEnumerator CustomIncreaseScore(TextMeshProUGUI scoreText, int scoreToDisplay)
    {
        float current = 0;

        while (Convert.ToInt32(scoreText.text) < scoreToDisplay)
        {
            current = Mathf.MoveTowards(current, (float)scoreToDisplay, (scoreToDisplay / 3f) * Time.deltaTime);

            UIHelper.DisplayTemporaryScore(scoreText, (int)current);

            yield return null;
        }

        yield break;
    }



    public static void DisplayTemporaryScore(TextMeshProUGUI scoreText, int playerScore)
    {
        string scoreString;

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
