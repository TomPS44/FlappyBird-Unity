using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameController))]
public class RestartGameEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GameController controller = (GameController)target;

        if (GUILayout.Button("RestartGame"))
        {
            controller.RestartGame();
        }
    }
}
