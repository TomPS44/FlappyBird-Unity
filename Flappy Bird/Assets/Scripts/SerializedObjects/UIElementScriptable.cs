using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

[CreateAssetMenu(fileName = "UIElementFlappyBird", menuName = "Scriptable Objects/UIElement")]
public class CustomUIElement : ScriptableObject
{
    public Image image;

    public Vector3 targetPos;
}
