using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingController : PlayerCharacterComponent
{
    private bool _shooting;
    private float _shootingInterval = 0.1f;
    private Coroutine _shootingCoroutine;
    
    protected override void OnStateChanged(PlayerState newState)
    {
        base.OnStateChanged(newState);
        if(newState == PlayerState.Shooting)
        {
            PlayerCharacter.Shooting = true;
            Shoot();
            PlayerCharacter.Shooting = false;
        }
    }

    private void Shoot()
    {
        var shot = Instantiate(PlayerCharacter.BulletPrefab);
        var rigidBody = shot.GetComponent<Rigidbody2D>();
        shot.transform.position = transform.position;
        rigidBody.velocity = Vector3.right * PlayerCharacter.ShotSpeed;
    }
}
