using System;
using System.Collections.Generic;

namespace UnityUtils.ManagedDelegateStateMachine
{
    //Assumes that State.name is the same as the key for the state in Dictionary<string, State<TInputs>> states
    //This one is the simplest but you have to make a class for every state and transition! GODILOVEOBJECTORIENTEDPROGRAMMING!!!!!!!!!
    [System.Serializable]
    public abstract class State<TInputs> {
        public string name;
        public Transition<TInputs>[] transitions;
        public abstract void OnEnter(TInputs inputs);
        public abstract void OnTick(float delta, TInputs inputs);
        public abstract void OnExit(TInputs inputs);
    }

    [System.Serializable]
    public abstract class Transition<TInputs> {
        public string destinationName;
        public abstract bool ShouldTrigger (TInputs input);
        public abstract bool OnTrigger (TInputs input);
    }
    
    [System.Serializable]
    public class StateMachine<TInputs> {

        public string currentStateName;
        public float currentStateTime;
        
        private Dictionary<string, State<TInputs>> states;

        public StateMachine(Dictionary<string, State<TInputs>> states) {
            this.states = states;
        }

        public void Tick(float delta, TInputs input) {
            var currentState = states[currentStateName];

            if (currentStateTime == 0) {
                currentState.OnEnter(input);
            }

            currentState.OnTick(currentStateTime, input);
            currentStateTime += delta;

            EvaluateTransitions(input);
        }
        
        private void EvaluateTransitions(TInputs input) {
            var currentState = states[currentStateName];
            foreach (var transition in currentState.transitions) {
                if (transition.ShouldTrigger(input)) {
                    SwitchState(transition.destinationName, input);
                    transition.OnTrigger(input);
                }
            }
        }
        
        private void SwitchState(string newStateName, TInputs input) {
            var currentState = states[currentStateName];
            currentState.OnExit(input);
            currentStateTime = 0;
            currentStateName = newStateName;
        }
    }
}
