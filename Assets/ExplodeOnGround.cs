using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnGround : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("floor"))
        {
            GetComponent<Target>().DestroyTarget();
        }
    }
}
