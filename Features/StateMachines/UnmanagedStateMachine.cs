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
    
    //---Data Types---
    public interface IMachineData {
        public uint currentStateID { get; set; }
        public uint previousStateID { get; set; }
        public float currentStateTime { get; set; }
    }

    public interface IName {
        public uint ID { get; }
    }

    public interface IEvents<TMachineData, TInput, TState> where TMachineData: unmanaged, IMachineData where TInput : unmanaged where TState : unmanaged, IName {
        //make these static abstract in c# 14 hopefully (and maybe have state classes implement it as well)
        public void OnEnter(ref TMachineData machine, TState state, TInput inputData);
        public void OnTick(float delta, ref TMachineData machine, TState state, TInput inputData);
        public void OnExit(ref TMachineData machine, TState state, TInput inputData);
        public uint EvaluateTransitions(ref TMachineData machine, TState state, TInput inputData);
    }

    //---Actual Brains of the Operation---
    //(Make sure to call Dispose()!)
    public struct StateMachine<TMachineData, TState, TInputs, TSwitch> 
                                where TMachineData: unmanaged, IMachineData
                                where TState : unmanaged, IName
                                where TInputs : unmanaged 
                                where TSwitch : unmanaged, IEvents<TMachineData, TInputs, TState> {

        private NativeHashMap<uint, TState> states;
        private TSwitch stateSwitch;

        public StateMachine(NativeArray<TState> states) {
            this.states = ArrayToDictionary(states);
            stateSwitch = new();
        }

        public void Tick(float delta, ref TMachineData machineData, TInputs input) {
            
            EvaluateTransitions(ref machineData, input);
            
            var currentState = states[machineData.currentStateID];

            if (machineData.currentStateTime == 0) {
                stateSwitch.OnEnter(ref machineData, currentState, input);
            }

            stateSwitch.OnTick(delta, ref machineData, currentState, input);
            machineData.currentStateTime += delta;
        }
        
        private void EvaluateTransitions(ref TMachineData machineData, TInputs input) {
            var currentState = states[machineData.currentStateID];
            var nextStateID = stateSwitch.EvaluateTransitions(ref machineData, currentState, input);
            if (nextStateID != machineData.currentStateID) {
                SwitchState(ref machineData, nextStateID, input);
            }
        }
        
        private void SwitchState(ref TMachineData machineData, uint newStateID, TInputs input) {
            var currentState = states[machineData.currentStateID];
            stateSwitch.OnExit(ref machineData, currentState, input);
            machineData.currentStateTime = 0;
            machineData.previousStateID = machineData.currentStateID;
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
            states.Dispose();
        }
    }
}