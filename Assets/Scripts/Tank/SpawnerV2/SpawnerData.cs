using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SpawnerV2
{
    [Serializable]
    public class SpawnerData
    {
        [SerializeField] 
        private string _name;
        
        [SerializeField] 
        private float _startDelay;
        
        [SerializeField]
        private AnimationCurve _frequencyOverTime = AnimationCurve.Constant(0, 1, 0.5f);
    
        [SerializeField]
        private float _maxFrequency = 2.0f;
        
        [SerializeField]
        private float _minFrequency = 0.1f;

        [SerializeField] 
        private int _minLevel = 0;
    
        [SerializeField] 
        private int _maxLevel = 5;

        [SerializeField]
        private SpawningInstance[] _spawningInstances = null;

        public List<SpawningInstance> SpawningInstances() => _spawningInstances?.ToList();
        public float StartDelay => _startDelay;
        public AnimationCurve FrequencyOverTime => _frequencyOverTime;
        public float MaxFrequency => _maxFrequency;
        public float MinFrequency => _minFrequency;
        public int MinLevel => _minLevel;
        public int MaxLevel => _maxLevel;
    }


    [Serializable]
    public class SpawningInstance
    {
        [SerializeField] private string _name;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private int _minLevel = 0;
        [SerializeField] private Vector3 _position;
        [SerializeField] private float _angle;
        [SerializeField] private float _force;
        
        public GameObject Prefab => _prefab;
        public int MinLevel => _minLevel;
        public Vector3 Position => _position;
        public float Angle => _angle;
        public float Force => _force;
    }
}
