using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankComponent : MonoBehaviour
{
    protected TankController Tank { get; private set; }
    public virtual void Initialize(TankController tank)
    {
        Tank = tank;
    }
}
