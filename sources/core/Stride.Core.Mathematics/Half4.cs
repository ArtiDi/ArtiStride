// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org/ & https://stride3d.net) and Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.
//
// Copyright (c) 2010-2011 SharpDX - Alexandre Mutel
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Stride.Core.Mathematics;

/// <summary>
/// Represents a four dimensional mathematical vector with half-precision floats.
/// </summary>
[DataContract]
[StructLayout(LayoutKind.Sequential, Pack = 2)]
public struct Half4 : IEquatable<Half4>, ISpanFormattable
{
    /// <summary>
    /// The size of the <see cref="Half4"/> type, in bytes.
    /// </summary>
    public static readonly int SizeInBytes = Unsafe.SizeOf<Half4>();

    /// <summary>
    /// A <see cref="Half4"/> with all of its components set to zero.
    /// </summary>
    public static readonly Half4 Zero = new();

    /// <summary>
    /// The X unit <see cref="Half4"/> (1, 0, 0, 0).
    /// </summary>
    public static readonly Half4 UnitX = new(1.0f, 0.0f, 0.0f, 0.0f);

    /// <summary>
    /// The Y unit <see cref="Half4"/> (0, 1, 0, 0).
    /// </summary>
    public static readonly Half4 UnitY = new(0.0f, 1.0f, 0.0f, 0.0f);

    /// <summary>
    /// The Z unit <see cref="Half4"/> (0, 0, 1, 0).
    /// </summary>
    public static readonly Half4 UnitZ = new(0.0f, 0.0f, 1.0f, 0.0f);

    /// <summary>
    /// The W unit <see cref="Half4"/> (0, 0, 0, 1).
    /// </summary>
    public static readonly Half4 UnitW = new(0.0f, 0.0f, 0.0f, 1.0f);

    /// <summary>
    /// A <see cref="Half4"/> with all of its components set to one.
    /// </summary>
    public static readonly Half4 One = new(1.0f, 1.0f, 1.0f, 1.0f);

    /// <summary>
    /// Gets or sets the X component of the vector.
    /// </summary>
    /// <value>The X component of the vector.</value>
    public Half X;

    /// <summary>
    /// Gets or sets the Y component of the vector.
    /// </summary>
    /// <value>The Y component of the vector.</value>
    public Half Y;

    /// <summary>
    /// Gets or sets the Z component of the vector.
    /// </summary>
    /// <value>The Z component of the vector.</value>
    public Half Z;

    /// <summary>
    /// Gets or sets the W component of the vector.
    /// </summary>
    /// <value>The W component of the vector.</value>
    public Half W;

