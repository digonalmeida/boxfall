using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    public void Deactivate()
    {
        gameObject.SetActive(false);
        enabled = false;
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        enabled = true;
    }
}
