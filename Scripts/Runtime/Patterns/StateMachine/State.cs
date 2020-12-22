// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

namespace Evolutex.Evolunity.Patterns.StateMachine
{
    public abstract class State
    {
        /// <summary>
        /// If the time equals -1, then the state is inactive.
        /// </summary>
        public float TimeSinceEnter { get; private set; } = -1;
        
        protected StateMachine StateMachine { get; private set; }

        /// <summary>
        /// Don't call this method manually. It will be called automatically from the state machine.
        /// </summary>
        public void Initialize(StateMachine stateMachine)
        {
            StateMachine = stateMachine;

            OnInitialize();
        }
        
        /// <summary>
        /// Don't call this method manually. It will be called automatically from the state machine.
        /// </summary>
        public void Enter()
        {
            TimeSinceEnter = 0;
            
            OnEnter();
        }
        
        /// <summary>
        /// Don't call this method manually. It will be called automatically from the state machine.
        /// </summary>
        public void Update(float deltaTime)
        {
            OnUpdate(deltaTime);
            
            TimeSinceEnter += deltaTime;
        }
        
        /// <summary>
        /// Don't call this method manually. It will be called automatically from the state machine.
        /// </summary>
        public void Exit()
        {
            OnExit();
            
            TimeSinceEnter = -1;
        }
        
        protected virtual void OnInitialize() { }

        protected virtual void OnEnter() { }

        protected virtual void OnUpdate(float deltaTime) { }

        protected virtual void OnExit() { }
    }
}