    /// <summary>
    /// Initializes a new instance of the <see cref="Half4"/> structure.
    /// </summary>
    /// <param name="x">The X component.</param>
    /// <param name="y">The Y component.</param>
    /// <param name="z">The Z component.</param>
    /// <param name="w">The W component.</param>
    public Half4(Half x, Half y, Half z, Half w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Half4"/> structure.
    /// </summary>
    /// <param name="value">The value to set for the X, Y, Z, and W components.</param>
    public Half4(Half value)
    {
        X = value;
        Y = value;
        Z = value;
        W = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Half4"/> struct.
    /// </summary>
    /// <param name="values">The values to assign to the X, Y, Z, and W components of the vector. This must be an array with four elements.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="values"/> is <c>null</c>.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="values"/> contains more or less than four elements.</exception>
    public Half4(Half[] values)
    {
        ArgumentNullException.ThrowIfNull(values);
        if (values.Length != 4)
            throw new ArgumentOutOfRangeException(nameof(values), "There must be four and only four input values for Half4.");

        X = values[0];
        Y = values[1];
        Z = values[2];
        W = values[3];
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Half4"/> structure.
    /// </summary>
    /// <param name="x">The X component.</param>
    /// <param name="y">The Y component.</param>
    /// <param name="z">The Z component.</param>
    /// <param name="w">The W component.</param>
    public Half4(float x, float y, float z, float w)
    {
        X = (Half)x;
        Y = (Half)y;
        Z = (Half)z;
        W = (Half)w;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Half4"/> structure.
    /// </summary>
    /// <param name="value">The value to set for the X, Y, Z, and W components.</param>
    public Half4(float value)
    {
        X = (Half)value;
        Y = (Half)value;
        Z = (Half)value;
        W = (Half)value;
    }

    /// <summary>
    /// Tests for equality between two objects.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <c>true</c> if <paramref name="left" /> has the same value as <paramref name="right" />; otherwise, <c>false</c>.</returns>
    public static bool operator ==(Half4 left, Half4 right)
    {
        return Equals(ref left, ref right);
    }

    /// <summary>
    /// Tests for inequality between two objects.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <c>true</c> if <paramref name="left" /> has a different value than <paramref name="right" />; otherwise, <c>false</c>.</returns>
    public static bool operator !=(Half4 left, Half4 right)
    {
        return !Equals(ref left, ref right);
    }

    /// <summary>
    /// Returns a <see cref="string"/> that represents this instance.
    /// </summary>
    /// <returns>
    /// A <see cref="string"/> that represents this instance.
    /// </returns>
    public override readonly string ToString() => $"{this}";

    /// <summary>
    /// Returns a <see cref="string"/> that represents this instance.
    /// </summary>
    /// <param name="format">The format.</param>
    /// <param name="formatProvider">The format provider.</param>
    /// <returns>
    /// A <see cref="string"/> that represents this instance.
    /// </returns>
    public readonly string ToString([StringSyntax(StringSyntaxAttribute.NumericFormat)] string? format, IFormatProvider? formatProvider)
    {
        var separator = $"{((formatProvider as CultureInfo) ?? CultureInfo.CurrentCulture).TextInfo.ListSeparator} ";
        var handler = new DefaultInterpolatedStringHandler(separator.Length * 3, 4, formatProvider);
        handler.AppendFormatted(X, format);
        handler.AppendLiteral(separator);
        handler.AppendFormatted(Y, format);
        handler.AppendLiteral(separator);
        handler.AppendFormatted(Z, format);
        handler.AppendLiteral(separator);
        handler.AppendFormatted(W, format);
        return handler.ToStringAndClear();
    }

    bool ISpanFormattable.TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
    {
        var separator = $"{((provider as CultureInfo) ?? CultureInfo.CurrentCulture).TextInfo.ListSeparator} ";
        var format1 = format.Length > 0 ? format.ToString() : null;
        var handler = new MemoryExtensions.TryWriteInterpolatedStringHandler(separator.Length * 3, 4, destination, provider, out _);
        handler.AppendFormatted(X, format1);
        handler.AppendLiteral(separator);
        handler.AppendFormatted(Y, format1);
        handler.AppendLiteral(separator);
        handler.AppendFormatted(Z, format1);
        handler.AppendLiteral(separator);
        handler.AppendFormatted(W, format1);
        return destination.TryWrite(ref handler, out charsWritten);
    }

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override readonly int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z, W);
    }

    /// <summary>
    /// Determines whether the specified object instances are considered equal.
    /// </summary>
    /// <param name="value1">The first value.</param>
    /// <param name="value2">The second value.</param>
    /// <returns>
    /// <c>true</c> if <paramref name="value1" /> is the same instance as <paramref name="value2" /> or
    /// if both are <c>null</c> references or if <c>value1.Equals(value2)</c> returns <c>true</c>; otherwise, <c>false</c>.</returns>
    public static bool Equals(ref readonly Half4 value1, ref readonly Half4 value2)
    {
        return (value1.X == value2.X) && (value1.Y == value2.Y) && (value1.Z == value2.Z) && (value1.W == value2.W);
    }

    /// <summary>
    /// Returns a value that indicates whether the current instance is equal to the specified object.
    /// </summary>
    /// <param name="other">Object to make the comparison with.</param>
    /// <returns>
    /// <c>true</c> if the current instance is equal to the specified object; <c>false</c> otherwise.</returns>
    public readonly bool Equals(Half4 other)
    {
        return (X == other.X) && (Y == other.Y) && (Z == other.Z) && (W == other.W);
    }

    /// <summary>
    /// Performs an explicit conversion from <see cref="Vector4"/> to <see cref="Half4"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The result of the conversion.</returns>
    public static explicit operator Half4(Vector4 value)
    {
        return new Half4((Half)value.X, (Half)value.Y, (Half)value.Z, (Half)value.W);
    }

    /// <summary>
    /// Performs an explicit conversion from <see cref="Half4"/> to <see cref="Vector4"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The result of the conversion.</returns>
    public static explicit operator Vector4(Half4 value)
    {
        return new Vector4(value.X, value.Y, value.Z, value.W);
    }

    /// <summary>
    /// Returns a value that indicates whether the current instance is equal to a specified object.
    /// </summary>
    /// <param name="obj">Object to make the comparison with.</param>
    /// <returns>
    /// <c>true</c> if the current instance is equal to the specified object; <c>false</c> otherwise.</returns>
    public override readonly bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is Half4 half && Equals(half);
    }

    /// <summary>
    /// Deconstructs the vector's components into named variables.
    /// </summary>
    /// <param name="x">The X component</param>
    /// <param name="y">The Y component</param>
    /// <param name="z">The Z component</param>
    /// <param name="w">The W component</param>
    public readonly void Deconstruct(out Half x, out Half y, out Half z, out Half w)
    {
        x = X;
        y = Y;
        z = Z;
        w = W;
    }
}
