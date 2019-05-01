using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxElement : MonoBehaviour
{
    [SerializeField]
    private float _ratio = 1.0f;

    public float Ratio
    {
        get
        {
            return _ratio;
        }
    }
}
