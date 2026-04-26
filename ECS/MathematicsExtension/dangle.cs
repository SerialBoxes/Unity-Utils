using System;
using System.Runtime.CompilerServices;


namespace Unity.Mathematics {

/// <summary>
/// A float that can only have a value between -180 and 180
/// </summary>
[Serializable]
public struct dangle : IEquatable<dangle>, IFormattable {

    /// <summary>
    /// The raw float value of the dangle.
    /// </summary>
    public float value;

    /// <summary>dangle zero value.</summary>
    public static readonly dangle zero = new dangle();

    /// <summary>
    /// The maximum finite dangle value as a single precision float.
    /// </summary>
    public static float MaxValue {
        get { return 180f - math.EPSILON; }
    }

    /// <summary>
    /// The minimum finite dangle value as a single precision float.
    /// </summary>
    public static float MinValue {
        get { return -180f; }
    }

    /// <summary>
    /// The maximum finite dangle value as a dangle.
    /// </summary>
    public static dangle MaxValueAsdangle => new dangle(MaxValue);

    /// <summary>
    /// The minimum finite dangle value as a dangle.
    /// </summary>
    public static dangle MinValueAsdangle => new dangle(MinValue);

    /// <summary>Constructs a dangle value from a dangle value.</summary>
    /// <param name="x">The input dangle value to copy.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public dangle(dangle x) {
        value = x.value;
    }

