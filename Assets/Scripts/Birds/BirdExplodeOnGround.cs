using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BirdController))]
public class BirdExplodeOnGround : BirdComponent
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("floor"))
        {
            Bird.KillBird();
        }
    }
}
