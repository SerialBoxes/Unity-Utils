using System;
using System.Runtime.CompilerServices;
using UnityEngine;


namespace NoodleKit.Math {

/// <summary>
/// A float that can only have a value between -π and π
/// </summary>
[Serializable]
public struct Radian : IEquatable<Radian>, IFormattable {

    /// <summary>
    /// The raw float value of the Radian.
    /// </summary>
    public float value;

    /// <summary>Radian zero value.</summary>
    public static readonly Radian zero = new Radian();

    /// <summary>
    /// The maximum finite Radian value as a single precision float.
    /// </summary>
    public static float MaxValue {
        get { return Mathf.PI - Mathf.Epsilon; }
    }

    /// <summary>
    /// The minimum finite Radian value as a single precision float.
    /// </summary>
    public static float MinValue {
        get { return -Mathf.PI; }
    }

    /// <summary>
    /// The maximum finite Radian value as a Radian.
    /// </summary>
    public static Radian MaxValueAsRadian => new Radian(MaxValue);

    /// <summary>
    /// The minimum finite Radian value as a Radian.
    /// </summary>
    public static Radian MinValueAsRadian => new Radian(MinValue);

    /// <summary>Constructs a Radian value from a Radian value.</summary>
    /// <param name="x">The input Radian value to copy.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Radian(Radian x) {
        value = x.value;
    }

