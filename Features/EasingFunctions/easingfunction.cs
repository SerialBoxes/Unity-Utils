using UnityEngine;

namespace UnityUtils
{
    public struct easingfunction
    {
        public enum EasingFunctionType {
            Linear,
            EaseInSine,
            EaseInQuad,
            EaseInCubic,
            EaseInQuart,
            EaseInQuint,
            EaseInExpo,
            EaseInCirc,
            EaseInBack,
            EaseOutSine,
            EaseOutQuad,
            EaseOutCubic,
            EaseOutQuart,
            EaseOutQuint,
            EaseOutExpo,
            EaseOutCirc,
            EaseOutBack,
            EaseInOutSine,
            EaseInOutQuad,
            EaseInOutCubic,
            EaseInOutQuart,
            EaseInOutQuint,
            EaseInOutExpo,
            EaseInOutCirc,
            EaseInOutBack,
        }

        public EasingFunctionType type;

        public float Evaluate(float x) => type switch {
            EasingFunctionType.Linear      => EasingFunctions.Linear(x),
            EasingFunctionType.EaseInSine  => EasingFunctions.EaseInSine(x),
            EasingFunctionType.EaseInQuad  => EasingFunctions.EaseInQuad(x),
            EasingFunctionType.EaseInCubic => EasingFunctions.EaseInCubic(x),
            EasingFunctionType.EaseInQuart => EasingFunctions.EaseInQuart(x),
            EasingFunctionType.EaseInQuint => EasingFunctions.EaseInQuint(x),
            EasingFunctionType.EaseInExpo  => EasingFunctions.EaseInExpo(x),
            EasingFunctionType.EaseInCirc  => EasingFunctions.EaseInCirc(x),
            EasingFunctionType.EaseInBack  => EasingFunctions.EaseInBack(x),
            EasingFunctionType.EaseOutSine  => EasingFunctions.EaseOutSine(x),
            EasingFunctionType.EaseOutQuad  => EasingFunctions.EaseOutQuad(x),
            EasingFunctionType.EaseOutCubic => EasingFunctions.EaseOutCubic(x),
            EasingFunctionType.EaseOutQuart => EasingFunctions.EaseOutQuart(x),
            EasingFunctionType.EaseOutQuint => EasingFunctions.EaseOutQuint(x),
            EasingFunctionType.EaseOutExpo  => EasingFunctions.EaseOutExpo(x),
            EasingFunctionType.EaseOutCirc  => EasingFunctions.EaseOutCirc(x),
            EasingFunctionType.EaseOutBack  => EasingFunctions.EaseOutBack(x),
            EasingFunctionType.EaseInOutSine  => EasingFunctions.EaseInOutSine(x),
            EasingFunctionType.EaseInOutQuad  => EasingFunctions.EaseInOutQuad(x),
            EasingFunctionType.EaseInOutCubic => EasingFunctions.EaseInOutCubic(x),
            EasingFunctionType.EaseInOutQuart => EasingFunctions.EaseInOutQuart(x),
            EasingFunctionType.EaseInOutQuint => EasingFunctions.EaseInOutQuint(x),
            EasingFunctionType.EaseInOutExpo  => EasingFunctions.EaseInOutExpo(x),
            EasingFunctionType.EaseInOutCirc  => EasingFunctions.EaseInOutCirc(x),
            EasingFunctionType.EaseInOutBack  => EasingFunctions.EaseInOutBack(x),
            _ => x,
        };
    }
}
