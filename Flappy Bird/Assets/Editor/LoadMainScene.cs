using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.Linq;
using Unity.VisualScripting;

[InitializeOnLoad]
public static class LoadMainScene
{
    private const string prefsKey = "MainMenuScene";

    private static SceneSetup[] previousSceneSetup;

    static LoadMainScene()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        switch (state)
        {
            // Juste avant d’entrer en Play Mode
            case PlayModeStateChange.ExitingEditMode:
            {
                string scenePath = EditorPrefs.GetString(prefsKey, "");

                if (!string.IsNullOrEmpty(scenePath))
                {
                    // Sauvegarde TOUT l’état des scènes
                    previousSceneSetup = EditorSceneManager.GetSceneManagerSetup();

                    if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                    {
                        var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);
                        EditorSceneManager.playModeStartScene = sceneAsset;

                        EditorApplication.delayCall += () =>
                        {
                            var scene = SceneManager.GetSceneByPath(scenePath);
                            if (!scene.IsValid() || !scene.isLoaded)
                                return;

                            // Force l’affichage de la scène dans la hiérarchie
                            foreach (var root in scene.GetRootGameObjects())
                            {
                                EditorGUIUtility.PingObject(root);
                            }
                        };
                    }
                }
                break;
            }

            // Une fois revenu en Edit Mode (IMPORTANT)
            case PlayModeStateChange.EnteredEditMode:
            {
                // Supprime la scène forcée
                EditorSceneManager.playModeStartScene = null;

                // Restaure les scènes précédentes
                if (previousSceneSetup != null && previousSceneSetup.Length > 0)
                {
                    EditorSceneManager.RestoreSceneManagerSetup(previousSceneSetup);
                    previousSceneSetup = null;
                }
                break;
            }
        }
    }

    [MenuItem("Tools/Scene Loader/Set Start Scene")]
    public static void SetStartScene()
    {
        string path = EditorUtility.OpenFilePanel("Select Start Scene", "Assets", "unity");

        if (!string.IsNullOrEmpty(path) && path.StartsWith(Application.dataPath))
        {
            path = "Assets" + path.Substring(Application.dataPath.Length);
            EditorPrefs.SetString(prefsKey, path);
            Debug.Log($"Play Mode will now start from: {path}");
        }
    }

    [MenuItem("Tools/Scene Loader/Clear Start Scene")]
    public static void ClearStartScene()
    {
        EditorPrefs.DeleteKey(prefsKey);
        EditorSceneManager.playModeStartScene = null;
        Debug.Log("Play Mode will now start from the currently open scene.");
    }

    [MenuItem("Tools/Scene Loader/Show Scene To Load")]
    public static void ShowSceneToLoad()
    {
        string sceneToLoad = EditorPrefs.GetString(prefsKey);
        /*
        string stringToLog = sceneToLoad.Substring("Assets/Scenes/".Length);

        string partToRemove = stringToLog.Substring(stringToLog.Length - ".unity".Length, ".unity".Length);

        stringToLog.Remove(partToRemove.Length);
        */
        
        Debug.Log(sceneToLoad);
    }
}