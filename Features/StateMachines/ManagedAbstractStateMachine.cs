using System;
using System.Collections.Generic;

namespace UnityUtils.ManagedAbstractStateMachine
{
    //Assumes that State.name is the same as the key for the state in Dictionary<string, State<TInputs>> states
    //This one is the simplest but you have to make a class for every state and transition! GODILOVEOBJECTORIENTEDPROGRAMMING!!!!!!!!!
    [System.Serializable]
    public abstract class Machine {
        public string currentStateName;
        public float currentStateTime;
    }
    
    [System.Serializable]
    public abstract class State<TMachine, TInputs> {
        public Transition<TMachine, TInputs>[] transitions;
        public abstract void OnEnter(TMachine machine, TInputs inputs);
        public abstract void OnTick(float delta, TMachine machine, TInputs inputs);
        public abstract void OnExit(TMachine machine, TInputs inputs);
    }

    [System.Serializable]
    public abstract class Transition<TMachine, TInputs> {
        public string destinationName;
        public abstract bool ShouldTrigger (TMachine machine, TInputs input);
        public abstract void OnTrigger (TMachine machine, TInputs input);
    }
    
    [System.Serializable]
    public class StateMachine<TMachine, TInputs> where TMachine : Machine {
        
        private Dictionary<string, State<TMachine, TInputs>> states;

        public StateMachine(Dictionary<string, State<TMachine, TInputs>> states) {
            this.states = states;
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
            foreach (var transition in currentState.transitions) {
                if (transition.ShouldTrigger(machine, input)) {
                    SwitchState(transition.destinationName, machine, input);
                    transition.OnTrigger(machine, input);
                    break;
                }
            }
        }
        
        private void SwitchState(string newStateName, TMachine machine, TInputs input) {
            var currentState = states[machine.currentStateName];
            currentState.OnExit(machine, input);
            machine.currentStateTime = 0;
            machine.currentStateName = newStateName;
        }
    }
}
