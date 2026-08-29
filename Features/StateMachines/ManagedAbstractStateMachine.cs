using System.Collections.Generic;

namespace UnityUtils.ManagedAbstractStateMachine
{
    // I am deeply upset that the cleanest state machine implementation I can come up with is Object Oriented
    [System.Serializable]
    public abstract class Machine {
        public string currentStateName;
        public string previousStateName;
        public float currentStateTime;
    }
    
    [System.Serializable]
    public abstract class State <TMachine, TInputs> {
        public string name;
        public abstract void OnEnter(TMachine machine, TInputs inputs);
        public abstract void OnTick(float delta, TMachine machine, TInputs inputs);
        public abstract void OnExit(TMachine machine, TInputs inputs);
        public abstract string EvaluateTransitions(TMachine machine, TInputs inputs);
    }
    
    [System.Serializable]
    public class StateMachine<TMachine, TInputs> where TMachine : Machine {
        
        private Dictionary<string, State<TMachine, TInputs>> states;

        public StateMachine(List<State<TMachine, TInputs>> stateList) {
            states = new();
            foreach (var state in stateList) {
                states.Add(state.name, state);
            }
        }

        public void Tick(float delta, TMachine machine, TInputs input) {
            EvaluateTransitions(machine, input);
            
            var currentState = states[machine.currentStateName];

            if (machine.currentStateTime == 0) {
                currentState.OnEnter(machine, input);
            }

            currentState.OnTick(delta, machine, input);
            machine.currentStateTime += delta;
        }
        
        private void EvaluateTransitions(TMachine machine, TInputs input) {
            var currentState = states[machine.currentStateName];
            var nextStateName = currentState.EvaluateTransitions(machine, input);
            if (machine.currentStateName != nextStateName) {
                SwitchState(nextStateName, machine, input);
            }
        }
        
        private void SwitchState(string newStateName, TMachine machine, TInputs input) {
            var currentState = states[machine.currentStateName];
            currentState.OnExit(machine, input);
            machine.currentStateTime = 0;
            machine.previousStateName = machine.currentStateName;
            machine.currentStateName = newStateName;
        }
    }
}
