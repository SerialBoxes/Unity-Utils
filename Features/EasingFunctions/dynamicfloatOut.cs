namespace UnityUtils {
    [System.Serializable]
    public struct dynamicfloatOut {
        public float minOutput;
        public float maxOutput;
        public easingfunction curve;

        public float Eval(float t, float minInput, float maxInput, bool clamp = true) {
            return curve.Evaluate(t, minInput, maxInput, minOutput, maxOutput, clamp);
        }
    }
}