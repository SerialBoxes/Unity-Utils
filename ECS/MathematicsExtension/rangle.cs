using System;
using System.Runtime.CompilerServices;


namespace Unity.Mathematics {

/// <summary>
/// A float that can only have a value between -π and π
/// </summary>
[Serializable]
public struct rangle : IEquatable<rangle>, IFormattable {

    /// <summary>
    /// The raw float value of the rangle.
    /// </summary>
    public float value;

    /// <summary>rangle zero value.</summary>
    public static readonly rangle zero = new rangle();

    /// <summary>
    /// The maximum finite rangle value as a single precision float.
    /// </summary>
    public static float MaxValue {
        get { return math.PI - math.EPSILON; }
    }

    /// <summary>
    /// The minimum finite rangle value as a single precision float.
    /// </summary>
    public static float MinValue {
        get { return -math.PI; }
    }

    /// <summary>
    /// The maximum finite rangle value as a rangle.
    /// </summary>
    public static rangle MaxValueAsRangle => new rangle(MaxValue);

    /// <summary>
    /// The minimum finite rangle value as a rangle.
    /// </summary>
    public static rangle MinValueAsRangle => new rangle(MinValue);

    /// <summary>Constructs a rangle value from a rangle value.</summary>
    /// <param name="x">The input rangle value to copy.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public rangle(rangle x) {
        value = x.value;
    }

