using System;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UnityUtils {

public static class UnityUtils {
    
    /// <summary>
    /// Given a string in Camel Case, this returns a human-friendly version of that string.
    /// </summary>
    public static string FormatCamelCase(string camelCase) {
        string result = "";
        //https://stackoverflow.com/questions/773303/splitting-camelcase
        string[] tokens = Regex.Matches(camelCase, "([A-Z]+(?![a-z])|[A-Z][a-z]+|[0-9]+|[a-z]+)")
            .Select(m => m.Value)
            .ToArray();

        //capitalize each token and add a space
        foreach (string token in tokens) {
            result += System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(token) + " ";
        }

        return result.Trim(); //trim removes the last trailing space character
    }
    
    //NOTE TO SELF: go to town on this someday
    //https://easings.net/
    
    /// <summary>
    /// Animates a variable from <c>startValue</c> to <c>endValue</c> over <c>animTime</c> seconds using linear interpolation,
    /// and sends the result into <c>callback</c>.
    /// <example><code>
    /// float animateMe = 0;
    /// IEnumerator routine = NoodleUtils.FloatAnimatorLerp(res => animateMe=res, animateMe, 5f, 10f);
    /// StartCoroutine(routine);
    /// </code></example>
    /// </summary>
    public static IEnumerator FloatAnimatorLerp(Action<float> callback, float startValue, float endingValue, float animTime){
        float timer = 0;
        while(timer <= animTime){
            timer += Time.deltaTime;
            callback(Mathf.Lerp(startValue, endingValue, timer/animTime));//timer/time is our animation progress %
            yield return null;
        }
    }
    
    /// <summary>
    /// Animates a variable from <c>startValue</c> to <c>endValue</c> over <c>animTime</c> seconds using smooth step (s-curve),
    /// and sends the result into <c>callback</c>.
    /// <example><code>
    /// float animateMe = 0;
    /// IEnumerator routine = NoodleUtils.FloatAnimatorSmoothStep(res => animateMe=res, animateMe, 5f, 10f);
    /// StartCoroutine(routine);
    /// </code></example>
    /// </summary>
    public static IEnumerator FloatAnimatorSmoothStep(Action<float> callback, float startValue, float endingValue, float animTime){
        float timer = 0;
        while(timer <= animTime){
            timer += Time.deltaTime;
            callback(Mathf.SmoothStep(startValue, endingValue, timer/animTime));
            yield return null;
        }
    }
    
    /// <summary>
    /// Animates a variable from <c>startValue</c> to <c>endValue</c> over <c>animTime</c> seconds using <c>curve</c>,
    /// and sends the result into <c>callback</c>.
    /// <example><code>
    /// public AnimationCurve curve;//set in inspector
    /// float animateMe = 0;
    /// IEnumerator routine = NoodleUtils.FloatAnimatorAnimCurve(res => animateMe=res, animateMe, 5f, 10f, curve);
    /// StartCoroutine(routine);
    /// </code></example>
    /// </summary>
    public static IEnumerator FloatAnimatorAnimCurve(Action<float> callback, float startValue, float endingValue, float animTime, AnimationCurve curve){
        float timer = 0;
        while(timer <= animTime){
            timer += Time.deltaTime;
            float percent = curve.Evaluate(timer / animTime);
            callback(Mathf.LerpUnclamped(startValue, endingValue, percent));
            yield return null;
        }
    }
    
#if UNITY_EDITOR
    /// <summary>
    /// Editor only function to get the inspector border color for the current editor theme
    /// </summary>
    public static Color GetEditorBorderColor() {
        Color lightColor = new Color(0.6f, 0.6f, 0.6f);
        Color darkColor = new Color(0.1f, 0.1f, 0.1f);
        Color color = EditorGUIUtility.isProSkin ? darkColor : lightColor;
        return color;
    }
    
    /// <summary>
    /// Editor only function to get the inspector border color for the current editor theme
    /// </summary>
    public static Color GetEditorUIPrimaryColor() {
        Color lightColor = new Color(0.38f, 0.38f, 0.38f);
        Color darkColor = new Color(0.6f, 0.6f, 0.6f);
        Color color = EditorGUIUtility.isProSkin ? darkColor : lightColor;
        return color;
    }
#endif
}
}