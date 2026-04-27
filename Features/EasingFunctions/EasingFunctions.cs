using Unity.Mathematics;
using UnityEngine;

namespace UnityUtils
{
    //https://easings.net/

    public static class EasingFunctions {
        
        // don't judge
        public static float Linear(float x) => x;

        //--- Ease In ---
        public static float EaseInSine    (float x) => 1 - math.cos((x * math.PI) / 2);
        public static float EaseInQuad    (float x) => x * x;
        public static float EaseInCubic   (float x) => x * x * x;
        public static float EaseInQuart   (float x) => x * x * x * x;
        public static float EaseInQuint   (float x) => x * x * x * x * x;
        public static float EaseInExpo    (float x) => x == 0 ? 0 : math.pow(2, 10 * x - 10);
        public static float EaseInCirc    (float x) => 1 - math.sqrt(1 - math.pow(x, 2));
        public static float EaseInBack    (float x, float c1 = 1.70158f) => (c1+1) * x * x * x - c1 * x * x;
        
        //--- Ease Out ---
        public static float EaseOutSine   (float x) => math.sin((x * math.PI) / 2);
        public static float EaseOutQuad   (float x) => 1 - (1 - x) * (1 - x);
        public static float EaseOutCubic  (float x) => 1 - math.pow(1 - x, 3);
        public static float EaseOutQuart  (float x) => 1 - math.pow(1 - x, 4);
        public static float EaseOutQuint  (float x) => 1 - math.pow(1 - x, 5);
        public static float EaseOutExpo   (float x) => x == 1 ? 1 : 1 - math.pow(2, -10 * x);
        public static float EaseOutCirc   (float x) => math.sqrt(1 - math.pow(x - 1, 2));
        public static float EaseOutBack   (float x, float c1 = 1.70158f) => 1 + (c1+1) * math.pow(x - 1, 3) + c1 * math.pow(x - 1, 2);
        
        //--- Ease In & Out ---
        public static float EaseInOutSine (float x) => -(math.cos(math.PI * x) - 1) / 2;
        public static float EaseInOutQuad (float x) => x < 0.5 ? 2 * x * x : 1 - math.pow(-2 * x + 2, 2) / 2;
        public static float EaseInOutCubic(float x) => x < 0.5 ? 4 * x * x * x : 1 - math.pow(-2 * x + 2, 3) / 2;
        public static float EaseInOutQuart(float x) => x < 0.5 ? 8 * x * x * x * x : 1 - math.pow(-2 * x + 2, 4) / 2;
        public static float EaseInOutQuint(float x) => x < 0.5 ? 16 * x * x * x * x * x : 1 - math.pow(-2 * x + 2, 5) / 2;
        public static float EaseInOutExpo (float x) => x == 0 ? 0 : x == 1 ? 1 : x < 0.5 ? math.pow(2, 20 * x - 10) / 2 : (2 - math.pow(2, -20 * x + 10)) / 2;
        public static float EaseInOutCirc (float x) => x < 0.5 ? (1 - math.sqrt(1 - math.pow(2 * x, 2))) / 2 : (math.sqrt(1 - math.pow(-2 * x + 2, 2)) + 1) / 2;
        public static float EaseInOutBack (float x, float c1 = 1.70158f*1.525f) => x < 0.5 ? (math.pow(2 * x, 2) * ((c1 + 1) * 2 * x - c1)) / 2 : (math.pow(2 * x - 2, 2) * ((c1 + 1) * (x * 2 - 2) + c1) + 2) / 2;
    }       
}
