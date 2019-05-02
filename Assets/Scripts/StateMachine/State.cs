using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public abstract class State<TEntity>
    {
        public virtual void OnEnter(TEntity entity) { }
        public virtual void OnUpdate(TEntity entity) { }
        public virtual void OnExit(TEntity entity) { }
    }
}