    /// <summary>Constructs a Radian value from a float value.</summary>
    /// <param name="v">The single precision float value to convert to Radian.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Radian(float v) {
        value = Mod2PISigned(v);
    }

    /// <summary>Constructs a half value from a double value.</summary>
    /// <param name="v">The double precision float value to convert to half.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Radian(double v) {
        value = Mod2PISigned(v);
    }
    
    /// <summary>Explicitly converts a float value to a Radian value.</summary>
    /// <param name="v">The single precision float value to convert to Radian.</param>
    /// <returns>The converted Radian value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator Radian(float v) { return new Radian(v); }

    /// <summary>Explicitly converts a double value to a Radian value.</summary>
    /// <param name="v">The double precision float value to convert to Radian.</param>
    /// <returns>The converted Radian value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator Radian(double v) { return new Radian(v); }
    
    /// <summary>Explicitly converts a Degree value to a Radian value.</summary>
    /// <param name="v">The Degree value to convert to Radian.</param>
    /// <returns>The converted Radian value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator Radian(Degree v) { return new Radian(v.value * Mathf.Deg2Rad); }

    /// <summary>Implicitly converts a Radian value to a float value.</summary>
    /// <param name="d">The Radian value to convert to a single precision float.</param>
    /// <returns>The converted single precision float value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator float(Radian d) { return d.value; }

    /// <summary>Implicitly converts a Radian value to a double value.</summary>
    /// <param name="d">The Radian value to convert to double precision float.</param>
    /// <returns>The converted double precision float value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator double(Radian d) { return d.value; }
    
    /// <summary>Returns the result of a multiplication operation on two Radians.</summary>
    /// <param name="lhs">Left hand side Radian to use to compute multiplication.</param>
    /// <param name="rhs">Right hand side Radian to use to compute multiplication.</param>
    /// <returns>Radian result of the multiplication.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Radian operator * (Radian lhs, Radian rhs) { return new Radian (lhs.value * rhs.value); }
    
    /// <summary>Returns the result of a multiplication operation on a Radian and a float value.</summary>
    /// <param name="lhs">Left hand side Radian to use to compute multiplication.</param>
    /// <param name="rhs">Right hand side float to use to compute multiplication.</param>
    /// <returns>Radian result of the multiplication.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Radian operator * (float lhs, Radian rhs) { return new Radian (lhs * rhs.value); }
    
    /// <summary>Returns the result of a multiplication operation on a float value and a Radian.</summary>
    /// <param name="lhs">Left hand side float to use to compute multiplication.</param>
    /// <param name="rhs">Right hand side Radian to use to compute multiplication.</param>
    /// <returns>Radian result of the multiplication.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Radian operator * (Radian lhs, float rhs) { return new Radian (lhs.value * rhs); }
    
    /// <summary>Returns the result of an addition operation on two Radians.</summary>
    /// <param name="lhs">Left hand side Radian to use to compute addition.</param>
    /// <param name="rhs">Right hand side Radian to use to compute addition.</param>
    /// <returns>Radian result of the addition.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Radian operator + (Radian lhs, Radian rhs) { return new Radian (lhs.value + rhs.value); }
    
    /// <summary>Returns the result of an addition operation on a Radian and a float value.</summary>
    /// <param name="lhs">Left hand side Radian to use to compute addition.</param>
    /// <param name="rhs">Right hand side float to use to compute addition.</param>
    /// <returns>Radian result of the addition.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Radian operator + (float lhs, Radian rhs) { return new Radian (lhs + rhs.value); }
    
    /// <summary>Returns the result of an addition operation on a float value and a Radian.</summary>
    /// <param name="lhs">Left hand side float to use to compute addition.</param>
    /// <param name="rhs">Right hand side Radian to use to compute addition.</param>
    /// <returns>Radian result of the addition.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Radian operator + (Radian lhs, float rhs) { return new Radian (lhs.value + rhs); }
    
    /// <summary>Returns the result of a division operation on two Radians.</summary>
    /// <param name="lhs">Left hand side Radian to use to compute division.</param>
    /// <param name="rhs">Right hand side Radian to use to compute division.</param>
    /// <returns>Radian result of the division.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Radian operator / (Radian lhs, Radian rhs) { return new Radian (lhs.value / rhs.value); }
    
    /// <summary>Returns the result of a division operation on a Radian and a float value.</summary>
    /// <param name="lhs">Left hand side Radian to use to compute division.</param>
    /// <param name="rhs">Right hand side float to use to compute division.</param>
    /// <returns>Radian result of the division.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Radian operator / (float lhs, Radian rhs) { return new Radian (lhs / rhs.value); }
    
    /// <summary>Returns the result of a division operation on a float value and a Radian.</summary>
    /// <param name="lhs">Left hand side float to use to compute division.</param>
    /// <param name="rhs">Right hand side Radian to use to compute division.</param>
    /// <returns>Radian result of the division.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Radian operator / (Radian lhs, float rhs) { return new Radian (lhs.value / rhs); }
    
    /// <summary>Returns the result of a subtraction operation on two Radians.</summary>
    /// <param name="lhs">Left hand side Radian to use to compute subtraction.</param>
    /// <param name="rhs">Right hand side Radian to use to compute subtraction.</param>
    /// <returns>Radian result of the subtraction.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Radian operator - (Radian lhs, Radian rhs) { return new Radian (lhs.value - rhs.value); }
    
    /// <summary>Returns the result of a subtraction operation on a Radian and a float value.</summary>
    /// <param name="lhs">Left hand side Radian to use to compute subtraction.</param>
    /// <param name="rhs">Right hand side float to use to compute subtraction.</param>
    /// <returns>Radian result of the subtraction.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Radian operator - (float lhs, Radian rhs) { return new Radian (lhs - rhs.value); }
    
    /// <summary>Returns the result of a subtraction operation on a float value and a Radian.</summary>
    /// <param name="lhs">Left hand side float to use to compute subtraction.</param>
    /// <param name="rhs">Right hand side Radian to use to compute subtraction.</param>
    /// <returns>Radian result of the subtraction.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Radian operator - (Radian lhs, float rhs) { return new Radian (lhs.value - rhs); }
    
    /// <summary>Returns the result of a modulus operation on two Radians.</summary>
    /// <param name="lhs">Left hand side Radian to use to compute modulus.</param>
    /// <param name="rhs">Right hand side Radian to use to compute modulus.</param>
    /// <returns>Radian result of the modulus.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Radian operator % (Radian lhs, Radian rhs) { return new Radian (lhs.value % rhs.value); }
    
    /// <summary>Returns the result of a modulus operation on a Radian and a float value.</summary>
    /// <param name="lhs">Left hand side Radian to use to compute modulus.</param>
    /// <param name="rhs">Right hand side float to use to compute modulus.</param>
    /// <returns>Radian result of the modulus.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Radian operator % (float lhs, Radian rhs) { return new Radian (lhs % rhs.value); }
    
    /// <summary>Returns the result of a modulus operation on a float value and a Radian.</summary>
    /// <param name="lhs">Left hand side float to use to compute modulus.</param>
    /// <param name="rhs">Right hand side Radian to use to compute modulus.</param>
    /// <returns>Radian result of the modulus.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Radian operator % (Radian lhs, float rhs) { return new Radian (lhs.value % rhs); }
    
    /// <summary>Returns the result of an increment operation on a Radian. Radians are incremented by π.</summary>
    /// <param name="val">Value to use when computing the increment.</param>
    /// <returns>Radian result of the increment.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Radian operator ++ (Radian val) { return new Radian (val.value + Mathf.PI); }


    /// <summary>Returns the result of a decrement operation on a Radian. Radians are decremented by π.</summary>
    /// <param name="val">Value to use when computing the decrement.</param>
    /// <returns>Radian result of the decrement.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Radian operator -- (Radian val) { return new Radian (val.value - Mathf.PI); }
    
    /// <summary>Returns whether two Radian values are bitwise equivalent.</summary>
    /// <param name="lhs">Left hand side Radian value to use in comparison.</param>
    /// <param name="rhs">Right hand side Radian value to use in comparison.</param>
    /// <returns>True if the two Radian values are bitwise equivalent, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Radian lhs, Radian rhs) { return lhs.value == rhs.value; }
    
    /// <summary>Returns whether a float value and a Radian are bitwise equivalent.</summary>
    /// <param name="lhs">Left hand side float value to use in comparison.</param>
    /// <param name="rhs">Right hand side Radian to use in comparison.</param>
    /// <returns>True if the float value and the Radian are bitwise equivalent, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(float lhs, Radian rhs) { return lhs == rhs.value; }
    
    /// <summary>Returns whether a Radian and a float value are bitwise equivalent.</summary>
    /// <param name="lhs">Left hand side Radian to use in comparison.</param>
    /// <param name="rhs">Right hand side float value to use in comparison.</param>
    /// <returns>True if the Radian and the float value are bitwise equivalent, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Radian lhs, float rhs) { return lhs.value == rhs; }

    /// <summary>Returns whether two Radian values are not bitwise equivalent.</summary>
    /// <param name="lhs">Left hand side Radian value to use in comparison.</param>
    /// <param name="rhs">Right hand side Radian value to use in comparison.</param>
    /// <returns>True if the two Radian values are not bitwise equivalent, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Radian lhs, Radian rhs) { return lhs.value != rhs.value; }
    
    /// <summary>Returns whether a float value and a Radian are not bitwise equivalent.</summary>
    /// <param name="lhs">Left hand side float value to use in comparison.</param>
    /// <param name="rhs">Right hand side Radian to use in comparison.</param>
    /// <returns>True if the float value and the Radian are not bitwise equivalent, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(float lhs, Radian rhs) { return lhs != rhs.value; }
    
    /// <summary>Returns whether a Radian and a float value are not bitwise equivalent.</summary>
    /// <param name="lhs">Left hand side Radian to use in comparison.</param>
    /// <param name="rhs">Right hand side float value to use in comparison.</param>
    /// <returns>True if the Radian and the float value are not bitwise equivalent, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Radian lhs, float rhs) { return lhs.value != rhs; }
    
    /// <summary>Returns the result of a less than operation on two Radians.</summary>
    /// <param name="lhs">Left hand side Radian to use to compute less than.</param>
    /// <param name="rhs">Right hand side Radian to use to compute less than.</param>
    /// <returns>bool result of the less than.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator < (Radian lhs, Radian rhs) { return lhs.value < rhs.value; }
    
    /// <summary>Returns the result of a less than operation on a Radian and a float value.</summary>
    /// <param name="lhs">Left hand side Radian to use to compute less than.</param>
    /// <param name="rhs">Right hand side float to use to compute less than.</param>
    /// <returns>bool result of the less than.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator < (float lhs, Radian rhs) { return lhs < rhs.value; }
    
    /// <summary>Returns the result of a less than operation on a float value and a Radian.</summary>
    /// <param name="lhs">Left hand side float to use to compute less than.</param>
    /// <param name="rhs">Right hand side Radian to use to compute less than.</param>
    /// <returns>bool result of the less than.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator < (Radian lhs, float rhs) { return lhs.value < rhs; }
    
    /// <summary>Returns the result of a less or equal operation on two Radians.</summary>
    /// <param name="lhs">Left hand side Radian to use to compute less or equal.</param>
    /// <param name="rhs">Right hand side Radian to use to compute less or equal.</param>
    /// <returns>bool result of the less or equal.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <= (Radian lhs, Radian rhs) { return lhs.value <= rhs.value; }
    
    /// <summary>Returns the result of a less or equal operation on a Radian and a float value.</summary>
    /// <param name="lhs">Left hand side Radian to use to compute less or equal.</param>
    /// <param name="rhs">Right hand side float to use to compute less or equal.</param>
    /// <returns>bool result of the less or equal.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <= (float lhs, Radian rhs) { return lhs <= rhs.value; }
    
    /// <summary>Returns the result of a less or equal operation on a float value and a Radian.</summary>
    /// <param name="lhs">Left hand side float to use to compute less or equal.</param>
    /// <param name="rhs">Right hand side Radian to use to compute less or equal.</param>
    /// <returns>bool result of the less or equal.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <= (Radian lhs, float rhs) { return lhs.value <= rhs; }
    
    /// <summary>Returns the result of a greater than operation on two Radians.</summary>
    /// <param name="lhs">Left hand side Radian to use to compute greater than.</param>
    /// <param name="rhs">Right hand side Radian to use to compute greater than.</param>
    /// <returns>bool result of the greater than.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator > (Radian lhs, Radian rhs) { return lhs.value > rhs.value; }
    
    /// <summary>Returns the result of a greater than operation on a Radian and a float value.</summary>
    /// <param name="lhs">Left hand side Radian to use to compute greater than.</param>
    /// <param name="rhs">Right hand side float to use to compute greater than.</param>
    /// <returns>bool result of the greater than.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator > (float lhs, Radian rhs) { return lhs > rhs.value; }
    
    /// <summary>Returns the result of a greater than operation on a float value and a Radian.</summary>
    /// <param name="lhs">Left hand side float to use to compute greater than.</param>
    /// <param name="rhs">Right hand side Radian to use to compute greater than.</param>
    /// <returns>bool result of the greater than.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator > (Radian lhs, float rhs) { return lhs.value > rhs; }
    
    /// <summary>Returns the result of a greater or equal operation on two Radians.</summary>
    /// <param name="lhs">Left hand side Radian to use to compute greater or equal.</param>
    /// <param name="rhs">Right hand side Radian to use to compute greater or equal.</param>
    /// <returns>bool result of the greater or equal.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >= (Radian lhs, Radian rhs) { return lhs.value >= rhs.value; }
    
    /// <summary>Returns the result of a greater or equal operation on a Radian and a float value.</summary>
    /// <param name="lhs">Left hand side Radian to use to compute greater or equal.</param>
    /// <param name="rhs">Right hand side float to use to compute greater or equal.</param>
    /// <returns>bool result of the greater or equal.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >= (float lhs, Radian rhs) { return lhs >= rhs.value; }
    
    /// <summary>Returns the result of a greater or equal operation on a float value and a Radian.</summary>
    /// <param name="lhs">Left hand side float to use to compute greater or equal.</param>
    /// <param name="rhs">Right hand side Radian to use to compute greater or equal.</param>
    /// <returns>bool result of the greater or equal.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >= (Radian lhs, float rhs) { return lhs.value >= rhs; }
    
    /// <summary>Returns the result of a unary minus operation on a Radian.</summary>
    /// <param name="val">Value to use when computing the unary minus.</param>
    /// <returns>Radian result of the unary minus.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Radian operator - (Radian val) { return new Radian (-val.value); }


    /// <summary>Returns the result of a unary plus operation on a Radian.</summary>
    /// <param name="val">Value to use when computing the unary plus.</param>
    /// <returns>Radian result of the unary plus.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Radian operator + (Radian val) { return new Radian (+val.value); }
    
    /// <summary>Returns true if the Radian is bitwise equivalent to a given Radian, false otherwise.</summary>
    /// <param name="rhs">Right hand side Radian value to use in comparison.</param>
    /// <returns>True if the Radian value is bitwise equivalent to the input, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Radian rhs) { return value == rhs.value; }

    /// <summary>Returns true if the Radian is equal to a given Radian, false otherwise.</summary>
    /// <param name="o">Right hand side object to use in comparison.</param>
    /// <returns>True if the object is of type Radian and is bitwise equivalent, false otherwise.</returns>
    public override bool Equals(object o) { return o is Radian converted && Equals(converted); }
    
    /// <summary>Returns a hash code for the Radian.</summary>
    /// <returns>The computed hash code of the Radian.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode() { return (int)(value*1000); }
    
    /// <summary>Returns a string representation of the Radian.</summary>
    /// <returns>The string representation of the Radian.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString(){
        return (value/Mathf.PI)+"π";
    }
    
    /// <summary>Returns a string representation of the Radian using a specified format and culture-specific format information.</summary>
    /// <param name="format">The format string to use during string formatting.</param>
    /// <param name="formatProvider">The format provider to use during string formatting.</param>
    /// <returns>The string representation of the half.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToString(string format, IFormatProvider formatProvider)
    {
        return (value/Mathf.PI).ToString(format, formatProvider)+"π";
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static float Mod2PISigned(float a) {
        return a - (2*Mathf.PI) * Mathf.Floor((a + Mathf.PI) / (2*Mathf.PI));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static float Mod2PISigned(double a) {
        return (float)(a - (2*System.Math.PI) * System.Math.Floor((a + System.Math.PI) / (2*System.Math.PI)));
    }
}
}