using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterCollision : PlayerCharacterComponent
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("here");
        if (!enabled)
        {
            return;
        }
        
        var box = other.collider.GetComponent<Target>();
        if (box != null)
        {
            PlayerCharacter.Events.NotifyHitByABox(other.collider);
            
        }
    }

    public void SetEnabled(bool enabled)
    {
        this.enabled = enabled;
        PlayerCharacter.Collider.enabled = enabled;
    }
}
