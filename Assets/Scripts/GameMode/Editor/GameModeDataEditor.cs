using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    private int _intColumnWidth = 100;
    private int _popupColumnWidth = 150;
    private int _floatColumnWidth = 80;
    private int _vector3ColumnWidth = 200;
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
        DrawCustomArray(spawnPointsProperty, DrawSpawnPointHeader, DrawSpawnPoint, false);
        DrawCustomArray(birdsProperty, DrawBirdDataHeader, DrawBirdData, false);
        DrawCustomArray(spawnersProperty, null, DrawSpawnerData, true);

        if (GUILayout.Button("save"))
        {
            Save();
        }
        //serializedObject
        //DrawDefaultInspector();
        serializedObject.ApplyModifiedProperties();
    }

    public void Save()
    {
        var gameModeDataSource = (GameModeDataSource) target;
        string json = JsonUtility.ToJson(gameModeDataSource.GameModeData);
        string filepath = Application.dataPath + "/data/game_mode_json/" + target.name + ".json";
        File.WriteAllText(filepath, json);
    }

    private void DrawSpawnPointHeader()
    {
        EditorGUILayout.BeginHorizontal();
        DrawLabel("Name");
        DrawVector3Label("Position");
        DrawFloatLabel("Angle");
        DrawFloatLabel("Force");
        EditorGUILayout.EndHorizontal();
    }
    private void DrawSpawnPoint(SerializedProperty spawn)
    {
        SerializedProperty name = spawn.FindPropertyRelative("_name");
        SerializedProperty position = spawn.FindPropertyRelative("_position");
        SerializedProperty angle = spawn.FindPropertyRelative("_angle");
        SerializedProperty force = spawn.FindPropertyRelative("_force");

        EditorGUILayout.BeginHorizontal();
        name.stringValue = EditorGUILayout.TextField(name.stringValue, GUILayout.Width(_popupColumnWidth));
        DrawVector3Property(position);
        DrawFloatProperty(angle);
        DrawFloatProperty(force);
        EditorGUILayout.EndHorizontal();
    }

    private void DrawFloatLabel(string label)
    {
        EditorGUILayout.LabelField(label, GUILayout.Width(_floatColumnWidth));
    }
    private void DrawFloatProperty(SerializedProperty property)
    {
        property.floatValue = EditorGUILayout.FloatField(property.floatValue, GUILayout.Width(_floatColumnWidth));
    }

    private void DrawVector3Label(string label)
    {
        EditorGUILayout.LabelField(label, GUILayout.Width(_vector3ColumnWidth));
    }
    private void DrawVector3Property(SerializedProperty property)
    {
        property.vector3Value = EditorGUILayout.Vector3Field("", property.vector3Value, GUILayout.Width(_vector3ColumnWidth));
    }
    
    private void DrawBirdDataHeader()
    {
        EditorGUILayout.BeginHorizontal();
        DrawLabel("Name");
        DrawLabel("Color");
        DrawLabel("Movement");
        EditorGUILayout.EndHorizontal();
    }
    private void DrawBirdData(SerializedProperty bird)
    {
        SerializedProperty name = bird.FindPropertyRelative("_name");
        SerializedProperty color = bird.FindPropertyRelative("_color");
        SerializedProperty movement = bird.FindPropertyRelative("_movementType");

        EditorGUILayout.BeginHorizontal();
        name.stringValue = EditorGUILayout.TextField(name.stringValue, GUILayout.Width(_popupColumnWidth));
        DrawEnumPopupProperty<BirdColor>(color);
        DrawEnumPopupProperty<MovementType>(movement);
        EditorGUILayout.EndHorizontal();
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
        
        DrawCustomArray(spawningInstances, DrawSpawningInstanceHeader, DrawSpawningInstance, false);
    }

    private void DrawSpawningInstanceHeader()
    {
        EditorGUILayout.BeginHorizontal();
        DrawIntLabel("Level");
        DrawLabel("SpawnPoint");
        DrawLabel("Spawn Type");
        DrawLabel("Spawn Id");
        EditorGUILayout.EndHorizontal();
    }
    
    private void DrawSpawningInstance(SerializedProperty spawningInstance)
    {
        SerializedProperty spawnId = spawningInstance.FindPropertyRelative("_spawnId");
        SerializedProperty spawnTypeProperty = spawningInstance.FindPropertyRelative("_spawnType");
        SerializedProperty minLevel = spawningInstance.FindPropertyRelative("_minLevel");
        SerializedProperty spawnPoint = spawningInstance.FindPropertyRelative("_spawnPointId");
        
        EditorGUILayout.BeginHorizontal();
        DrawIntProperty(minLevel);
        DrawPopupProperty(spawnPoint, _spawnPointNames);
        DrawEnumPopupProperty<SpawningInstance.ESpawnType>(spawnTypeProperty);
        if (spawnTypeProperty.intValue == (int) SpawningInstance.ESpawnType.Bird)
        {
            DrawPopupProperty(spawnId, _birdNames);
        }
        EditorGUILayout.EndHorizontal();
    }

    private void DrawLabel(string text)
    {
        EditorGUILayout.LabelField(text, GUILayout.Width(_popupColumnWidth));
    }
    private void DrawIntLabel(string text)
    {
        EditorGUILayout.LabelField(text, GUILayout.Width(_intColumnWidth));
    }
    private void DrawPopupProperty(SerializedProperty spawnPoint, string[] spawnPointNames)
    {
        spawnPoint.intValue = EditorGUILayout.Popup(spawnPoint.intValue, _spawnPointNames, GUILayout.Width(_popupColumnWidth));
    }

    private void DrawEnumPopupProperty<T>(SerializedProperty enumProperty) where T: Enum
    {
        T enumIndex = (T)(object)enumProperty.intValue;
        
        enumIndex = (T) EditorGUILayout.EnumPopup(enumIndex, GUILayout.Width(_popupColumnWidth));

        enumProperty.intValue = Convert.ToInt32(enumIndex);
    }

    private void DrawIntProperty(SerializedProperty intProperty)
    {
        intProperty.intValue = EditorGUILayout.IntField(intProperty.intValue, GUILayout.Width(_intColumnWidth));
    }

    private bool DrawFoldout(SerializedProperty property)
    {
        string key = property.propertyPath;
        if (!_foldoutStates2.ContainsKey(key))
        {
            _foldoutStates2[key] = true;
        }
        
        _foldoutStates2[key] = EditorGUILayout.Foldout(_foldoutStates2[key], property.displayName);
        return _foldoutStates2[key];
    }

    private bool DrawListFoldout(SerializedProperty property)
    {
        EditorGUILayout.BeginHorizontal();
        bool foldout = DrawFoldout(property);
        
        EditorGUILayout.EndHorizontal();
        return foldout;
    }
    private void DrawCustomArray(SerializedProperty listProperty, Action drawHeaderMethod, Action<SerializedProperty> drawChildMethod, bool childFoldout)
    {
        if (!DrawListFoldout(listProperty))
        {
            return;
        }
        
        EditorGUI.indentLevel++;
        SerializedProperty sizeProperty = listProperty.FindPropertyRelative("Array.size");
        EditorGUILayout.PropertyField(sizeProperty);
        
        drawHeaderMethod?.Invoke();
        
        for (int i = 0; i < listProperty.arraySize; i++)
        {
            SerializedProperty childProperty = listProperty.GetArrayElementAtIndex(i);
            if (childFoldout && !DrawFoldout(childProperty))
            {   
                continue;
            }
            
            EditorGUI.indentLevel++;
            drawChildMethod(childProperty);
            EditorGUI.indentLevel--;
           // EditorGUILayout.Space();
        }

        EditorGUI.indentLevel--;
    }
}

