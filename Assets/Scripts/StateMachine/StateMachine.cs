using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public class StateMachine<TEntity>
    {
        private State<TEntity> _currentState;
       
        public delegate void OnStateChangedDelegate(TEntity currentState);

        public TEntity Entity { private get; set; }

        public void SetState(State<TEntity> state)
        {
            if (_currentState != null)
            {
                _currentState.OnExit(Entity);
            }

            _currentState = state;

            if (_currentState != null)
            {
                _currentState.OnEnter(Entity);
            }
        }

        public void Update(TEntity entity)
        {
            if (_currentState != null)
            {
                _currentState.OnUpdate(entity);
            }
        }
    }

}