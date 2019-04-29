using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterEvents : MonoBehaviour
{
    public event Action OnShotFinished;
    public event Action<PlayerCharacterEntity, Collider2D> OnHitByABox;
    
    public PlayerCharacterEntity Entity { get; set; }
    public void NotifyShot()
    {
        if(OnShotFinished != null)
        {
            OnShotFinished();
        }
    }

    public void NotifyHitByABox(Collider2D box)
    {
        if (OnHitByABox != null)
        {
            OnHitByABox.Invoke(Entity, box);
        }
    }
}
