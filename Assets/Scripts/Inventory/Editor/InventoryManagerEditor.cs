using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(InventoryManager))] 
public class InventoryManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        var inventoryManager = (InventoryManager)target;
        if(GUILayout.Button("ReloadInventory"))
        {
            var itemConfigs = FindItemConfigs();
            inventoryManager.SetupItems(itemConfigs);
            EditorUtility.SetDirty(inventoryManager);
        }
    }

    private List<ItemConfig> FindItemConfigs()
    {
        List<ItemConfig> itemConfigs = new List<ItemConfig>();
        
        var configs = AssetDatabase.FindAssets("t:" + typeof(ItemConfig).Name, new string[]{"Assets/data/Items"});
        foreach (var config in configs)
        {
            string path = AssetDatabase.GUIDToAssetPath(config);
            var itemConfig = AssetDatabase.LoadAssetAtPath<ItemConfig>(path);
            itemConfigs.Add(itemConfig);
        }

        return itemConfigs;
    }
}
