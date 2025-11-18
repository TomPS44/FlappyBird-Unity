using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CustomCollection
{
    public Image image;
    public Button button;
    public Vector3 targetPos;
    public float speed;
    public AnimationCurve animCurve;

    public CollectionType collectionType;


    public enum CollectionType
    {
        OnlyImage, ImagePlusCurve, OnlyButton, ButtonPlusCurve
    }


    // ---------------------------------------------------------------------
    //                         Constructors
    // ---------------------------------------------------------------------

    public CustomCollection(Image image, Vector3 targetPos, float speed)
    {
        this.image = image;
        this.button = null;
        this.targetPos = targetPos;
        this.speed = speed;
        this.animCurve = null;

        collectionType = CollectionType.OnlyImage;
    }

    public CustomCollection(Image image, Vector3 targetPos, float speed, AnimationCurve animCurve)
    {
        this.image = image;
        this.button = null;
        this.targetPos = targetPos;
        this.speed = speed;
        this.animCurve = animCurve;

        collectionType = CollectionType.ImagePlusCurve;
    }
    public CustomCollection(Button button, Vector3 targetPos, float speed)
    {
        this.image = null;
        this.button = button;
        this.targetPos = targetPos;
        this.speed = speed;
        this.animCurve = null;

        collectionType = CollectionType.OnlyButton;

    }
    public CustomCollection(Button button, Vector3 targetPos, float speed, AnimationCurve animCurve)
    {
        this.image = null;
        this.button = button;
        this.targetPos = targetPos;
        this.speed = speed;
        this.animCurve = animCurve;

        collectionType = CollectionType.ButtonPlusCurve;
    }


    // ---------------------------------------------------------------------
    //                         Functions
    // ---------------------------------------------------------------------





}


