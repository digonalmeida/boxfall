using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Birds;
using SpawnerV2;
using UnityEngine;
using UnityEditor;
using UnityEditor.Graphs;

[CustomEditor(typeof(GameModeDataSource))]
public class GameModeDataSourceEditor : Editor
{
    private Dictionary<string, bool> _foldoutStates2 = new Dictionary<string, bool>();
    private string[] _birdNames;
    private string[] _spawnPointNames;
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        var gameModeDataSource = (GameModeDataSource) target;
        
        List<string> birdNamesList = new List<string>();
        foreach (BirdData birdData in gameModeDataSource.GameModeData.Birds)
        {
            birdNamesList.Add(birdData.Name);
        }
        _birdNames = birdNamesList.ToArray();
        
        List<string> spawnPointNameList = new List<string>();
        foreach (SpawnPointData spawnPointData in gameModeDataSource.GameModeData.SpawnPoints)
        {
            spawnPointNameList.Add(spawnPointData.Name);
        }
        _spawnPointNames = spawnPointNameList.ToArray();

        SerializedProperty gameModeProperty = serializedObject.FindProperty("_gameModeData");
        SerializedProperty tankProperty = gameModeProperty.FindPropertyRelative("_tankData");
        SerializedProperty turrentProperty = gameModeProperty.FindPropertyRelative("_turrentData");
        SerializedProperty spawnersProperty = gameModeProperty.FindPropertyRelative("_spawners");
        SerializedProperty birdsProperty = gameModeProperty.FindPropertyRelative("_birds");
        SerializedProperty spawnPointsProperty = gameModeProperty.FindPropertyRelative("_spawnPoints");
        
        EditorGUILayout.PropertyField(tankProperty, true);
        EditorGUILayout.PropertyField(turrentProperty, true);
        EditorGUILayout.PropertyField(birdsProperty, true);
        EditorGUILayout.PropertyField(spawnPointsProperty, true);
        DrawCustomArray(spawnersProperty, DrawSpawnerData);
        
        //serializedObject
        //DrawDefaultInspector();
        serializedObject.ApplyModifiedProperties();
    }

    private void DrawSpawnerData(SerializedProperty spawner)
    {
        SerializedProperty nameProperty = spawner.FindPropertyRelative("_name");
        SerializedProperty startDelayProperty = spawner.FindPropertyRelative("_startDelay");
        SerializedProperty frequencyOverTimeProperty = spawner.FindPropertyRelative("_frequencyOverTime");
        SerializedProperty maxFrequencyProperty = spawner.FindPropertyRelative("_maxFrequency");
        SerializedProperty minFrequencyProperty = spawner.FindPropertyRelative("_minFrequency");
        SerializedProperty minLevel = spawner.FindPropertyRelative("_minLevel");
        SerializedProperty maxLevel = spawner.FindPropertyRelative("_maxLevel");
        SerializedProperty spawningInstances = spawner.FindPropertyRelative("_spawningInstances");
        
        EditorGUILayout.PropertyField(nameProperty, true);
        EditorGUILayout.PropertyField(startDelayProperty, true);
        EditorGUILayout.PropertyField(frequencyOverTimeProperty, true);
        EditorGUILayout.PropertyField(maxFrequencyProperty, true);
        EditorGUILayout.PropertyField(minFrequencyProperty, true);
        EditorGUILayout.PropertyField(minLevel, true);
        EditorGUILayout.PropertyField(maxLevel, true);
        DrawCustomArray(spawningInstances, DrawSpawningInstance);
    }

    private void DrawSpawningInstance(SerializedProperty spawningInstance)
    {
        
        SerializedProperty spawnId = spawningInstance.FindPropertyRelative("_spawnId");
        SerializedProperty spawnTypeProperty = spawningInstance.FindPropertyRelative("_spawnType");
        SerializedProperty minLevel = spawningInstance.FindPropertyRelative("_minLevel");
        SerializedProperty spawnPoint = spawningInstance.FindPropertyRelative("_spawnPointId");
     
        EditorGUILayout.PropertyField(minLevel);
        spawnPoint.intValue = EditorGUILayout.Popup("Spawn Point", spawnPoint.intValue, _spawnPointNames);
        EditorGUILayout.PropertyField(spawnTypeProperty);

        var spawnType = (SpawningInstance.ESpawnType) spawnTypeProperty.intValue;
        if (spawnType == SpawningInstance.ESpawnType.Bird)
        {
            spawnId.intValue = EditorGUILayout.Popup("bird", spawnId.intValue, _birdNames);
        }
    }
    

    private bool DrawFoldout(SerializedProperty property)
    {
        string key = property.propertyPath;
        if (!_foldoutStates2.ContainsKey(key))
        {
            _foldoutStates2[key] = false;
        }
        
        
        _foldoutStates2[key] = EditorGUILayout.Foldout(_foldoutStates2[key], property.displayName);
        return _foldoutStates2[key];
    }
    private void DrawCustomArray(SerializedProperty listProperty, Action<SerializedProperty> drawChildMethod)
    {
        if (!DrawFoldout(listProperty))
        {
            return;
        }

        EditorGUI.indentLevel++;
        SerializedProperty sizeProperty = listProperty.FindPropertyRelative("Array.size");
        EditorGUILayout.PropertyField(sizeProperty);
        for (int i = 0; i < listProperty.arraySize; i++)
        {
            SerializedProperty childProperty = listProperty.GetArrayElementAtIndex(i);
            if (!DrawFoldout(childProperty))
            {
                continue;
            }

            EditorGUI.indentLevel++;
            drawChildMethod(childProperty);
            EditorGUI.indentLevel--;
        }

        EditorGUI.indentLevel--;
    }
}

