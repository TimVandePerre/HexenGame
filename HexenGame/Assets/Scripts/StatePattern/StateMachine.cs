﻿namespace StatePattern
{
    public class StateMachine
    {
        private IState _currentState;
        public StateMachine(IState startState) 
        {
            _currentState = startState;
            _currentState.OnEnter();
        }

        public void Update()
        {
            _currentState.Update();
        }

        public void MoveToState(IState nextState)
        {
            _currentState.OnExit();
            _currentState = nextState;
            _currentState.OnEnter();
        }
    }
}
