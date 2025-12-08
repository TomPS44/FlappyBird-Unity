using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

[InitializeOnLoad] // le conctructor est exécuté lorsque qu'on compile ou recharge les scripts, ou on lance Unity 
public static class LoadMainScene
{
    
    private const string prefsKey = "MainMenuScene"; // sauvegarde la location de la scène à démarrer 
                                                     // dans EditorPrefs (même chose que PlayerPrefs mais pour l'Editor)

    private static string previousScenePath = "";

    static LoadMainScene()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged; // l'évenement (qui est un énum) est appelé lorsque qu'
                                                                          // on entre en Play Mode (EnterderPlayMode)
                                                                          // on sort du Play Mode (ExitingPlayMode)
                                                                          // on commence à entrer dans le Play Mode (ExitingEditMode) 
                                                                          // on revient à l’éditeur (EnteredEditMode)
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        /*
        if (state == PlayModeStateChange.ExitingEditMode) // vérifie si on entre en PlayMode
        {
            string scenePath = EditorPrefs.GetString(prefsKey, ""); // récupère la location de la scène, 
                                                                    // et si elle est vide, renvoie une string null

            if (!string.IsNullOrEmpty(scenePath)) // vérifie que la string (la location de la scène) ne soit pas null
            {
                /*
                var scene = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath); // ?? je crois que ça charge une scène ??

                if (scene != null) // vérifie que la scène n'est pas null
                {
                    EditorSceneManager.playModeStartScene = scene; // lance la scène au PlayMode
                }
                

                // Charger la scène dans l'éditeur (Hiérarchie)
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                {
                    var openedScene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);

                    if (openedScene.IsValid()) // vérifie que la scène n'est pas null
                    {
                        // Forcer Unity à utiliser cette scène comme scène de départ du Play Mode
                        var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath); // ?? je crois que ça charge une scène ??
                        EditorSceneManager.playModeStartScene = sceneAsset; // lance la scène au PlayMode
                    }
                    else
                    {
                        Debug.LogError($"Impossible d'ouvrir la scène : {scenePath}");
                    }
                }
            }
        }
        */

