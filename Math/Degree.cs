using System;
using System.Runtime.CompilerServices;
using UnityEngine;


namespace NoodleKit.Math {

/// <summary>
/// A float that can only have a value between -180 and 180
/// </summary>
[Serializable]
public struct Degree : IEquatable<Degree>, IFormattable {

    /// <summary>
    /// The raw float value of the Degree.
    /// </summary>
    public float value;

    /// <summary>Degree zero value.</summary>
    public static readonly Degree zero = new Degree();

    /// <summary>
    /// The maximum finite Degree value as a single precision float.
    /// </summary>
    public static float MaxValue {
        get { return 180f - Mathf.Epsilon; }
    }

    /// <summary>
    /// The minimum finite Degree value as a single precision float.
    /// </summary>
    public static float MinValue {
        get { return -180f; }
    }

    /// <summary>
    /// The maximum finite Degree value as a Degree.
    /// </summary>
    public static Degree MaxValueAsDegree => new Degree(MaxValue);

    /// <summary>
    /// The minimum finite Degree value as a Degree.
    /// </summary>
    public static Degree MinValueAsDegree => new Degree(MinValue);

    /// <summary>Constructs a Degree value from a Degree value.</summary>
    /// <param name="x">The input Degree value to copy.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Degree(Degree x) {
        value = x.value;
    }

