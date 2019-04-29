using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public class StateTransition<T>
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