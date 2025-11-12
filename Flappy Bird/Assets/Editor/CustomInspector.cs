using System;
using Unity.Properties;
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


/*
[CustomEditor(typeof(UIHelper))]
public class LerpAssetEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        UIHelper helper = (UIHelper)target;

        if (GUILayout.Button("Lerp"))
        {
            UIHelper.CallFade(helper.go, helper.speed);
        }
    }
}
*/


[CustomPropertyDrawer(typeof(CustomCollection))]
public class CustomCollectionDrawer : PropertyDrawer
{
    private bool isFolderOpen;


    // this other line is also useless, it was used to create a style that modified the font of folder 
    // private GUIStyle style = new GUIStyle();


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // the line below is useless, it was just used to set the font of the folder to normal instead of bold
        // but it causes another issue that I don't know how to fix :)
        // style.fontStyle = FontStyle.Normal;


        // creates a folder that is open if "isFolderOpen" is true 
        isFolderOpen = EditorGUI.BeginFoldoutHeaderGroup(position, isFolderOpen, property.displayName);

        // make the inspector appear if the folder is open, and handles all the fields, the variables...
        if (isFolderOpen)
        {
            // begins the entire property containing all the fields 
            EditorGUI.BeginProperty(position, label, property);


            // ----------------SerializedProperties-------------------


            // variable storing the image (Image)
            SerializedProperty image = property.FindPropertyRelative("image");

            // variable that stores the button (Button)
            SerializedProperty button = property.FindPropertyRelative("button");

            // variable that stores the target position (Vector3)
            SerializedProperty targetPos = property.FindPropertyRelative("targetPos");

            // variable that stores the speed (float)
            SerializedProperty speed = property.FindPropertyRelative("speed");

            // variable that stores the animation curve (AnimationCurve)
            SerializedProperty animCurve = property.FindPropertyRelative("animCurve");


            // variable that stores the enum 
            SerializedProperty typeOfCollection = property.FindPropertyRelative("collectionType");
            // variable storing the type of collection (from the CollectionType enum)
            CustomCollection.CollectionType collectionType = (CustomCollection.CollectionType)typeOfCollection.enumValueIndex;

            // ---------------------------Fields--------------------------------


            // RectTransform (same as tranform but with 4 parameters) of the enum
            Rect collectionTypeRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 1.5f, position.width, position.height);

            // field that displays the enum
            EditorGUI.PropertyField(collectionTypeRect, typeOfCollection);

            bool defaultWideMode = EditorGUIUtility.wideMode;

            // switch that makes certains fields appear or not, depending on the type of the collection
            switch (collectionType)
            {
                case CustomCollection.CollectionType.OnlyImage:

                    Rect imageRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 3f, position.width, position.height);
                    EditorGUI.PropertyField(imageRect, image);


                    EditorGUIUtility.wideMode = true;

                    Rect targetPosRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 5f, position.width, position.height);
                    EditorGUI.PropertyField(targetPosRect, targetPos);

                    EditorGUIUtility.wideMode = defaultWideMode;


                    Rect speedRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 6.5f, position.width, position.height);
                    EditorGUI.PropertyField(speedRect, speed);

                    break;

                case CustomCollection.CollectionType.ImagePlusCurve:

                    Rect imageRect2 = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 3f, position.width, position.height);
                    EditorGUI.PropertyField(imageRect2, image);

                    Rect curveRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 4.5f, position.width, position.height);
                    EditorGUI.PropertyField(curveRect, animCurve);


                    EditorGUIUtility.wideMode = true;

                    Rect targetPosRect2 = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 6.5f, position.width, position.height);
                    EditorGUI.PropertyField(targetPosRect2, targetPos);

                    EditorGUIUtility.wideMode = defaultWideMode;


                    Rect speedRect2 = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 8f, position.width, position.height);
                    EditorGUI.PropertyField(speedRect2, speed);

                    break;

                case CustomCollection.CollectionType.OnlyButton:

                    Rect buttonRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 3f, position.width, position.height);
                    EditorGUI.PropertyField(buttonRect, button);


                    EditorGUIUtility.wideMode = true;

                    Rect targetPosRect3 = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 5f, position.width, position.height);
                    EditorGUI.PropertyField(targetPosRect3, targetPos);

                    EditorGUIUtility.wideMode = defaultWideMode;


                    Rect speedRect3 = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 6.5f, position.width, position.height);
                    EditorGUI.PropertyField(speedRect3, speed);

                    break;

                case CustomCollection.CollectionType.ButtonPlusCurve:

                    Rect buttonRect2 = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 3f, position.width, position.height);
                    EditorGUI.PropertyField(buttonRect2, button);

                    Rect curveRect2 = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 4.5f, position.width, position.height);
                    EditorGUI.PropertyField(curveRect2, animCurve);


                    EditorGUIUtility.wideMode = true;

                    Rect targetPosRect4 = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 6.5f, position.width, position.height);
                    EditorGUI.PropertyField(targetPosRect4, targetPos);

                    EditorGUIUtility.wideMode = defaultWideMode;


                    Rect speedRect4 = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 8f, position.width, position.height);
                    EditorGUI.PropertyField(speedRect4, speed);

                    break;
            }

            // GUILayout.Space(EditorGUIUtility.singleLineHeight * 2f);

            // Rect trueTargetPosRect = new Rect(position.x, position.y, position.width, position.height);
            // EditorGUI.PropertyField(trueTargetPosRect, targetPos);

            // Rect trueSpeedRect4 = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 5.5f, position.width, position.height);
            // EditorGUI.PropertyField(trueSpeedRect4, speed);


            // and specifies the end of the folder
            EditorGUI.EndProperty();
        }

        // and specifies the end of the folder
        EditorGUI.EndFoldoutHeaderGroup();



        // variable that stores the enum 
        SerializedProperty typeOfCollection2 = property.FindPropertyRelative("collectionType");
        // variable storing the type of collection (from the CollectionType enum)
        CustomCollection.CollectionType collectionType2 = (CustomCollection.CollectionType)typeOfCollection2.enumValueIndex;

        // handles the spaces in the inspector depending on the collectionType, and if the folder is opened or not
        if (isFolderOpen)
        {
            switch (collectionType2)
            {
                case CustomCollection.CollectionType.OnlyImage:

                    GUILayout.Space(120f);
                    GUILayout.Space(15f);
                    break;

                case CustomCollection.CollectionType.ImagePlusCurve:

                    GUILayout.Space(150f);
                    GUILayout.Space(15f);
                    break;

                case CustomCollection.CollectionType.OnlyButton:

                    GUILayout.Space(120f);
                    GUILayout.Space(15f);
                    break;

                case CustomCollection.CollectionType.ButtonPlusCurve:

                    GUILayout.Space(150f);
                    GUILayout.Space(15f);
                    break;
            }
        }
        else
        {
            GUILayout.Space(10f);
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label);
    }
} 


