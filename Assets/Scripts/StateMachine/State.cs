using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public abstract class State<TEntity>
    {
        protected TEntity Entity { get; private set; }

        private StateMachine<TEntity> _stateMachine;
        private bool _initialized;

        public virtual void OnEnter() { }
        public virtual void OnUpdate() { }
        public virtual void OnExit() { }

        public void Initialize(StateMachine<TEntity> stateMachine, TEntity entity)
        {
            if(_initialized)
            {
                return;
            }

            _initialized = true;
            Entity = entity;
            _stateMachine = stateMachine;
        }

        protected void ChangeState(State<TEntity> state)
        {
            _stateMachine?.SetState(state);
        }
    }
}

