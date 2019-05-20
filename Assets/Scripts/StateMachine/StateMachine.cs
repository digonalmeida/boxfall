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

        private TEntity _entity;

        public void Initialize(TEntity entity)
        {
            _entity = entity;
        }

        public void SetState(State<TEntity> state)
        {
            if (_currentState != null)
            {
                _currentState.OnExit();
            }

            _currentState = state;

            if (_currentState != null)
            {
                _currentState.Initialize(this, _entity);
                _currentState.OnEnter();
            }
        }

        public void Update()
        {
            if (_currentState != null)
            {
                _currentState.OnUpdate();
            }
        }

        public void Dispose()
        {
            SetState(null);
        }
    }

}