using System;
using System.Collections.Generic;

namespace UnityUtils.ManagedDelegateStateMachine
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
    
    public interface IMachineData {
        public string currentStateName { get; set; }
        public float currentStateTime { get; set; }
    }

    [System.Serializable]
    public struct State<TInputs> {
        public IStateData data;
        public Transition<TInputs>[] transitions;
        public Action<IMachineData, IStateData, TInputs> OnEnter;
        public Action<float, IMachineData, IStateData, TInputs> OnTick;
        public Action<IMachineData, IStateData, TInputs> OnExit;
    }

    [System.Serializable]
    public struct Transition<TInputs> {
        public ITransitionData data;
        public Func<IMachineData, ITransitionData, TInputs, bool> ShouldTrigger;
        public Action<IMachineData, ITransitionData, TInputs> OnTrigger;
    }
    
    [System.Serializable]
    public class StateMachine<TInputs> {
        
        private Dictionary<string, State<TInputs>> states;

        public StateMachine(Dictionary<string, State<TInputs>> states) {
            this.states = states;
        }

        public void Tick(float delta, ref IMachineData machineData, TInputs input) {
            EvaluateTransitions(ref machineData, input);
            
            var currentState = states[machineData.currentStateName];

            if (machineData.currentStateTime == 0) {
                currentState.OnEnter(machineData, currentState.data, input);
            }

            currentState.OnTick(delta, machineData, currentState.data, input);
            machineData.currentStateTime += delta;
        }
        
        private void EvaluateTransitions(ref IMachineData machineData, TInputs input) {
            var currentState = states[machineData.currentStateName];
            foreach (var transition in currentState.transitions) {
                if (transition.ShouldTrigger(machineData, transition.data, input)) {
                    SwitchState(ref machineData, transition.data.destinationName, input);
                    transition.OnTrigger(machineData, transition.data, input);
                    break;
                }
            }
        }
        
        private void SwitchState(ref IMachineData machineData, string newStateName, TInputs input) {
            var currentState = states[machineData.currentStateName];
            currentState.OnExit(machineData, currentState.data, input);
            machineData.currentStateTime = 0;
            machineData.currentStateName = newStateName;
        }
    }
}
