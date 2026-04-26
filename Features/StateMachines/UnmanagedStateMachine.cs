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

    public interface IState<TInput> {
        public FixedList512Bytes<uint> transitionIDs { get; }
        public void OnEnter(TInput inputData);
        public void OnTick(float delta, TInput inputData);
        public void OnExit(TInput inputData);
    }

    public interface ITransition<TState, TInput> where TState : unmanaged, IState<TInput>, IName where TInput : unmanaged {
        public uint ID { get; }
        public uint destinationID { get; }
        public bool ShouldTrigger(TState state, TInput input);
        public void OnTrigger(TState state, TInput input);
    }
    
    //---Actual Brains of the Operation---
    //(Make sure to call Dispose()!)
    public struct StateMachine<TMachineData, TStates, TTransitions, TInputs> 
                                where TMachineData: unmanaged, IMachineData
                                where TStates : unmanaged, IState<TInputs>, IName
                                where TTransitions : unmanaged, ITransition<TStates, TInputs>, IName
                                where TInputs : unmanaged {

        private NativeHashMap<uint, TStates> states;
        private NativeHashMap<uint, TTransitions> transitions;

        public StateMachine(NativeArray<TStates> states, NativeArray<TTransitions> transitions) {
            this.states = ArrayToDictionary(states);
            this.transitions = ArrayToDictionary(transitions);
        }

        public void Tick(float delta, ref TMachineData machineData, TInputs input) {
            var currentState = states[machineData.currentStateID];

            if (machineData.currentStateTime == 0) {
                currentState.OnEnter(input);
            }

            currentState.OnTick(machineData.currentStateTime, input);
            machineData.currentStateTime += delta;

            EvaluateTransitions(ref machineData, input);
        }
        
        private void EvaluateTransitions(ref TMachineData machineData, TInputs input) {
            var currentState = states[machineData.currentStateID];
            var transitionIDs = currentState.transitionIDs;
            foreach (var transitionID in transitionIDs) {
                var transition = transitions[transitionID];
                if (transition.ShouldTrigger(currentState, input)) {
                    SwitchState(ref machineData, transition.destinationID, input);
                    transition.OnTrigger(currentState, input);
                }
            }
        }
        
        private void SwitchState(ref TMachineData machineData, uint newStateID, TInputs input) {
            var currentState = states[machineData.currentStateID];
            currentState.OnExit(input);
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
            states.Dispose();
            transitions.Dispose();
        }
    }
}