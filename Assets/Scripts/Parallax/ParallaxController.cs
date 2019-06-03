using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : GameAgent
{
    private ParallaxElement[] _parallaxElements;
    [SerializeField]
    private float startSpeed = 1.0f;

    [SerializeField]
    private float maxSpeed = 5.0f;

    [SerializeField]
    private float acceleration = 0.2f;

    private float speed;

    protected override void Awake()
    {
        base.Awake();
        _parallaxElements = transform.GetComponentsInChildren<ParallaxElement>();
        speed = startSpeed;

        enabled = false;
    }

    protected override void OnGameStarted()
    {
        base.OnGameStarted();
        
        speed = startSpeed;
        enabled = true;
    }

    protected override void OnGameEnded()
    {
        speed = 0;
        enabled = false;
    }

    private void Update()
    {
        if (IsPaused)
        {
            return;
        }
        
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
