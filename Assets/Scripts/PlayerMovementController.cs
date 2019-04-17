using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : PlayerCharacterComponent
{
    Rigidbody2D _rigidbody;

    protected override void Awake()
    {
        base.Awake();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected override void OnStateChanged(PlayerState newState)
    {
        base.OnStateChanged(newState);
        switch (newState)
        {
            case PlayerState.Idle:
                _rigidbody.velocity = Vector3.zero;
                break;
            case PlayerState.Shooting:
                _rigidbody.AddForce(Vector2.left * PlayerCharacter.RecoilForce, ForceMode2D.Impulse);
                break;
            case PlayerState.ComeBack:
                _rigidbody.velocity = Vector3.right * PlayerCharacter.WalkingSpeed;
                break;
            default:
                break;
        }
    }
}