    /// <summary>Constructs a Degree value from a float value.</summary>
    /// <param name="v">The single precision float value to convert to Degree.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Degree(float v) {
        value = Mod360Signed(v);
    }

    /// <summary>Constructs a half value from a double value.</summary>
    /// <param name="v">The double precision float value to convert to half.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Degree(double v) {
        value = Mod360Signed(v);
    }
    
    /// <summary>Explicitly converts a float value to a Degree value.</summary>
    /// <param name="v">The single precision float value to convert to Degree.</param>
    /// <returns>The converted Degree value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator Degree(float v) { return new Degree(v); }

    /// <summary>Explicitly converts a double value to a Degree value.</summary>
    /// <param name="v">The double precision float value to convert to Degree.</param>
    /// <returns>The converted Degree value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator Degree(double v) { return new Degree(v); }
    
    /// <summary>Explicitly converts a Radian value to a Degree value.</summary>
    /// <param name="v">The Radian value to convert to Degree.</param>
    /// <returns>The converted Degree value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator Degree(Radian v) { return new Degree(v.value * Mathf.Rad2Deg); }

    /// <summary>Implicitly converts a Degree value to a float value.</summary>
    /// <param name="d">The Degree value to convert to a single precision float.</param>
    /// <returns>The converted single precision float value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator float(Degree d) { return d.value; }

    /// <summary>Implicitly converts a Degree value to a double value.</summary>
    /// <param name="d">The Degree value to convert to double precision float.</param>
    /// <returns>The converted double precision float value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator double(Degree d) { return d.value; }
    
    /// <summary>Returns the result of a multiplication operation on two Degrees.</summary>
    /// <param name="lhs">Left hand side Degree to use to compute multiplication.</param>
    /// <param name="rhs">Right hand side Degree to use to compute multiplication.</param>
    /// <returns>Degree result of the multiplication.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Degree operator * (Degree lhs, Degree rhs) { return new Degree (lhs.value * rhs.value); }
    
    /// <summary>Returns the result of a multiplication operation on a Degree and a float value.</summary>
    /// <param name="lhs">Left hand side Degree to use to compute multiplication.</param>
    /// <param name="rhs">Right hand side float to use to compute multiplication.</param>
    /// <returns>Degree result of the multiplication.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Degree operator * (float lhs, Degree rhs) { return new Degree (lhs * rhs.value); }
    
    /// <summary>Returns the result of a multiplication operation on a float value and a Degree.</summary>
    /// <param name="lhs">Left hand side float to use to compute multiplication.</param>
    /// <param name="rhs">Right hand side Degree to use to compute multiplication.</param>
    /// <returns>Degree result of the multiplication.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Degree operator * (Degree lhs, float rhs) { return new Degree (lhs.value * rhs); }
    
    /// <summary>Returns the result of an addition operation on two Degrees.</summary>
    /// <param name="lhs">Left hand side Degree to use to compute addition.</param>
    /// <param name="rhs">Right hand side Degree to use to compute addition.</param>
    /// <returns>Degree result of the addition.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Degree operator + (Degree lhs, Degree rhs) { return new Degree (lhs.value + rhs.value); }
    
    /// <summary>Returns the result of an addition operation on a Degree and a float value.</summary>
    /// <param name="lhs">Left hand side Degree to use to compute addition.</param>
    /// <param name="rhs">Right hand side float to use to compute addition.</param>
    /// <returns>Degree result of the addition.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Degree operator + (float lhs, Degree rhs) { return new Degree (lhs + rhs.value); }
    
    /// <summary>Returns the result of an addition operation on a float value and a Degree.</summary>
    /// <param name="lhs">Left hand side float to use to compute addition.</param>
    /// <param name="rhs">Right hand side Degree to use to compute addition.</param>
    /// <returns>Degree result of the addition.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Degree operator + (Degree lhs, float rhs) { return new Degree (lhs.value + rhs); }
    
    /// <summary>Returns the result of a division operation on two Degrees.</summary>
    /// <param name="lhs">Left hand side Degree to use to compute division.</param>
    /// <param name="rhs">Right hand side Degree to use to compute division.</param>
    /// <returns>Degree result of the division.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Degree operator / (Degree lhs, Degree rhs) { return new Degree (lhs.value / rhs.value); }
    
    /// <summary>Returns the result of a division operation on a Degree and a float value.</summary>
    /// <param name="lhs">Left hand side Degree to use to compute division.</param>
    /// <param name="rhs">Right hand side float to use to compute division.</param>
    /// <returns>Degree result of the division.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Degree operator / (float lhs, Degree rhs) { return new Degree (lhs / rhs.value); }
    
    /// <summary>Returns the result of a division operation on a float value and a Degree.</summary>
    /// <param name="lhs">Left hand side float to use to compute division.</param>
    /// <param name="rhs">Right hand side Degree to use to compute division.</param>
    /// <returns>Degree result of the division.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Degree operator / (Degree lhs, float rhs) { return new Degree (lhs.value / rhs); }
    
    /// <summary>Returns the result of a subtraction operation on two Degrees.</summary>
    /// <param name="lhs">Left hand side Degree to use to compute subtraction.</param>
    /// <param name="rhs">Right hand side Degree to use to compute subtraction.</param>
    /// <returns>Degree result of the subtraction.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Degree operator - (Degree lhs, Degree rhs) { return new Degree (lhs.value - rhs.value); }
    
    /// <summary>Returns the result of a subtraction operation on a Degree and a float value.</summary>
    /// <param name="lhs">Left hand side Degree to use to compute subtraction.</param>
    /// <param name="rhs">Right hand side float to use to compute subtraction.</param>
    /// <returns>Degree result of the subtraction.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Degree operator - (float lhs, Degree rhs) { return new Degree (lhs - rhs.value); }
    
    /// <summary>Returns the result of a subtraction operation on a float value and a Degree.</summary>
    /// <param name="lhs">Left hand side float to use to compute subtraction.</param>
    /// <param name="rhs">Right hand side Degree to use to compute subtraction.</param>
    /// <returns>Degree result of the subtraction.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Degree operator - (Degree lhs, float rhs) { return new Degree (lhs.value - rhs); }
    
    /// <summary>Returns the result of a modulus operation on two Degrees.</summary>
    /// <param name="lhs">Left hand side Degree to use to compute modulus.</param>
    /// <param name="rhs">Right hand side Degree to use to compute modulus.</param>
    /// <returns>Degree result of the modulus.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Degree operator % (Degree lhs, Degree rhs) { return new Degree (lhs.value % rhs.value); }
    
    /// <summary>Returns the result of a modulus operation on a Degree and a float value.</summary>
    /// <param name="lhs">Left hand side Degree to use to compute modulus.</param>
    /// <param name="rhs">Right hand side float to use to compute modulus.</param>
    /// <returns>Degree result of the modulus.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Degree operator % (float lhs, Degree rhs) { return new Degree (lhs % rhs.value); }
    
    /// <summary>Returns the result of a modulus operation on a float value and a Degree.</summary>
    /// <param name="lhs">Left hand side float to use to compute modulus.</param>
    /// <param name="rhs">Right hand side Degree to use to compute modulus.</param>
    /// <returns>Degree result of the modulus.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Degree operator % (Degree lhs, float rhs) { return new Degree (lhs.value % rhs); }
    
    /// <summary>Returns the result of an increment operation on a Degree.</summary>
    /// <param name="val">Value to use when computing the increment.</param>
    /// <returns>Degree result of the increment.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Degree operator ++ (Degree val) { return new Degree (++val.value); }


    /// <summary>Returns the result of a decrement operation on a Degree.</summary>
    /// <param name="val">Value to use when computing the decrement.</param>
    /// <returns>Degree result of the decrement.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Degree operator -- (Degree val) { return new Degree (--val.value); }
    
    /// <summary>Returns whether two Degree values are bitwise equivalent.</summary>
    /// <param name="lhs">Left hand side Degree value to use in comparison.</param>
    /// <param name="rhs">Right hand side Degree value to use in comparison.</param>
    /// <returns>True if the two Degree values are bitwise equivalent, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Degree lhs, Degree rhs) { return lhs.value == rhs.value; }
    
    /// <summary>Returns whether a float value and a Degree are bitwise equivalent.</summary>
    /// <param name="lhs">Left hand side float value to use in comparison.</param>
    /// <param name="rhs">Right hand side Degree to use in comparison.</param>
    /// <returns>True if the float value and the Degree are bitwise equivalent, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(float lhs, Degree rhs) { return lhs == rhs.value; }
    
    /// <summary>Returns whether a Degree and a float value are bitwise equivalent.</summary>
    /// <param name="lhs">Left hand side Degree to use in comparison.</param>
    /// <param name="rhs">Right hand side float value to use in comparison.</param>
    /// <returns>True if the Degree and the float value are bitwise equivalent, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Degree lhs, float rhs) { return lhs.value == rhs; }

    /// <summary>Returns whether two Degree values are not bitwise equivalent.</summary>
    /// <param name="lhs">Left hand side Degree value to use in comparison.</param>
    /// <param name="rhs">Right hand side Degree value to use in comparison.</param>
    /// <returns>True if the two Degree values are not bitwise equivalent, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Degree lhs, Degree rhs) { return lhs.value != rhs.value; }
    
    /// <summary>Returns whether a float value and a Degree are not bitwise equivalent.</summary>
    /// <param name="lhs">Left hand side float value to use in comparison.</param>
    /// <param name="rhs">Right hand side Degree to use in comparison.</param>
    /// <returns>True if the float value and the Degree are not bitwise equivalent, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(float lhs, Degree rhs) { return lhs != rhs.value; }
    
    /// <summary>Returns whether a Degree and a float value are not bitwise equivalent.</summary>
    /// <param name="lhs">Left hand side Degree to use in comparison.</param>
    /// <param name="rhs">Right hand side float value to use in comparison.</param>
    /// <returns>True if the Degree and the float value are not bitwise equivalent, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Degree lhs, float rhs) { return lhs.value != rhs; }
    
    /// <summary>Returns the result of a less than operation on two Degrees.</summary>
    /// <param name="lhs">Left hand side Degree to use to compute less than.</param>
    /// <param name="rhs">Right hand side Degree to use to compute less than.</param>
    /// <returns>bool result of the less than.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator < (Degree lhs, Degree rhs) { return lhs.value < rhs.value; }
    
    /// <summary>Returns the result of a less than operation on a Degree and a float value.</summary>
    /// <param name="lhs">Left hand side Degree to use to compute less than.</param>
    /// <param name="rhs">Right hand side float to use to compute less than.</param>
    /// <returns>bool result of the less than.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator < (float lhs, Degree rhs) { return lhs < rhs.value; }
    
    /// <summary>Returns the result of a less than operation on a float value and a Degree.</summary>
    /// <param name="lhs">Left hand side float to use to compute less than.</param>
    /// <param name="rhs">Right hand side Degree to use to compute less than.</param>
    /// <returns>bool result of the less than.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator < (Degree lhs, float rhs) { return lhs.value < rhs; }
    
    /// <summary>Returns the result of a less or equal operation on two Degrees.</summary>
    /// <param name="lhs">Left hand side Degree to use to compute less or equal.</param>
    /// <param name="rhs">Right hand side Degree to use to compute less or equal.</param>
    /// <returns>bool result of the less or equal.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <= (Degree lhs, Degree rhs) { return lhs.value <= rhs.value; }
    
    /// <summary>Returns the result of a less or equal operation on a Degree and a float value.</summary>
    /// <param name="lhs">Left hand side Degree to use to compute less or equal.</param>
    /// <param name="rhs">Right hand side float to use to compute less or equal.</param>
    /// <returns>bool result of the less or equal.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <= (float lhs, Degree rhs) { return lhs <= rhs.value; }
    
    /// <summary>Returns the result of a less or equal operation on a float value and a Degree.</summary>
    /// <param name="lhs">Left hand side float to use to compute less or equal.</param>
    /// <param name="rhs">Right hand side Degree to use to compute less or equal.</param>
    /// <returns>bool result of the less or equal.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <= (Degree lhs, float rhs) { return lhs.value <= rhs; }
    
    /// <summary>Returns the result of a greater than operation on two Degrees.</summary>
    /// <param name="lhs">Left hand side Degree to use to compute greater than.</param>
    /// <param name="rhs">Right hand side Degree to use to compute greater than.</param>
    /// <returns>bool result of the greater than.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator > (Degree lhs, Degree rhs) { return lhs.value > rhs.value; }
    
    /// <summary>Returns the result of a greater than operation on a Degree and a float value.</summary>
    /// <param name="lhs">Left hand side Degree to use to compute greater than.</param>
    /// <param name="rhs">Right hand side float to use to compute greater than.</param>
    /// <returns>bool result of the greater than.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator > (float lhs, Degree rhs) { return lhs > rhs.value; }
    
    /// <summary>Returns the result of a greater than operation on a float value and a Degree.</summary>
    /// <param name="lhs">Left hand side float to use to compute greater than.</param>
    /// <param name="rhs">Right hand side Degree to use to compute greater than.</param>
    /// <returns>bool result of the greater than.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator > (Degree lhs, float rhs) { return lhs.value > rhs; }
    
    /// <summary>Returns the result of a greater or equal operation on two Degrees.</summary>
    /// <param name="lhs">Left hand side Degree to use to compute greater or equal.</param>
    /// <param name="rhs">Right hand side Degree to use to compute greater or equal.</param>
    /// <returns>bool result of the greater or equal.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >= (Degree lhs, Degree rhs) { return lhs.value >= rhs.value; }
    
    /// <summary>Returns the result of a greater or equal operation on a Degree and a float value.</summary>
    /// <param name="lhs">Left hand side Degree to use to compute greater or equal.</param>
    /// <param name="rhs">Right hand side float to use to compute greater or equal.</param>
    /// <returns>bool result of the greater or equal.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >= (float lhs, Degree rhs) { return lhs >= rhs.value; }
    
    /// <summary>Returns the result of a greater or equal operation on a float value and a Degree.</summary>
    /// <param name="lhs">Left hand side float to use to compute greater or equal.</param>
    /// <param name="rhs">Right hand side Degree to use to compute greater or equal.</param>
    /// <returns>bool result of the greater or equal.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >= (Degree lhs, float rhs) { return lhs.value >= rhs; }
    
    /// <summary>Returns the result of a unary minus operation on a Degree.</summary>
    /// <param name="val">Value to use when computing the unary minus.</param>
    /// <returns>Degree result of the unary minus.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Degree operator - (Degree val) { return new Degree (-val.value); }


    /// <summary>Returns the result of a unary plus operation on a Degree.</summary>
    /// <param name="val">Value to use when computing the unary plus.</param>
    /// <returns>Degree result of the unary plus.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Degree operator + (Degree val) { return new Degree (+val.value); }
    
    /// <summary>Returns true if the Degree is bitwise equivalent to a given Degree, false otherwise.</summary>
    /// <param name="rhs">Right hand side Degree value to use in comparison.</param>
    /// <returns>True if the Degree value is bitwise equivalent to the input, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Degree rhs) { return value == rhs.value; }

    /// <summary>Returns true if the Degree is equal to a given Degree, false otherwise.</summary>
    /// <param name="o">Right hand side object to use in comparison.</param>
    /// <returns>True if the object is of type Degree and is bitwise equivalent, false otherwise.</returns>
    public override bool Equals(object o) { return o is Degree converted && Equals(converted); }
    
    /// <summary>Returns a hash code for the Degree.</summary>
    /// <returns>The computed hash code of the Degree.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode() { return (int)(value*1000); }
    
    /// <summary>Returns a string representation of the Degree.</summary>
    /// <returns>The string representation of the Degree.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString(){
        return value+"°";
    }
    
    /// <summary>Returns a string representation of the Degree using a specified format and culture-specific format information.</summary>
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
        return a - 360f * Mathf.Floor((a + 180f) / 360f);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static float Mod360Signed(double a) {
        return (float)(a - 360d * System.Math.Floor((a + 180d) / 360d));
    }
}
}