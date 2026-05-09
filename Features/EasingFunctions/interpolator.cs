using Unity.Mathematics;
using UnityEngine;

namespace UnityUtils
{
    //another case for descriminated unions
    public struct interpolator {
        public float from;
        public float to;
        public float time;
        public float duration;
        public easingfunction curve;

        public interpolator(float f, EasingFunctionType curveType = EasingFunctionType.Linear) {
            from = f;
            to = f;
            time = 1f;
            duration = 1f;
            curve = new easingfunction(curveType);
        }
        
        public interpolator(float from, float to, float duration, EasingFunctionType curveType = EasingFunctionType.Linear) {
            this.from = from;
            this.to = to;
            time = 1f;
            this.duration = duration;
            curve = new easingfunction(curveType);
        }

        public static implicit operator float(interpolator i) => i.value;
        
        public float value => math.lerp(from, to, curve.Evaluate(time/(duration != 0 ? duration : math.EPSILON)));

        public void stepTime(float t, bool clamp = true) => time = clamp ? math.min(duration, time + t) : time + t;

        public void lerpTowards(float to, float duration) {
            from = value;
            this.to = to;
            this.duration = duration;
            time = 0f;
        }

        public void setTo(float newValue) {
            from = newValue;
            to = newValue;
            time = 1f;
            duration = 1f;
        }
    }
}
