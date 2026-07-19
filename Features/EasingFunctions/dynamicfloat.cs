using Unity.Mathematics;
using UnityEngine;
using UnityUtils.Mathematics;

namespace UnityUtils
{
    [System.Serializable]
    public struct dynamicfloat {
        public float minInput;
        public float maxInput;
        public float minOutput;
        public float maxOutput;
        public easingfunction curve;

        public float Eval(float t, bool clamp = true) {
            return Evaluate(t, minInput, maxInput, minOutput, maxOutput, curve, clamp);
        }
        
        public float EvalWithMaxInput(float t, float maxInput, bool clamp = true) {
            return Evaluate(t, minInput, maxInput, minOutput, maxOutput, curve, clamp);
        }
        
        public float EvalWithMinInput(float t, float minInput, bool clamp = true) {
            return Evaluate(t, minInput, maxInput, minOutput, maxOutput, curve, clamp);
        }
        
        public float EvalWithInputRange(float t, float minInput, float maxInput, bool clamp = true) {
            return Evaluate(t, minInput, maxInput, minOutput, maxOutput, curve, clamp);
        }

        private static float Evaluate(float t, float minI, float maxI, float minO, float maxO, easingfunction curve, bool clampInput) {
            float input = roxmath.MapFromRange(t, minI, maxI);
            if (clampInput) input = math.clamp(input, 0, 1);
            float fitToCurve = curve.Evaluate(input);
            return roxmath.MapToRange(fitToCurve, minO, maxO);
        }
    }
}
