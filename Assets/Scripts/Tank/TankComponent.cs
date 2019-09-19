using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankComponent : GameAgent
{
    protected TankController Tank { get; private set; }
    protected TankData TankData;
    public virtual void Initialize(TankController tank)
    {
        Tank = tank;
        TankData = Tank.TankData;
    }
}
