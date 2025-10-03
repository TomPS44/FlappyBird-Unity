using UnityEngine;

[CreateAssetMenu(fileName = "UIElementScriptable", menuName = "Scriptable Objects/UIElement")]
public class UIElementScriptable : ScriptableObject
{
    public Vector3 hiddenPosition;
    public Vector3 showedPosition;

    public AnimationCurve animCurve;

    public bool isInPosition;

    public float speed;
}
