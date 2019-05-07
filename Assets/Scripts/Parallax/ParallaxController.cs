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
        GameEvents.OnGameStarted += StartParallax;
        GameEvents.OnGameEnded += StopParallax;
        enabled = false;
    }

    private void OnDestroy()
    {
        GameEvents.OnGameStarted -= StartParallax;
        GameEvents.OnGameEnded -= StopParallax;
    }

    private void StartParallax()
    {
        speed = startSpeed;
        enabled = true;
    }

    private void StopParallax()
    {
        speed = 0;
        enabled = false;
    }

    private void Update()
    {
        if (!isActiveAndEnabled)
        {
            return;
        }
        
        speed = Mathf.Min(maxSpeed, speed + (acceleration * Time.deltaTime));
        foreach(var element in _parallaxElements)
        {
            var pos = element.gameObject.transform.position;
            
            pos += Vector3.left * speed * Time.deltaTime * element.Ratio;
            if(pos.x <= element.EndPoint)
            {
                pos.x += element.StartPoint - element.EndPoint;
            }

            element.transform.position = pos;
        }
    }
}
