using UnityEngine;

namespace UnityUtils.Math {

public static class MathUtils {

    /// <summary>
    /// Calculates third side of triangle with sides a & b, separated by C degrees
    /// </summary>
    public static float LawOfCosines(float a, float b, float C) {
        float csq = a * a + b * b - 2 * a * b * Mathf.Cos(C * Mathf.Deg2Rad);
        return Mathf.Sqrt(csq);
    }

    //https://math.stackexchange.com/questions/4108428/how-to-project-vector-onto-a-plane-but-not-along-plane-normal
    /// <summary>
    /// Projects a vector onto a plane along an arbitrary direction.
    /// </summary>
    public static Vector3 ProjectOntoPlane(Vector3 vector, Vector3 planeNormal, Vector3 direction) {
        float numerator = Vector3.Dot(vector, planeNormal);
        float denominator = Vector3.Dot(direction, planeNormal);
        if (denominator == 0f) {
            //vector already inside plane
            return vector;
        }

        Vector3 orthogonalComponent = (numerator / denominator) * direction;
        return vector - orthogonalComponent;
    }
}
}