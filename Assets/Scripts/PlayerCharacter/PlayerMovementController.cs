using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : PlayerCharacterComponent
{
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
        if (PlayerCharacter.transform.position.x > 0 &&
            PlayerCharacter.Rigidbody.velocity.x >= 0)
        {
            PlayerCharacter.Rigidbody.velocity = Vector2.zero;
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
        if(PlayerCharacter.Rigidbody.velocity.x > 0)
        {
            PlayerCharacter.Rigidbody.velocity = Vector2.zero;
        }
        PlayerCharacter.Rigidbody.velocity += Vector2.left * PlayerCharacter.RecoilForce;
    }
}
