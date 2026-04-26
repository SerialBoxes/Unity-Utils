using System.Runtime.CompilerServices;

namespace Unity.Mathematics {

public static partial class math {

    /// <summary>
    /// Takes the mod of dividend with divisor, but negative numbers are treated the same as positive numbers, so results are continuous across 0, if that makes sense?
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float modRight(float dividend, float divisor) {
        return dividend - divisor * floor(dividend / divisor);
    }
    
    /// <summary>
    /// Takes the mod of dividend with divisor, but negative numbers are treated the same as positive numbers, so results are continuous across 0, if that makes sense?
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double modRight(double dividend, double divisor) {
        return dividend - divisor * floor(dividend / divisor);
    }
    
    
    //https://forum.kerbalspaceprogram.com/topic/164418-vector3angle-more-accurate-and-numerically-stable-at-small-angles-version/
    /// <summary>
    /// Returns the smallest angle between two vectors in radians. This function remains stable at small angles, but is slower.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float angleStable(float3 a, float3 b) {
        var abm = a*length(b);
        var bam = b*length(a);
        return 2 * atan2(length(abm-bam), length(abm+bam));
    }
    
    //https://github.com/Unity-Technologies/Unity.Mathematics/issues/84
    /// <summary>
    /// Returns the signed angle in radians between two vectors as defined by an axis.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float angleSigned(float3 from, float3 to, float3 axis) {
        float angle = acos(dot(normalize(from), normalize(to)));
        float signn = sign(dot(axis, cross(from, to)));
        return angle * signn;
    }
    
    /// <summary>
    /// Returns the smallest angle between two vectors in radians.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float angleFast(float3 a, float3 b) {
        return acos(clamp(dot(a, b) / (length(a) * length(b)), -1f, 1f));
    }
    
    /// <summary>
    /// Calculates third side of triangle with sides a & b, separated by C radians
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float lawOfCosines(float a, float b, float C) {
        float csq = a*a + b*b - 2 *a*b*cos(C);
        return sqrt(csq);
    }

    /// <summary>
    /// Projects a vector onto a plane. Very helpful documentation yes yes.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float3 projectOntoPlane(float3 vector, float3 planeNormal) {
        return vector - dot(planeNormal, vector) * planeNormal;
    }

    //https://math.stackexchange.com/questions/4108428/how-to-project-vector-onto-a-plane-but-not-along-plane-normal
    /// <summary>
    /// Projects a vector onto a plane along an arbitrary direction.
    /// </summary>
    public static float3 projectOntoPlane(float3 vector, float3 planeNormal, float3 direction) {
        float numerator = dot(vector, planeNormal);
        float denominator = dot(direction, planeNormal);
        if (denominator == 0f){
            //vector already inside plane
            return vector;
        }
        float3 orthogonalComponent = (numerator / denominator) * direction;
        return vector - orthogonalComponent;
    }
}
}
