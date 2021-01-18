// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Evolutex.Evolunity.Patterns.StateMachine
{
    // TO DO:
    // EnterPreviousState method.
    // Make property drawer for state type selection.

    public class StateMachine : IEnumerable<State>
    {
        private readonly Dictionary<Type, State> states = new Dictionary<Type, State>();

        public StateMachine(State initialState, params State[] otherStates)
        {
            AddState(initialState);
            EnterState(initialState.GetType());

            foreach (State state in otherStates)
                AddState(state);
        }

        public delegate void StateChangeHandler(State previousState, State currentState);

        public event StateChangeHandler StateChanged;

        public State CurrentState { get; private set; }
        public State PreviousState { get; private set; }

        public void EnterState<TState>() where TState : State
        {
            EnterState(typeof(TState));
        }

        public void EnterState(Type stateType)
        {
            if (!ContainsState(stateType))
            {
                Debug.LogError("State machine doesn't contains state " + stateType.Name);

                return;
            }

            PreviousState = CurrentState;
            CurrentState = GetState(stateType);

            PreviousState?.Exit();
            CurrentState.Enter();

            StateChanged?.Invoke(PreviousState, CurrentState);
        }

        public bool ContainsState<TState>() where TState : State
        {
            return ContainsState(typeof(TState));
        }

        public bool ContainsState(Type stateType)
        {
            return states.ContainsKey(stateType);
        }

        public TState GetState<TState>() where TState : State
        {
            return (TState) GetState(typeof(TState));
        }

        public State GetState(Type stateType)
        {
            return states[stateType];
        }

        public void Update(float deltaTime)
        {
            CurrentState.Update(deltaTime);
        }

        private void AddState(State state)
        {
            Type stateType = state.GetType();
            if (ContainsState(stateType))
            {
                Debug.LogError("State machine already contains state " + stateType.Name);

                return;
            }

            states.Add(stateType, state);
            state.Initialize(this);
        }

        public IEnumerator<State> GetEnumerator()
        {
            return states.Select(x => x.Value).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}