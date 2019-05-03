using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingController : PlayerCharacterComponent
{
    private bool _shooting;
    private float _shootingInterval = 0.1f;
    private Coroutine _shootingCoroutine;
    private Vector2 _currentDirection;
    private Target _currentTarget;

    private void Start()
    {
        _currentDirection = Quaternion.Euler(0, 0, 45) * Vector2.right;
        Debug.Log(_currentDirection);
    }
    private void Update()
    {
        UpdateTarget();
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            Shoot(_currentDirection);
        }
    }

    private void UpdateTarget()
    {
        var targets = FindObjectsOfType<Target>();
        _currentTarget = null;
        var bestDistance = float.MaxValue;
        foreach(var target in targets)
        {
            if(!target.Alive)
            {
                continue;
            }
            var distanceVec = target.transform.position - PlayerCharacter.transform.position;
            if(Vector2.Angle(distanceVec, Vector2.right) > PlayerCharacter.AimMaxAngle)
            {
                continue;
            }

            var distance = distanceVec.magnitude;
            if(distance > PlayerCharacter.AimMaxRange)
            {
                continue;
            }
            if (distance < bestDistance)
            {
                bestDistance = distance;
                _currentTarget = target;
            }
        }

        if(_currentTarget != null)
        {
            var time = Vector2.Distance(_currentTarget.transform.position, PlayerCharacter.TurrentPivot.transform.position) / PlayerCharacter.ShotSpeed;
            var deflectionDistance = _currentTarget.Rigidbody.velocity * time;
            var aimPosition = _currentTarget.transform.position + (Vector3) deflectionDistance;
            if (aimPosition.y < -2.35f)
            {
                aimPosition.y = -2.35f;
            }
            _currentDirection = (aimPosition - PlayerCharacter.transform.position).normalized;
            PlayerCharacter.CrossHair.SetActive(true);
            PlayerCharacter.CrossHair.transform.position = _currentTarget.transform.position;
        }
        else
        {
            _currentDirection = Vector2.right;
            PlayerCharacter.CrossHair.SetActive(false);
        }

        PlayerCharacter.TurrentPivot.right = _currentDirection;
    }

    private void Shoot(Vector2 direction)
    {
        var shot = Instantiate(PlayerCharacter.BulletPrefab);
        var rigidBody = shot.GetComponent<Rigidbody2D>();
        shot.transform.position = PlayerCharacter.ShotOrigin.transform.position;
        rigidBody.velocity = direction * PlayerCharacter.ShotSpeed;
        shot.transform.right = direction;
        PlayerCharacter.Events.NotifyShot();
        var turrent = PlayerCharacter.TurrentPivot.GetComponent<TurrentController>();
        if (turrent != null)
        {
            turrent.PlayShotAnimation();
        }
        
    }
}
