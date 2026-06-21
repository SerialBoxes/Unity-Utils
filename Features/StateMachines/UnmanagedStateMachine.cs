using Unity.Collections;
using Unity.Entities;

namespace UnityUtils.UnmanagedStateMachine {
    
    //---How to Use---
    //You need a State struct, Transition struct, and Input struct
    //  State struct must inhereit IState and IName. In the callback functions, switch on <name> to determine what function of yours to call
    //      put the names of *outgoing* transitions in <transitionNames>
    //  Transition struct must inherit ITransition and IName. Again, switch on <name> to call appropriate function for the transition
    //  Input struct can be whatever the hell you want or need, as long as its unmanaged! :)
    //
    //call StateMachine.Tick() to step the machine!
    
    //If we ever get C# 15, switch this to unions types!!!!
    
    //---Data Types---
    public interface IMachineData {
        public uint currentStateID { get; set; }
        public float currentStateTime { get; set; }
    }

    public interface IName {
        public uint ID { get; }
    }

    public interface IState {
        public FixedList512Bytes<uint> transitionIDs { get; }
    }

    public interface ITransition {
        public uint destinationID { get; }
    }

    public interface IEvents<TMachineData, TInput, TState, TTransition> where TMachineData: unmanaged, IMachineData where TInput : unmanaged where TState : unmanaged, IState, IName where TTransition : unmanaged, ITransition, IName {
        public void OnEnter(ref TMachineData machine, TState state, TInput inputData);
        public void OnTick(float delta, ref TMachineData machine, TState state, TInput inputData);
        public void OnExit(ref TMachineData machine, TState state, TInput inputData);

        public bool ShouldTrigger(ref TMachineData machine, TTransition transition, TState state, TInput inputData);
        public void OnTrigger(ref TMachineData machine, TTransition transition, TState state, TInput inputData);
    }

    //---Actual Brains of the Operation---
    //(Make sure to call Dispose()!)
    public struct StateMachine<TMachineData, TState, TTransition, TInputs, TEvents> 
                                where TMachineData: unmanaged, IMachineData
                                where TState : unmanaged, IState, IName
                                where TTransition : unmanaged, ITransition, IName
                                where TInputs : unmanaged 
                                where TEvents : unmanaged, IEvents<TMachineData, TInputs, TState, TTransition> {

        private NativeHashMap<uint, TState> states;
        private NativeHashMap<uint, TTransition> transitions;
        private TEvents events;

        public StateMachine(NativeArray<TState> states, NativeArray<TTransition> transitions) {
            this.states = ArrayToDictionary(states);
            this.transitions = ArrayToDictionary(transitions);
            events = new();
        }

        public void Tick(float delta, ref TMachineData machineData, TInputs input) {
            
            EvaluateTransitions(ref machineData, input);
            
            var currentState = states[machineData.currentStateID];

            if (machineData.currentStateTime == 0) {
                events.OnEnter(ref machineData, currentState, input);
            }

            events.OnTick(delta, ref machineData, currentState, input);
            machineData.currentStateTime += delta;
        }
        
        private void EvaluateTransitions(ref TMachineData machineData, TInputs input) {
            var currentState = states[machineData.currentStateID];
            var transitionIDs = currentState.transitionIDs;
            
            foreach (var transitionID in transitionIDs) {
                var transition = transitions[transitionID];
                if (events.ShouldTrigger(ref machineData, transition, currentState, input)) {
                    SwitchState(ref machineData, transition.destinationID, input);
                    events.OnTrigger(ref machineData, transition, currentState, input);
                    break;
                }
            }
        }
        
        private void SwitchState(ref TMachineData machineData, uint newStateID, TInputs input) {
            var currentState = states[machineData.currentStateID];
            events.OnExit(ref machineData, currentState, input);
            machineData.currentStateTime = 0;
            machineData.currentStateID = newStateID;
        }


        private static NativeHashMap<uint, T> ArrayToDictionary<T>(NativeArray<T> array) where T : unmanaged, IName {
            NativeHashMap<uint, T> dictionary = new(array.Length, Allocator.Persistent);
            foreach (T item in array) {
                dictionary.Add(item.ID,item);
            }
            return dictionary;
        }

        public void Dispose() {
            states.Clear();
            transitions.Clear();
            states.Dispose();
            transitions.Dispose();
        }
    }
}