    /// <summary>Constructs a rangle value from a float value.</summary>
    /// <param name="v">The single precision float value to convert to rangle.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public rangle(float v) {
        value = Mod2PISigned(v);
    }

    /// <summary>Constructs a half value from a double value.</summary>
    /// <param name="v">The double precision float value to convert to half.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public rangle(double v) {
        value = Mod2PISigned(v);
    }
    
    /// <summary>Explicitly converts a float value to a rangle value.</summary>
    /// <param name="v">The single precision float value to convert to rangle.</param>
    /// <returns>The converted rangle value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator rangle(float v) { return new rangle(v); }

    /// <summary>Explicitly converts a double value to a rangle value.</summary>
    /// <param name="v">The double precision float value to convert to rangle.</param>
    /// <returns>The converted rangle value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator rangle(double v) { return new rangle(v); }
    
    /// <summary>Explicitly converts a dangle value to a rangle value.</summary>
    /// <param name="v">The dangle value to convert to rangle.</param>
    /// <returns>The converted rangle value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator rangle(dangle v) { return new rangle(v.value * math.TORADIANS); }

    /// <summary>Implicitly converts a rangle value to a float value.</summary>
    /// <param name="d">The rangle value to convert to a single precision float.</param>
    /// <returns>The converted single precision float value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator float(rangle d) { return d.value; }

    /// <summary>Implicitly converts a rangle value to a double value.</summary>
    /// <param name="d">The rangle value to convert to double precision float.</param>
    /// <returns>The converted double precision float value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator double(rangle d) { return d.value; }
    
    /// <summary>Returns the result of a multiplication operation on two rangles.</summary>
    /// <param name="lhs">Left hand side rangle to use to compute multiplication.</param>
    /// <param name="rhs">Right hand side rangle to use to compute multiplication.</param>
    /// <returns>rangle result of the multiplication.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static rangle operator * (rangle lhs, rangle rhs) { return new rangle (lhs.value * rhs.value); }
    
    /// <summary>Returns the result of a multiplication operation on a rangle and a float value.</summary>
    /// <param name="lhs">Left hand side rangle to use to compute multiplication.</param>
    /// <param name="rhs">Right hand side float to use to compute multiplication.</param>
    /// <returns>rangle result of the multiplication.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static rangle operator * (float lhs, rangle rhs) { return new rangle (lhs * rhs.value); }
    
    /// <summary>Returns the result of a multiplication operation on a float value and a rangle.</summary>
    /// <param name="lhs">Left hand side float to use to compute multiplication.</param>
    /// <param name="rhs">Right hand side rangle to use to compute multiplication.</param>
    /// <returns>rangle result of the multiplication.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static rangle operator * (rangle lhs, float rhs) { return new rangle (lhs.value * rhs); }
    
    /// <summary>Returns the result of an addition operation on two rangles.</summary>
    /// <param name="lhs">Left hand side rangle to use to compute addition.</param>
    /// <param name="rhs">Right hand side rangle to use to compute addition.</param>
    /// <returns>rangle result of the addition.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static rangle operator + (rangle lhs, rangle rhs) { return new rangle (lhs.value + rhs.value); }
    
    /// <summary>Returns the result of an addition operation on a rangle and a float value.</summary>
    /// <param name="lhs">Left hand side rangle to use to compute addition.</param>
    /// <param name="rhs">Right hand side float to use to compute addition.</param>
    /// <returns>rangle result of the addition.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static rangle operator + (float lhs, rangle rhs) { return new rangle (lhs + rhs.value); }
    
    /// <summary>Returns the result of an addition operation on a float value and a rangle.</summary>
    /// <param name="lhs">Left hand side float to use to compute addition.</param>
    /// <param name="rhs">Right hand side rangle to use to compute addition.</param>
    /// <returns>rangle result of the addition.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static rangle operator + (rangle lhs, float rhs) { return new rangle (lhs.value + rhs); }
    
    /// <summary>Returns the result of a division operation on two rangles.</summary>
    /// <param name="lhs">Left hand side rangle to use to compute division.</param>
    /// <param name="rhs">Right hand side rangle to use to compute division.</param>
    /// <returns>rangle result of the division.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static rangle operator / (rangle lhs, rangle rhs) { return new rangle (lhs.value / rhs.value); }
    
    /// <summary>Returns the result of a division operation on a rangle and a float value.</summary>
    /// <param name="lhs">Left hand side rangle to use to compute division.</param>
    /// <param name="rhs">Right hand side float to use to compute division.</param>
    /// <returns>rangle result of the division.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static rangle operator / (float lhs, rangle rhs) { return new rangle (lhs / rhs.value); }
    
    /// <summary>Returns the result of a division operation on a float value and a rangle.</summary>
    /// <param name="lhs">Left hand side float to use to compute division.</param>
    /// <param name="rhs">Right hand side rangle to use to compute division.</param>
    /// <returns>rangle result of the division.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static rangle operator / (rangle lhs, float rhs) { return new rangle (lhs.value / rhs); }
    
    /// <summary>Returns the result of a subtraction operation on two rangles.</summary>
    /// <param name="lhs">Left hand side rangle to use to compute subtraction.</param>
    /// <param name="rhs">Right hand side rangle to use to compute subtraction.</param>
    /// <returns>rangle result of the subtraction.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static rangle operator - (rangle lhs, rangle rhs) { return new rangle (lhs.value - rhs.value); }
    
    /// <summary>Returns the result of a subtraction operation on a rangle and a float value.</summary>
    /// <param name="lhs">Left hand side rangle to use to compute subtraction.</param>
    /// <param name="rhs">Right hand side float to use to compute subtraction.</param>
    /// <returns>rangle result of the subtraction.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static rangle operator - (float lhs, rangle rhs) { return new rangle (lhs - rhs.value); }
    
    /// <summary>Returns the result of a subtraction operation on a float value and a rangle.</summary>
    /// <param name="lhs">Left hand side float to use to compute subtraction.</param>
    /// <param name="rhs">Right hand side rangle to use to compute subtraction.</param>
    /// <returns>rangle result of the subtraction.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static rangle operator - (rangle lhs, float rhs) { return new rangle (lhs.value - rhs); }
    
    /// <summary>Returns the result of a modulus operation on two rangles.</summary>
    /// <param name="lhs">Left hand side rangle to use to compute modulus.</param>
    /// <param name="rhs">Right hand side rangle to use to compute modulus.</param>
    /// <returns>rangle result of the modulus.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static rangle operator % (rangle lhs, rangle rhs) { return new rangle (lhs.value % rhs.value); }
    
    /// <summary>Returns the result of a modulus operation on a rangle and a float value.</summary>
    /// <param name="lhs">Left hand side rangle to use to compute modulus.</param>
    /// <param name="rhs">Right hand side float to use to compute modulus.</param>
    /// <returns>rangle result of the modulus.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static rangle operator % (float lhs, rangle rhs) { return new rangle (lhs % rhs.value); }
    
    /// <summary>Returns the result of a modulus operation on a float value and a rangle.</summary>
    /// <param name="lhs">Left hand side float to use to compute modulus.</param>
    /// <param name="rhs">Right hand side rangle to use to compute modulus.</param>
    /// <returns>rangle result of the modulus.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static rangle operator % (rangle lhs, float rhs) { return new rangle (lhs.value % rhs); }
    
    /// <summary>Returns the result of an increment operation on a rangle. Rangles are incremented by π.</summary>
    /// <param name="val">Value to use when computing the increment.</param>
    /// <returns>rangle result of the increment.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static rangle operator ++ (rangle val) { return new rangle (val.value + math.PI); }


    /// <summary>Returns the result of a decrement operation on a rangle. Rangles are decremented by π.</summary>
    /// <param name="val">Value to use when computing the decrement.</param>
    /// <returns>rangle result of the decrement.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static rangle operator -- (rangle val) { return new rangle (val.value - math.PI); }
    
    /// <summary>Returns whether two rangle values are bitwise equivalent.</summary>
    /// <param name="lhs">Left hand side rangle value to use in comparison.</param>
    /// <param name="rhs">Right hand side rangle value to use in comparison.</param>
    /// <returns>True if the two rangle values are bitwise equivalent, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(rangle lhs, rangle rhs) { return lhs.value == rhs.value; }
    
    /// <summary>Returns whether a float value and a rangle are bitwise equivalent.</summary>
    /// <param name="lhs">Left hand side float value to use in comparison.</param>
    /// <param name="rhs">Right hand side rangle to use in comparison.</param>
    /// <returns>True if the float value and the rangle are bitwise equivalent, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(float lhs, rangle rhs) { return lhs == rhs.value; }
    
    /// <summary>Returns whether a rangle and a float value are bitwise equivalent.</summary>
    /// <param name="lhs">Left hand side rangle to use in comparison.</param>
    /// <param name="rhs">Right hand side float value to use in comparison.</param>
    /// <returns>True if the rangle and the float value are bitwise equivalent, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(rangle lhs, float rhs) { return lhs.value == rhs; }

    /// <summary>Returns whether two rangle values are not bitwise equivalent.</summary>
    /// <param name="lhs">Left hand side rangle value to use in comparison.</param>
    /// <param name="rhs">Right hand side rangle value to use in comparison.</param>
    /// <returns>True if the two rangle values are not bitwise equivalent, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(rangle lhs, rangle rhs) { return lhs.value != rhs.value; }
    
    /// <summary>Returns whether a float value and a rangle are not bitwise equivalent.</summary>
    /// <param name="lhs">Left hand side float value to use in comparison.</param>
    /// <param name="rhs">Right hand side rangle to use in comparison.</param>
    /// <returns>True if the float value and the rangle are not bitwise equivalent, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(float lhs, rangle rhs) { return lhs != rhs.value; }
    
    /// <summary>Returns whether a rangle and a float value are not bitwise equivalent.</summary>
    /// <param name="lhs">Left hand side rangle to use in comparison.</param>
    /// <param name="rhs">Right hand side float value to use in comparison.</param>
    /// <returns>True if the rangle and the float value are not bitwise equivalent, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(rangle lhs, float rhs) { return lhs.value != rhs; }
    
    /// <summary>Returns the result of a less than operation on two rangles.</summary>
    /// <param name="lhs">Left hand side rangle to use to compute less than.</param>
    /// <param name="rhs">Right hand side rangle to use to compute less than.</param>
    /// <returns>bool result of the less than.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator < (rangle lhs, rangle rhs) { return lhs.value < rhs.value; }
    
    /// <summary>Returns the result of a less than operation on a rangle and a float value.</summary>
    /// <param name="lhs">Left hand side rangle to use to compute less than.</param>
    /// <param name="rhs">Right hand side float to use to compute less than.</param>
    /// <returns>bool result of the less than.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator < (float lhs, rangle rhs) { return lhs < rhs.value; }
    
    /// <summary>Returns the result of a less than operation on a float value and a rangle.</summary>
    /// <param name="lhs">Left hand side float to use to compute less than.</param>
    /// <param name="rhs">Right hand side rangle to use to compute less than.</param>
    /// <returns>bool result of the less than.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator < (rangle lhs, float rhs) { return lhs.value < rhs; }
    
    /// <summary>Returns the result of a less or equal operation on two rangles.</summary>
    /// <param name="lhs">Left hand side rangle to use to compute less or equal.</param>
    /// <param name="rhs">Right hand side rangle to use to compute less or equal.</param>
    /// <returns>bool result of the less or equal.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <= (rangle lhs, rangle rhs) { return lhs.value <= rhs.value; }
    
    /// <summary>Returns the result of a less or equal operation on a rangle and a float value.</summary>
    /// <param name="lhs">Left hand side rangle to use to compute less or equal.</param>
    /// <param name="rhs">Right hand side float to use to compute less or equal.</param>
    /// <returns>bool result of the less or equal.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <= (float lhs, rangle rhs) { return lhs <= rhs.value; }
    
    /// <summary>Returns the result of a less or equal operation on a float value and a rangle.</summary>
    /// <param name="lhs">Left hand side float to use to compute less or equal.</param>
    /// <param name="rhs">Right hand side rangle to use to compute less or equal.</param>
    /// <returns>bool result of the less or equal.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <= (rangle lhs, float rhs) { return lhs.value <= rhs; }
    
    /// <summary>Returns the result of a greater than operation on two rangles.</summary>
    /// <param name="lhs">Left hand side rangle to use to compute greater than.</param>
    /// <param name="rhs">Right hand side rangle to use to compute greater than.</param>
    /// <returns>bool result of the greater than.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator > (rangle lhs, rangle rhs) { return lhs.value > rhs.value; }
    
    /// <summary>Returns the result of a greater than operation on a rangle and a float value.</summary>
    /// <param name="lhs">Left hand side rangle to use to compute greater than.</param>
    /// <param name="rhs">Right hand side float to use to compute greater than.</param>
    /// <returns>bool result of the greater than.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator > (float lhs, rangle rhs) { return lhs > rhs.value; }
    
    /// <summary>Returns the result of a greater than operation on a float value and a rangle.</summary>
    /// <param name="lhs">Left hand side float to use to compute greater than.</param>
    /// <param name="rhs">Right hand side rangle to use to compute greater than.</param>
    /// <returns>bool result of the greater than.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator > (rangle lhs, float rhs) { return lhs.value > rhs; }
    
    /// <summary>Returns the result of a greater or equal operation on two rangles.</summary>
    /// <param name="lhs">Left hand side rangle to use to compute greater or equal.</param>
    /// <param name="rhs">Right hand side rangle to use to compute greater or equal.</param>
    /// <returns>bool result of the greater or equal.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >= (rangle lhs, rangle rhs) { return lhs.value >= rhs.value; }
    
    /// <summary>Returns the result of a greater or equal operation on a rangle and a float value.</summary>
    /// <param name="lhs">Left hand side rangle to use to compute greater or equal.</param>
    /// <param name="rhs">Right hand side float to use to compute greater or equal.</param>
    /// <returns>bool result of the greater or equal.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >= (float lhs, rangle rhs) { return lhs >= rhs.value; }
    
    /// <summary>Returns the result of a greater or equal operation on a float value and a rangle.</summary>
    /// <param name="lhs">Left hand side float to use to compute greater or equal.</param>
    /// <param name="rhs">Right hand side rangle to use to compute greater or equal.</param>
    /// <returns>bool result of the greater or equal.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >= (rangle lhs, float rhs) { return lhs.value >= rhs; }
    
    /// <summary>Returns the result of a unary minus operation on a rangle.</summary>
    /// <param name="val">Value to use when computing the unary minus.</param>
    /// <returns>rangle result of the unary minus.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static rangle operator - (rangle val) { return new rangle (-val.value); }


    /// <summary>Returns the result of a unary plus operation on a rangle.</summary>
    /// <param name="val">Value to use when computing the unary plus.</param>
    /// <returns>rangle result of the unary plus.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static rangle operator + (rangle val) { return new rangle (+val.value); }
    
    /// <summary>Returns true if the rangle is bitwise equivalent to a given rangle, false otherwise.</summary>
    /// <param name="rhs">Right hand side rangle value to use in comparison.</param>
    /// <returns>True if the rangle value is bitwise equivalent to the input, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(rangle rhs) { return value == rhs.value; }

    /// <summary>Returns true if the rangle is equal to a given rangle, false otherwise.</summary>
    /// <param name="o">Right hand side object to use in comparison.</param>
    /// <returns>True if the object is of type rangle and is bitwise equivalent, false otherwise.</returns>
    public override bool Equals(object o) { return o is rangle converted && Equals(converted); }
    
    /// <summary>Returns a hash code for the rangle.</summary>
    /// <returns>The computed hash code of the rangle.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode() { return (int)(value*1000); }
    
    /// <summary>Returns a string representation of the rangle.</summary>
    /// <returns>The string representation of the rangle.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString(){
        return (value/math.PI)+"π";
    }
    
    /// <summary>Returns a string representation of the rangle using a specified format and culture-specific format information.</summary>
    /// <param name="format">The format string to use during string formatting.</param>
    /// <param name="formatProvider">The format provider to use during string formatting.</param>
    /// <returns>The string representation of the half.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToString(string format, IFormatProvider formatProvider)
    {
        return (value/math.PI).ToString(format, formatProvider)+"π";
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static float Mod2PISigned(float a) {
        return a - math.TAU * math.floor((a + math.PI) / math.TAU);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static float Mod2PISigned(double a) {
        return (float)(a - math.TAU_DBL * math.floor((a + math.PI_DBL) / math.TAU_DBL));
    }
}
}