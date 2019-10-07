using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Birds
{
    [Serializable]
    public class BirdData
    {
        [SerializeField] private string _name;
        [SerializeField] private MovementType _movementType;
        [SerializeField] private BirdColor _color;

        public string Name => _name;
        public MovementType MovementType => _movementType;
        public BirdColor Color => _color;
    }
}