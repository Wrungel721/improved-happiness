

using System.Collections.Generic;
using LearnProject.Exceptions;

namespace LearnProject.FSM
{
    public class BaseStateMachine
    {
        private BaseState _currentState;

        private List<BaseState> _states;
        private Dictionary<BaseState, List<Transition>> _transitions;

        public BaseStateMachine()
        {
            _states = new List<BaseState>();
            _transitions = new Dictionary<BaseState, List<Transition>>();
        }

        public void SetInitialState(BaseState state)
        {
            _currentState = state;
        }

        public void AddState(BaseState state, List<Transition> transitions)
        {
            if (!_states.Contains(state))
            {
                _states.Add(state);
                _transitions.Add(state, transitions);
            }
            else
            {

                throw new AlreadyExsistsException($"State {state.GetType()} already exists in state machine!");
            }
        }

        public void Update()
        {
            foreach (var transition in _transitions[_currentState])
            {
                if (transition.Condition())
                {
                    _currentState = transition.ToState;
                    break;

                }
            }

            _currentState.Execute();
        }
    }
}
