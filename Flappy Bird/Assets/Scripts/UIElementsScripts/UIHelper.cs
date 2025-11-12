using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Unity.VisualScripting;
using System;

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

    public static void CallFade(GameObject gameObjectToFade, float speed, bool isChildren)
    {
        UIHelper.Instance.StartCoroutine(UIHelper.Instance.Fade(gameObjectToFade, speed, isChildren));
    }

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
}
