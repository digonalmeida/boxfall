using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] 
    private float _amplitude = 1.0f;

    [SerializeField]
    private float _frequency = 5.0f;
    
    [SerializeField] 
    private float _time = 1.0f;

    private Vector3 _startPos;
    private Coroutine _coroutine;
    private Vector3 _pointA;
    private Vector3 _pointB;

    private void Awake()
    {
        _startPos = transform.position;
    }

    private void Start()
    {
        GameEvents.OnShotFired += StartShake;
    }

    private void OnDestroy()
    {
        GameEvents.OnShotFired -= StartShake;
    }

    [ContextMenu("Shake")]
    public void StartShake()
    {
        EndShake();
        _coroutine = StartCoroutine(ShakeRoutine());
    }

    public void EndShake()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
        
        transform.position = _startPos;
    }

    private IEnumerator ShakeRoutine()
    {
        var shakeTimeTotal = 60.0f / _frequency;
        var totalTime = 0.0f;
        _pointA = _startPos;
        while (totalTime < _time)
        {
            _pointA = transform.position;
           
            _pointB = _startPos +
                      (Random.Range(-_amplitude, _amplitude) * Vector3.right) +
                      (Random.Range(-_amplitude, _amplitude) * Vector3.up);
            var shakeTime = 0.0f;
            if (totalTime + shakeTime > _time)
            {
                shakeTime = _time - shakeTime;
                _pointB = _startPos;
            }
            
            while (shakeTime < shakeTimeTotal)
            {
                shakeTime += Time.deltaTime;
                totalTime += Time.deltaTime;
                transform.position = Vector3.Lerp(_pointA, _pointB, shakeTime / shakeTimeTotal);
                yield return null;
            }
        }
        EndShake();
        yield break;
    }
}
