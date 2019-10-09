
using System;
using UnityEngine;

[Serializable]
public class IndexReference
{
    [SerializeField] private int _index;
    
    [HideInInspector]
    public string[] EditorIndexNames;

    public int Index => _index;
}
