using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(MusicPlayer))]
public class MusicPlayerEditor : Editor {
    

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        MusicPlayer musicPlayer = (MusicPlayer)target;
        int levelCount = SceneManager.sceneCountInBuildSettings;

        while (musicPlayer.audioClips.Count < levelCount)
        {
            musicPlayer.audioClips.Add(null);
        }

        for (int i = 0; i < levelCount; i++)
        {
            EditorGUILayout.BeginHorizontal();
            // Point out current active level.
            if (SceneManager.GetActiveScene().buildIndex == i)
            {
                GUI.color = Color.green;
            }
            else
            {
                GUI.color = Color.white;
            }
            string levelName = System.IO.Path.GetFileNameWithoutExtension( SceneUtility.GetScenePathByBuildIndex(i));
            EditorGUILayout.PropertyField(
                serializedObject.FindProperty("audioClips").GetArrayElementAtIndex(i), 
                new GUIContent(levelName)
            );
            EditorGUILayout.EndHorizontal();
        }

        serializedObject.ApplyModifiedProperties();
    }

}
