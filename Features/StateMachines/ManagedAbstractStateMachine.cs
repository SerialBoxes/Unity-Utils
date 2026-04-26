using System;
using System.Collections.Generic;

namespace UnityUtils.ManagedAbstractStateMachine
{
    //Assumes that IStateData.name is the same as the key for the state in Dictionary<string, State<TInputs>> states
    //State & Transition data objects are not strongly typed so different states can store different data
    //  downcast for your specific data type!
    public interface IStateData {
        public string name { get; set; }
    }
    
    public interface ITransitionData {
        public string destinationName { get; set; }
    }

    [System.Serializable]
    public struct State<TInputs> {
        public IStateData data;
        public Transition<TInputs>[] transitions;
        public Action<IStateData, TInputs> OnEnter;
        public Action<float, IStateData, TInputs> OnTick;
        public Action<IStateData, TInputs> OnExit;
    }

    [System.Serializable]
    public struct Transition<TInputs> {
        public ITransitionData data;
        public Func<ITransitionData, TInputs, bool> ShouldTrigger;
        public Action<ITransitionData, TInputs> OnTrigger;
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
                currentState.OnEnter(currentState.data, input);
            }

            currentState.OnTick(currentStateTime, currentState.data, input);
            currentStateTime += delta;

            EvaluateTransitions(input);
        }
        
        private void EvaluateTransitions(TInputs input) {
            var currentState = states[currentStateName];
            foreach (var transition in currentState.transitions) {
                if (transition.ShouldTrigger(transition.data, input)) {
                    SwitchState(transition.data.destinationName, input);
                    transition.OnTrigger(transition.data, input);
                }
            }
        }
        
        private void SwitchState(string newStateName, TInputs input) {
            var currentState = states[currentStateName];
            currentState.OnExit(currentState.data, input);
            currentStateTime = 0;
            currentStateName = newStateName;
        }
    }
}
