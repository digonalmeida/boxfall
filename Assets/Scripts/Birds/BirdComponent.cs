using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BirdController))]
public class BirdComponent : MonoBehaviour
{
    protected BirdController Bird { get; private set; }

    protected virtual void Awake()
    {
        Bird = GetComponent<BirdController>();
    }
}
