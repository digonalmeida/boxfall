using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : PlayerCharacterComponent
{
    public void Enable()
    {
        PlayerCharacter.Rigidbody.velocity = Vector2.zero;
    }

    public void Disable()
    {
        PlayerCharacter.Rigidbody.velocity = Vector2.zero;
    }

    
    private void Start()
    {
        PlayerCharacter.Events.OnShotFinished += OnShot;
    }

    private void OnDestroy()
    {
        PlayerCharacter.Events.OnShotFinished -= OnShot;
    }
    
    public void Update()
    {
        float distanceFromCenter = PlayerCharacter.CenterX - PlayerCharacter.transform.position.x;
        if (distanceFromCenter < PlayerCharacter.WalkingStoppingDistance &&
            PlayerCharacter.Rigidbody.velocity.x >= 0)
        {
            float distanceRatio = distanceFromCenter / PlayerCharacter.WalkingStoppingDistance;
            var vel = Vector2.Lerp(Vector2.right * PlayerCharacter.WalkingSpeed, Vector2.zero, 1 - distanceRatio);
            if (vel.magnitude < PlayerCharacter.Rigidbody.velocity.magnitude)
            {
                PlayerCharacter.Rigidbody.velocity = vel;
            }
        }
        else
        {
            
            if(PlayerCharacter.Rigidbody.velocity.x > 0)
            {
                PlayerCharacter.Rigidbody.AddForce(Vector2.right * PlayerCharacter.WalkingSpeed);
                PlayerCharacter.Rigidbody.velocity = Vector2.ClampMagnitude(PlayerCharacter.Rigidbody.velocity,
                                                                        PlayerCharacter.WalkingSpeed);
            }
            else
            {
                PlayerCharacter.Rigidbody.AddForce(Vector2.right * PlayerCharacter.RecoilDeacceleration);
            }
        }
    }

    private void OnShot()
    {
        if(PlayerCharacter.Rigidbody.velocity.x > 0 || true)
        {
            PlayerCharacter.Rigidbody.velocity = Vector2.zero;
        }
        PlayerCharacter.Rigidbody.velocity += Vector2.left * PlayerCharacter.RecoilForce;
        PlayerCharacter.Rigidbody.velocity = Vector2.ClampMagnitude(PlayerCharacter.Rigidbody.velocity,
            PlayerCharacter.MaxRecoilForce);
    }
}
