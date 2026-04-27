using Unity.Mathematics;
using UnityEngine;

namespace UnityUtils
{
    public struct dynamicfloat {
        public float minInput;
        public float maxInput;
        public float minOutput;
        public float maxOutput;
        public easingfunction curve;

        public float Eval(float t, bool clamp = true) {
            float input = math.MapFromRange(t, minInput, maxInput);
            if (clamp) input = math.clamp(input, 0, 1);
            float fitToCurve = curve.Evaluate(input);
            return math.MapToRange(fitToCurve, minOutput, maxOutput);
        }
    }
}
