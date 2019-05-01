using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    private ParallaxElement[] _parallaxElements;
    [SerializeField]
    private float startSpeed = 1.0f;

    [SerializeField]
    private float maxSpeed = 5.0f;

    [SerializeField]
    private float acceleration = 0.2f;

    private float speed;

    private void Awake()
    {
        _parallaxElements = transform.GetComponentsInChildren<ParallaxElement>();
        speed = startSpeed;
    }

    private void Update()
    {
        speed = Mathf.Min(maxSpeed, speed + (acceleration * Time.deltaTime));
        foreach(var element in _parallaxElements)
        {
            var pos = element.gameObject.transform.position;
            
            pos += Vector3.left * speed * Time.deltaTime * element.Ratio;
            if(pos.x < -4)
            {
                pos.x += 10;
            }
            element.transform.position = pos;
        }
    }
}