    /// <summary>Constructs a dangle value from a float value.</summary>
    /// <param name="v">The single precision float value to convert to dangle.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public dangle(float v) {
        value = Mod360Signed(v);
    }

    /// <summary>Constructs a half value from a double value.</summary>
    /// <param name="v">The double precision float value to convert to half.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public dangle(double v) {
        value = Mod360Signed(v);
    }
    
    /// <summary>Explicitly converts a float value to a dangle value.</summary>
    /// <param name="v">The single precision float value to convert to dangle.</param>
    /// <returns>The converted dangle value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator dangle(float v) { return new dangle(v); }

    /// <summary>Explicitly converts a double value to a dangle value.</summary>
    /// <param name="v">The double precision float value to convert to dangle.</param>
    /// <returns>The converted dangle value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator dangle(double v) { return new dangle(v); }
    
    /// <summary>Explicitly converts a rangle value to a dangle value.</summary>
    /// <param name="v">The rangle value to convert to dangle.</param>
    /// <returns>The converted dangle value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator dangle(rangle v) { return new dangle(v.value * math.TODEGREES); }

    /// <summary>Implicitly converts a dangle value to a float value.</summary>
    /// <param name="d">The dangle value to convert to a single precision float.</param>
    /// <returns>The converted single precision float value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator float(dangle d) { return d.value; }

    /// <summary>Implicitly converts a dangle value to a double value.</summary>
    /// <param name="d">The dangle value to convert to double precision float.</param>
    /// <returns>The converted double precision float value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator double(dangle d) { return d.value; }
    
    /// <summary>Returns the result of a multiplication operation on two dangles.</summary>
    /// <param name="lhs">Left hand side dangle to use to compute multiplication.</param>
    /// <param name="rhs">Right hand side dangle to use to compute multiplication.</param>
    /// <returns>dangle result of the multiplication.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static dangle operator * (dangle lhs, dangle rhs) { return new dangle (lhs.value * rhs.value); }
    
    /// <summary>Returns the result of a multiplication operation on a dangle and a float value.</summary>
    /// <param name="lhs">Left hand side dangle to use to compute multiplication.</param>
    /// <param name="rhs">Right hand side float to use to compute multiplication.</param>
    /// <returns>dangle result of the multiplication.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static dangle operator * (float lhs, dangle rhs) { return new dangle (lhs * rhs.value); }
    
    /// <summary>Returns the result of a multiplication operation on a float value and a dangle.</summary>
    /// <param name="lhs">Left hand side float to use to compute multiplication.</param>
    /// <param name="rhs">Right hand side dangle to use to compute multiplication.</param>
    /// <returns>dangle result of the multiplication.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static dangle operator * (dangle lhs, float rhs) { return new dangle (lhs.value * rhs); }
    
    /// <summary>Returns the result of an addition operation on two dangles.</summary>
    /// <param name="lhs">Left hand side dangle to use to compute addition.</param>
    /// <param name="rhs">Right hand side dangle to use to compute addition.</param>
    /// <returns>dangle result of the addition.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static dangle operator + (dangle lhs, dangle rhs) { return new dangle (lhs.value + rhs.value); }
    
    /// <summary>Returns the result of an addition operation on a dangle and a float value.</summary>
    /// <param name="lhs">Left hand side dangle to use to compute addition.</param>
    /// <param name="rhs">Right hand side float to use to compute addition.</param>
    /// <returns>dangle result of the addition.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static dangle operator + (float lhs, dangle rhs) { return new dangle (lhs + rhs.value); }
    
    /// <summary>Returns the result of an addition operation on a float value and a dangle.</summary>
    /// <param name="lhs">Left hand side float to use to compute addition.</param>
    /// <param name="rhs">Right hand side dangle to use to compute addition.</param>
    /// <returns>dangle result of the addition.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static dangle operator + (dangle lhs, float rhs) { return new dangle (lhs.value + rhs); }
    
    /// <summary>Returns the result of a division operation on two dangles.</summary>
    /// <param name="lhs">Left hand side dangle to use to compute division.</param>
    /// <param name="rhs">Right hand side dangle to use to compute division.</param>
    /// <returns>dangle result of the division.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static dangle operator / (dangle lhs, dangle rhs) { return new dangle (lhs.value / rhs.value); }
    
    /// <summary>Returns the result of a division operation on a dangle and a float value.</summary>
    /// <param name="lhs">Left hand side dangle to use to compute division.</param>
    /// <param name="rhs">Right hand side float to use to compute division.</param>
    /// <returns>dangle result of the division.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static dangle operator / (float lhs, dangle rhs) { return new dangle (lhs / rhs.value); }
    
    /// <summary>Returns the result of a division operation on a float value and a dangle.</summary>
    /// <param name="lhs">Left hand side float to use to compute division.</param>
    /// <param name="rhs">Right hand side dangle to use to compute division.</param>
    /// <returns>dangle result of the division.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static dangle operator / (dangle lhs, float rhs) { return new dangle (lhs.value / rhs); }
    
    /// <summary>Returns the result of a subtraction operation on two dangles.</summary>
    /// <param name="lhs">Left hand side dangle to use to compute subtraction.</param>
    /// <param name="rhs">Right hand side dangle to use to compute subtraction.</param>
    /// <returns>dangle result of the subtraction.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static dangle operator - (dangle lhs, dangle rhs) { return new dangle (lhs.value - rhs.value); }
    
    /// <summary>Returns the result of a subtraction operation on a dangle and a float value.</summary>
    /// <param name="lhs">Left hand side dangle to use to compute subtraction.</param>
    /// <param name="rhs">Right hand side float to use to compute subtraction.</param>
    /// <returns>dangle result of the subtraction.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static dangle operator - (float lhs, dangle rhs) { return new dangle (lhs - rhs.value); }
    
    /// <summary>Returns the result of a subtraction operation on a float value and a dangle.</summary>
    /// <param name="lhs">Left hand side float to use to compute subtraction.</param>
    /// <param name="rhs">Right hand side dangle to use to compute subtraction.</param>
    /// <returns>dangle result of the subtraction.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static dangle operator - (dangle lhs, float rhs) { return new dangle (lhs.value - rhs); }
    
    /// <summary>Returns the result of a modulus operation on two dangles.</summary>
    /// <param name="lhs">Left hand side dangle to use to compute modulus.</param>
    /// <param name="rhs">Right hand side dangle to use to compute modulus.</param>
    /// <returns>dangle result of the modulus.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static dangle operator % (dangle lhs, dangle rhs) { return new dangle (lhs.value % rhs.value); }
    
    /// <summary>Returns the result of a modulus operation on a dangle and a float value.</summary>
    /// <param name="lhs">Left hand side dangle to use to compute modulus.</param>
    /// <param name="rhs">Right hand side float to use to compute modulus.</param>
    /// <returns>dangle result of the modulus.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static dangle operator % (float lhs, dangle rhs) { return new dangle (lhs % rhs.value); }
    
    /// <summary>Returns the result of a modulus operation on a float value and a dangle.</summary>
    /// <param name="lhs">Left hand side float to use to compute modulus.</param>
    /// <param name="rhs">Right hand side dangle to use to compute modulus.</param>
    /// <returns>dangle result of the modulus.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static dangle operator % (dangle lhs, float rhs) { return new dangle (lhs.value % rhs); }
    
    /// <summary>Returns the result of an increment operation on a dangle.</summary>
    /// <param name="val">Value to use when computing the increment.</param>
    /// <returns>dangle result of the increment.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static dangle operator ++ (dangle val) { return new dangle (++val.value); }


    /// <summary>Returns the result of a decrement operation on a dangle.</summary>
    /// <param name="val">Value to use when computing the decrement.</param>
    /// <returns>dangle result of the decrement.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static dangle operator -- (dangle val) { return new dangle (--val.value); }
    
    /// <summary>Returns whether two dangle values are bitwise equivalent.</summary>
    /// <param name="lhs">Left hand side dangle value to use in comparison.</param>
    /// <param name="rhs">Right hand side dangle value to use in comparison.</param>
    /// <returns>True if the two dangle values are bitwise equivalent, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(dangle lhs, dangle rhs) { return lhs.value == rhs.value; }
    
    /// <summary>Returns whether a float value and a dangle are bitwise equivalent.</summary>
    /// <param name="lhs">Left hand side float value to use in comparison.</param>
    /// <param name="rhs">Right hand side dangle to use in comparison.</param>
    /// <returns>True if the float value and the dangle are bitwise equivalent, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(float lhs, dangle rhs) { return lhs == rhs.value; }
    
    /// <summary>Returns whether a dangle and a float value are bitwise equivalent.</summary>
    /// <param name="lhs">Left hand side dangle to use in comparison.</param>
    /// <param name="rhs">Right hand side float value to use in comparison.</param>
    /// <returns>True if the dangle and the float value are bitwise equivalent, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(dangle lhs, float rhs) { return lhs.value == rhs; }

    /// <summary>Returns whether two dangle values are not bitwise equivalent.</summary>
    /// <param name="lhs">Left hand side dangle value to use in comparison.</param>
    /// <param name="rhs">Right hand side dangle value to use in comparison.</param>
    /// <returns>True if the two dangle values are not bitwise equivalent, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(dangle lhs, dangle rhs) { return lhs.value != rhs.value; }
    
    /// <summary>Returns whether a float value and a dangle are not bitwise equivalent.</summary>
    /// <param name="lhs">Left hand side float value to use in comparison.</param>
    /// <param name="rhs">Right hand side dangle to use in comparison.</param>
    /// <returns>True if the float value and the dangle are not bitwise equivalent, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(float lhs, dangle rhs) { return lhs != rhs.value; }
    
    /// <summary>Returns whether a dangle and a float value are not bitwise equivalent.</summary>
    /// <param name="lhs">Left hand side dangle to use in comparison.</param>
    /// <param name="rhs">Right hand side float value to use in comparison.</param>
    /// <returns>True if the dangle and the float value are not bitwise equivalent, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(dangle lhs, float rhs) { return lhs.value != rhs; }
    
    /// <summary>Returns the result of a less than operation on two dangles.</summary>
    /// <param name="lhs">Left hand side dangle to use to compute less than.</param>
    /// <param name="rhs">Right hand side dangle to use to compute less than.</param>
    /// <returns>bool result of the less than.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator < (dangle lhs, dangle rhs) { return lhs.value < rhs.value; }
    
    /// <summary>Returns the result of a less than operation on a dangle and a float value.</summary>
    /// <param name="lhs">Left hand side dangle to use to compute less than.</param>
    /// <param name="rhs">Right hand side float to use to compute less than.</param>
    /// <returns>bool result of the less than.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator < (float lhs, dangle rhs) { return lhs < rhs.value; }
    
    /// <summary>Returns the result of a less than operation on a float value and a dangle.</summary>
    /// <param name="lhs">Left hand side float to use to compute less than.</param>
    /// <param name="rhs">Right hand side dangle to use to compute less than.</param>
    /// <returns>bool result of the less than.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator < (dangle lhs, float rhs) { return lhs.value < rhs; }
    
    /// <summary>Returns the result of a less or equal operation on two dangles.</summary>
    /// <param name="lhs">Left hand side dangle to use to compute less or equal.</param>
    /// <param name="rhs">Right hand side dangle to use to compute less or equal.</param>
    /// <returns>bool result of the less or equal.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <= (dangle lhs, dangle rhs) { return lhs.value <= rhs.value; }
    
    /// <summary>Returns the result of a less or equal operation on a dangle and a float value.</summary>
    /// <param name="lhs">Left hand side dangle to use to compute less or equal.</param>
    /// <param name="rhs">Right hand side float to use to compute less or equal.</param>
    /// <returns>bool result of the less or equal.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <= (float lhs, dangle rhs) { return lhs <= rhs.value; }
    
    /// <summary>Returns the result of a less or equal operation on a float value and a dangle.</summary>
    /// <param name="lhs">Left hand side float to use to compute less or equal.</param>
    /// <param name="rhs">Right hand side dangle to use to compute less or equal.</param>
    /// <returns>bool result of the less or equal.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <= (dangle lhs, float rhs) { return lhs.value <= rhs; }
    
    /// <summary>Returns the result of a greater than operation on two dangles.</summary>
    /// <param name="lhs">Left hand side dangle to use to compute greater than.</param>
    /// <param name="rhs">Right hand side dangle to use to compute greater than.</param>
    /// <returns>bool result of the greater than.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator > (dangle lhs, dangle rhs) { return lhs.value > rhs.value; }
    
    /// <summary>Returns the result of a greater than operation on a dangle and a float value.</summary>
    /// <param name="lhs">Left hand side dangle to use to compute greater than.</param>
    /// <param name="rhs">Right hand side float to use to compute greater than.</param>
    /// <returns>bool result of the greater than.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator > (float lhs, dangle rhs) { return lhs > rhs.value; }
    
    /// <summary>Returns the result of a greater than operation on a float value and a dangle.</summary>
    /// <param name="lhs">Left hand side float to use to compute greater than.</param>
    /// <param name="rhs">Right hand side dangle to use to compute greater than.</param>
    /// <returns>bool result of the greater than.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator > (dangle lhs, float rhs) { return lhs.value > rhs; }
    
    /// <summary>Returns the result of a greater or equal operation on two dangles.</summary>
    /// <param name="lhs">Left hand side dangle to use to compute greater or equal.</param>
    /// <param name="rhs">Right hand side dangle to use to compute greater or equal.</param>
    /// <returns>bool result of the greater or equal.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >= (dangle lhs, dangle rhs) { return lhs.value >= rhs.value; }
    
    /// <summary>Returns the result of a greater or equal operation on a dangle and a float value.</summary>
    /// <param name="lhs">Left hand side dangle to use to compute greater or equal.</param>
    /// <param name="rhs">Right hand side float to use to compute greater or equal.</param>
    /// <returns>bool result of the greater or equal.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >= (float lhs, dangle rhs) { return lhs >= rhs.value; }
    
    /// <summary>Returns the result of a greater or equal operation on a float value and a dangle.</summary>
    /// <param name="lhs">Left hand side float to use to compute greater or equal.</param>
    /// <param name="rhs">Right hand side dangle to use to compute greater or equal.</param>
    /// <returns>bool result of the greater or equal.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >= (dangle lhs, float rhs) { return lhs.value >= rhs; }
    
    /// <summary>Returns the result of a unary minus operation on a dangle.</summary>
    /// <param name="val">Value to use when computing the unary minus.</param>
    /// <returns>dangle result of the unary minus.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static dangle operator - (dangle val) { return new dangle (-val.value); }


    /// <summary>Returns the result of a unary plus operation on a dangle.</summary>
    /// <param name="val">Value to use when computing the unary plus.</param>
    /// <returns>dangle result of the unary plus.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static dangle operator + (dangle val) { return new dangle (+val.value); }
    
    /// <summary>Returns true if the dangle is bitwise equivalent to a given dangle, false otherwise.</summary>
    /// <param name="rhs">Right hand side dangle value to use in comparison.</param>
    /// <returns>True if the dangle value is bitwise equivalent to the input, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(dangle rhs) { return value == rhs.value; }

    /// <summary>Returns true if the dangle is equal to a given dangle, false otherwise.</summary>
    /// <param name="o">Right hand side object to use in comparison.</param>
    /// <returns>True if the object is of type dangle and is bitwise equivalent, false otherwise.</returns>
    public override bool Equals(object o) { return o is dangle converted && Equals(converted); }
    
    /// <summary>Returns a hash code for the dangle.</summary>
    /// <returns>The computed hash code of the dangle.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode() { return (int)(value*1000); }
    
    /// <summary>Returns a string representation of the dangle.</summary>
    /// <returns>The string representation of the dangle.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString(){
        return value+"°";
    }
    
    /// <summary>Returns a string representation of the dangle using a specified format and culture-specific format information.</summary>
    /// <param name="format">The format string to use during string formatting.</param>
    /// <param name="formatProvider">The format provider to use during string formatting.</param>
    /// <returns>The string representation of the half.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToString(string format, IFormatProvider formatProvider)
    {
        return value.ToString(format, formatProvider)+"°";
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static float Mod360Signed(float a) {
        return a - 360f * math.floor((a + 180f) / 360f);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static float Mod360Signed(double a) {
        return (float)(a - 360d * math.floor((a + 180d) / 360d));
    }
}
}