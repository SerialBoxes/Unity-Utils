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
            return curve.Evaluate(t, minInput, maxInput, minOutput, maxOutput, clamp);
        }
    }
}
