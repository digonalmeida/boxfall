using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxElement : MonoBehaviour
{
    [SerializeField]
    private float _ratio = 1.0f;

    [SerializeField]
    private float _startPoint = -1.0f;

    [SerializeField]
    private float _endPoint = 1.0f;

    public float Ratio => _ratio;
    public float StartPoint => _startPoint;
    public float EndPoint => _endPoint;
}
