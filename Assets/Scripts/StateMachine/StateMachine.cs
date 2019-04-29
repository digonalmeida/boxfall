using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<TEntity>
{
    void OnEnter(TEntity entity) { }
    void OnExit(TEntity entity) { }
}

public class StateMachine<T>
{
    private T _currentState;
    private List<StateTransition> _stateTransitions = new List<StateTransition>();

    public delegate void OnStateChangedDelegate(T currentState);
    public event OnStateChangedDelegate OnStateChanged;

    public T CurrentState
    {
        get
        {
            return _currentState;
        }
        set
        {
            _currentState = value;
            if(OnStateChanged != null)
            {
                OnStateChanged(_currentState);
            }
        }
    }

    public void AddTransition(T currentState, Func<bool> condition, T targetState)
    {
        _stateTransitions.Add(new StateTransition(currentState, condition, targetState));
    }

    public void UpdateStateMachine()
    {
        for(int i = 0; i < _stateTransitions.Count; i++)
        {
            if(!EqualityComparer<T>.Default.Equals(CurrentState, _stateTransitions[i].CurrentState))
            {
                continue;
            }
            
            if(!_stateTransitions[i].Condition())
            {
                continue;
            }

            CurrentState = _stateTransitions[i].TargetState;
            return;
          //  i = 0;
        }
    }

    private class StateTransition
    {
        public readonly T CurrentState;
        public readonly Func<bool> Condition;
        public readonly T TargetState;
        public StateTransition(T currentState, Func<bool> condition, T targetState)
        {
            CurrentState = currentState;
            Condition = condition;
            TargetState = targetState;
        }
    }
}