        switch (state)
        {
            // Juste avant d'entrer dans le Play Mode
            case PlayModeStateChange.ExitingEditMode:

                string scenePath = EditorPrefs.GetString(prefsKey, "");
                if (!string.IsNullOrEmpty(scenePath))
                {
                    // Sauvegarde la scène active avant le Play
                    previousScenePath = SceneManager.GetActiveScene().path;

                    // Demande de sauvegarde si la scène actuelle a été modifiée
                    if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                    {
                        // Ouvre la scène forcée dans la hiérarchie
                        var openedScene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);

                        if (openedScene.IsValid())
                        {
                            // Force Unity à utiliser cette scène comme scène de départ du Play Mode
                            var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);
                            EditorSceneManager.playModeStartScene = sceneAsset;
                        }
                        else
                        {
                            Debug.LogError($"Impossible d'ouvrir la scène : {scenePath}");
                        }
                    }
                }
                break;

            // Quand on quitte le Play Mode
            case PlayModeStateChange.EnteredEditMode:
                // Restaure la scène initiale si elle existe
                if (!string.IsNullOrEmpty(previousScenePath))
                {
                    EditorSceneManager.OpenScene(previousScenePath, OpenSceneMode.Single);
                    previousScenePath = "";
                }
                break;
        }
    }

    [MenuItem("Tools/Play From Scene/Set Start Scene")] // crée un menu Tools -> Play From Scene -> Set Start Scene
    public static void SetStartScene()
    {
        string path = EditorUtility.OpenFilePanel("Select Start Scene", "Assets", "unity"); // permet d'ouvrir le gestionnaire de fichier
                                                                                            // et de sélectionner la scène que l'on souhaite

        if (!string.IsNullOrEmpty(path)) // vérifie que le fichier sélectionné est pas vide
        {
            if (path.StartsWith(Application.dataPath))                             // convertit la location "C:\Users\AppData\Tom...
            {                                                                      // en chemin que Unity peut comprendre,
                path = "Assets" + path.Substring(Application.dataPath.Length);     // soit Assets\Scenes\
            }                                                                      
            
            EditorPrefs.SetString(prefsKey, path);  // sauvegarde cette location dans le EditorPrefs
            Debug.Log($"Play Mode will now start from: {path}"); // et log un message pour dire que c'est bon
        }
    }

    [MenuItem("Tools/Play From Scene/Clear Start Scene")] // crée un menu Tools -> Play From Scene -> Clear Start Scene
    public static void ClearStartScene()
    {
        EditorPrefs.DeleteKey(prefsKey); // supprime la variable stockant al location de la scène
        EditorSceneManager.playModeStartScene = null; // supprime la scène forcée au PlayMode
        Debug.Log("Play Mode will now start from the currently open scene."); // et log un message pour dire que c'est bon
    }
    

    /*
    private const string prefsKey = "PlayModeStartScene";
    private static string previousScenePath = "";
    private static List<string> favoriteScenes = new List<string>();

    static LoadMainScene()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        ToolbarExtender.RightToolbarGUI.Add(DrawToolbar);
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        switch (state)
        {
            case PlayModeStateChange.ExitingEditMode:
                string scenePath = EditorPrefs.GetString(prefsKey, "");
                if (!string.IsNullOrEmpty(scenePath))
                {
                    previousScenePath = SceneManager.GetActiveScene().path;
                    if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                    {
                        var openedScene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
                        if (openedScene.IsValid())
                        {
                            var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);
                            EditorSceneManager.playModeStartScene = sceneAsset;
                        }
                    }
                }
                break;

            case PlayModeStateChange.EnteredEditMode:
                if (!string.IsNullOrEmpty(previousScenePath))
                {
                    EditorSceneManager.OpenScene(previousScenePath, OpenSceneMode.Single);
                    previousScenePath = "";
                }
                break;
        }
    }

    // ---------------- Menu Items ----------------
    [MenuItem("Tools/Play From Scene/Set Start Scene")]
    private static void SetStartScene()
    {
        string path = EditorUtility.OpenFilePanel("Select Start Scene", "Assets", "unity");
        if (!string.IsNullOrEmpty(path))
        {
            if (path.StartsWith(Application.dataPath))
                path = "Assets" + path.Substring(Application.dataPath.Length);

            EditorPrefs.SetString(prefsKey, path);
            AddFavorite(path);
            Debug.Log($"Play Mode will now start from: {path}");
        }
    }

    [MenuItem("Tools/Play From Scene/Clear Start Scene")]
    private static void ClearStartScene()
    {
        EditorPrefs.DeleteKey(prefsKey);
        EditorSceneManager.playModeStartScene = null;
        Debug.Log("Play Mode will now start from the currently open scene.");
    }

    // ---------------- Favorites ----------------
    private static void AddFavorite(string path)
    {
        if (!favoriteScenes.Contains(path))
            favoriteScenes.Add(path);
    }

    [MenuItem("Tools/Play From Scene/Favorites")]
    private static void ShowFavorites()
    {
        GenericMenu menu = new GenericMenu();
        foreach (var fav in favoriteScenes)
            menu.AddItem(new GUIContent(fav), false, () => SetFavoriteScene(fav));
        menu.ShowAsContext();
    }

    private static void SetFavoriteScene(string path)
    {
        EditorPrefs.SetString(prefsKey, path);
        Debug.Log($"Start Scene set to favorite: {path}");
    }

    // ---------------- Toolbar Button ----------------
    private static void DrawToolbar()
    {
        GUILayout.FlexibleSpace();
        string scenePath = EditorPrefs.GetString(prefsKey, "None");
        string displayName = string.IsNullOrEmpty(scenePath) ? "None" : System.IO.Path.GetFileNameWithoutExtension(scenePath);

        if (GUILayout.Button($"Start Scene: {displayName}", GUILayout.Width(200)))
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Set Start Scene..."), false, SetStartScene);
            menu.AddItem(new GUIContent("Clear Start Scene"), false, ClearStartScene);
            if (favoriteScenes.Count > 0)
            {
                menu.AddSeparator("");
                foreach (var fav in favoriteScenes)
                    menu.AddItem(new GUIContent($"Favorite: {System.IO.Path.GetFileNameWithoutExtension(fav)}"), false, () => SetFavoriteScene(fav));
            }
            menu.ShowAsContext();
        }
    }
    */
}
