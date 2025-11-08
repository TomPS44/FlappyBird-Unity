using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.UI;

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

[CustomEditor(typeof(UIController))]
public class LerpAssetEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        UIController controller = (UIController)target;

        if (GUILayout.Button("Lerp"))
        {
            controller.Test();
        }
    }
}
