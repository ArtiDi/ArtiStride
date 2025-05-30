// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org/ & https://stride3d.net) and Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.
//
// -----------------------------------------------------------------------------
// Original code from SlimMath project. http://code.google.com/p/slimmath/
// Greetings to SlimDX Group. Original code published with the following license:
// -----------------------------------------------------------------------------
/*
* Copyright (c) 2007-2011 SlimDX Group
* 
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
* 
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
* THE SOFTWARE.
*/

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Stride.Core.Mathematics;

/// <summary>
/// Represents an axis-aligned bounding box in three dimensional space.
/// </summary>
[DataContract]
[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct BoundingBox : IEquatable<BoundingBox>, ISpanFormattable, IIntersectableWithRay, IIntersectableWithPlane
{
    /// <summary>
    /// A <see cref="BoundingBox"/> which represents an empty space.
    /// </summary>
    public static readonly BoundingBox Empty = new(new Vector3(float.MaxValue), new Vector3(float.MinValue));

    /// <summary>
    /// The minimum point of the box.
    /// </summary>
    public Vector3 Minimum;

    /// <summary>
    /// The maximum point of the box.
    /// </summary>
    public Vector3 Maximum;

    /// <summary>
    /// Initializes a new instance of the <see cref="BoundingBox"/> struct.
    /// </summary>
    /// <param name="minimum">The minimum vertex of the bounding box.</param>
    /// <param name="maximum">The maximum vertex of the bounding box.</param>
    public BoundingBox(Vector3 minimum, Vector3 maximum)
    {
        Minimum = minimum;
        Maximum = maximum;
    }

    /// <summary>
    /// Gets the center of this bouding box.
    /// </summary>
    public readonly Vector3 Center
    {
        get { return (Minimum + Maximum) / 2; }
    }

    /// <summary>
    /// Gets the extent of this bouding box.
    /// </summary>
    public readonly Vector3 Extent
    {
        get { return (Maximum - Minimum) / 2; }
    }

    /// <summary>
    /// Retrieves the eight corners of the bounding box.
    /// </summary>
    /// <returns>An array of points representing the eight corners of the bounding box.</returns>
    public readonly Vector3[] GetCorners()
    {
        return
        [
            new Vector3(Minimum.X, Maximum.Y, Maximum.Z),
            new Vector3(Maximum.X, Maximum.Y, Maximum.Z),
            new Vector3(Maximum.X, Minimum.Y, Maximum.Z),
            new Vector3(Minimum.X, Minimum.Y, Maximum.Z),
            new Vector3(Minimum.X, Maximum.Y, Minimum.Z),
            new Vector3(Maximum.X, Maximum.Y, Minimum.Z),
            new Vector3(Maximum.X, Minimum.Y, Minimum.Z),
            new Vector3(Minimum.X, Minimum.Y, Minimum.Z),
        ];
    }

    /// <summary>
    /// Determines if there is an intersection between the current object and a <see cref="Ray"/>.
    /// </summary>
    /// <param name="ray">The ray to test.</param>
    /// <returns>Whether the two objects intersected.</returns>
    public bool Intersects(ref readonly Ray ray)
    {
        return CollisionHelper.RayIntersectsBox(in ray, ref this, out float _);
    }

    /// <summary>
    /// Determines if there is an intersection between the current object and a <see cref="Ray"/>.
    /// </summary>
    /// <param name="ray">The ray to test.</param>
    /// <param name="distance">When the method completes, contains the distance of the intersection,
    /// or 0 if there was no intersection.</param>
    /// <returns>Whether the two objects intersected.</returns>
    public bool Intersects(ref readonly Ray ray, out float distance)
    {
        return CollisionHelper.RayIntersectsBox(in ray, ref this, out distance);
    }

    /// <summary>
    /// Determines if there is an intersection between the current object and a <see cref="Ray"/>.
    /// </summary>
    /// <param name="ray">The ray to test.</param>
    /// <param name="point">When the method completes, contains the point of intersection,
    /// or <see cref="Vector3.Zero"/> if there was no intersection.</param>
    /// <returns>Whether the two objects intersected.</returns>
    public bool Intersects(ref readonly Ray ray, out Vector3 point)
    {
        return CollisionHelper.RayIntersectsBox(in ray, ref this, out point);
    }

    /// <summary>
    /// Determines if there is an intersection between the current object and a <see cref="Plane"/>.
    /// </summary>
    /// <param name="plane">The plane to test.</param>
    /// <returns>Whether the two objects intersected.</returns>
    public PlaneIntersectionType Intersects(ref readonly Plane plane)
    {
        return CollisionHelper.PlaneIntersectsBox(in plane, ref this);
    }

    /* This implentation is wrong
    /// <summary>
    /// Determines if there is an intersection between the current object and a triangle.
    /// </summary>
    /// <param name="vertex1">The first vertex of the triangle to test.</param>
    /// <param name="vertex2">The second vertex of the triagnle to test.</param>
    /// <param name="vertex3">The third vertex of the triangle to test.</param>
    /// <returns>Whether the two objects intersected.</returns>
    public bool Intersects(ref Vector3 vertex1, ref Vector3 vertex2, ref Vector3 vertex3)
    {
        return Collision.BoxIntersectsTriangle(ref this, ref vertex1, ref vertex2, ref vertex3);
    }
    */

    /// <summary>
    /// Determines if there is an intersection between the current object and a <see cref="BoundingBox"/>.
    /// </summary>
    /// <param name="box">The box to test.</param>
    /// <returns>Whether the two objects intersected.</returns>
    public bool Intersects(ref readonly BoundingBox box)
    {
        return CollisionHelper.BoxIntersectsBox(ref this, in box);
    }

    /// <summary>
    /// Determines if there is an intersection between the current object and a <see cref="BoundingSphere"/>.
    /// </summary>
    /// <param name="sphere">The sphere to test.</param>
    /// <returns>Whether the two objects intersected.</returns>
    public bool Intersects(ref readonly BoundingSphere sphere)
    {
        return CollisionHelper.BoxIntersectsSphere(ref this, in sphere);
    }

    /// <summary>
    /// Determines whether the current objects contains a point.
    /// </summary>
    /// <param name="point">The point to test.</param>
    /// <returns>The type of containment the two objects have.</returns>
    public ContainmentType Contains(ref readonly Vector3 point)
    {
        return CollisionHelper.BoxContainsPoint(ref this, in point);
    }

    /* This implentation is wrong
    /// <summary>
    /// Determines whether the current objects contains a triangle.
    /// </summary>
    /// <param name="vertex1">The first vertex of the triangle to test.</param>
    /// <param name="vertex2">The second vertex of the triagnle to test.</param>
    /// <param name="vertex3">The third vertex of the triangle to test.</param>
    /// <returns>The type of containment the two objects have.</returns>
    public ContainmentType Contains(ref Vector3 vertex1, ref Vector3 vertex2, ref Vector3 vertex3)
    {
        return Collision.BoxContainsTriangle(ref this, ref vertex1, ref vertex2, ref vertex3);
    }
    */

    /// <summary>
    /// Determines whether the current objects contains a <see cref="BoundingBox"/>.
    /// </summary>
    /// <param name="box">The box to test.</param>
    /// <returns>The type of containment the two objects have.</returns>
    public ContainmentType Contains(ref readonly BoundingBox box)
    {
        return CollisionHelper.BoxContainsBox(ref this, in box);
    }

    /// <summary>
    /// Determines whether the current objects contains a <see cref="BoundingSphere"/>.
    /// </summary>
    /// <param name="sphere">The sphere to test.</param>
    /// <returns>The type of containment the two objects have.</returns>
    public ContainmentType Contains(ref readonly BoundingSphere sphere)
    {
        return CollisionHelper.BoxContainsSphere(ref this, in sphere);
    }

    /// <summary>
    /// Constructs a <see cref="BoundingBox"/> that fully contains the given points.
    /// </summary>
    /// <param name="points">The points that will be contained by the box.</param>
    /// <param name="result">When the method completes, contains the newly constructed bounding box.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="points"/> is <c>null</c>.</exception>
    public static void FromPoints(Vector3[] points, out BoundingBox result)
    {
        ArgumentNullException.ThrowIfNull(points);

        Vector3 min = new Vector3(float.MaxValue);
        Vector3 max = new Vector3(float.MinValue);

        for (int i = 0; i < points.Length; ++i)
        {
            Vector3.Min(ref min, ref points[i], out min);
            Vector3.Max(ref max, ref points[i], out max);
        }

        result = new BoundingBox(min, max);
    }

    /// <summary>
    /// Constructs a <see cref="Stride.Core.Mathematics.BoundingBox"/> that fully contains the given points.
    /// </summary>
    /// <param name="points">The points that will be contained by the box.</param>
    /// <returns>The newly constructed bounding box.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="points"/> is <c>null</c>.</exception>
    public static BoundingBox FromPoints(Vector3[] points)
    {
        ArgumentNullException.ThrowIfNull(points);

        Vector3 min = new Vector3(float.MaxValue);
        Vector3 max = new Vector3(float.MinValue);

        for (int i = 0; i < points.Length; ++i)
        {
            Vector3.Min(ref min, ref points[i], out min);
            Vector3.Max(ref max, ref points[i], out max);
        }

        return new BoundingBox(min, max);
    }

    /// <summary>
    /// Constructs a <see cref="Stride.Core.Mathematics.BoundingBox"/> from a given sphere.
    /// </summary>
    /// <param name="sphere">The sphere that will designate the extents of the box.</param>
    /// <param name="result">When the method completes, contains the newly constructed bounding box.</param>
    public static void FromSphere(ref readonly BoundingSphere sphere, out BoundingBox result)
    {
        result.Minimum = new Vector3(sphere.Center.X - sphere.Radius, sphere.Center.Y - sphere.Radius, sphere.Center.Z - sphere.Radius);
        result.Maximum = new Vector3(sphere.Center.X + sphere.Radius, sphere.Center.Y + sphere.Radius, sphere.Center.Z + sphere.Radius);
    }

    /// <summary>
    /// Constructs a <see cref="Stride.Core.Mathematics.BoundingBox"/> from a given sphere.
    /// </summary>
    /// <param name="sphere">The sphere that will designate the extents of the box.</param>
    /// <returns>The newly constructed bounding box.</returns>
    public static BoundingBox FromSphere(BoundingSphere sphere)
    {
        BoundingBox box;
        box.Minimum = new Vector3(sphere.Center.X - sphere.Radius, sphere.Center.Y - sphere.Radius, sphere.Center.Z - sphere.Radius);
        box.Maximum = new Vector3(sphere.Center.X + sphere.Radius, sphere.Center.Y + sphere.Radius, sphere.Center.Z + sphere.Radius);
        return box;
    }

    /// <summary>
    /// Transform a bounding box.
    /// </summary>
    /// <param name="value">The original bounding box.</param>
    /// <param name="transform">The transform to apply to the bounding box.</param>
    /// <param name="result">The transformed bounding box.</param>
    public static void Transform(ref readonly BoundingBox value, ref readonly Matrix transform, out BoundingBox result)
    {
        var boundingBox = new BoundingBoxExt(value);
        boundingBox.Transform(transform);
        result = (BoundingBox)boundingBox;
    }

    /// <summary>
    /// Constructs a <see cref="Stride.Core.Mathematics.BoundingBox"/> that is as large enough to contains the bounding box and the given point.
    /// </summary>
    /// <param name="value1">The box to merge.</param>
    /// <param name="value2">The point to merge.</param>
    /// <param name="result">When the method completes, contains the newly constructed bounding box.</param>
    public static void Merge(ref readonly BoundingBox value1, ref readonly Vector3 value2, out BoundingBox result)
    {
        Vector3.Min(in value1.Minimum, in value2, out result.Minimum);
        Vector3.Max(in value1.Maximum, in value2, out result.Maximum);
    }

    /// <summary>
    /// Constructs a <see cref="Stride.Core.Mathematics.BoundingBox"/> that is as large as the total combined area of the two specified boxes.
    /// </summary>
    /// <param name="value1">The first box to merge.</param>
    /// <param name="value2">The second box to merge.</param>
    /// <param name="result">When the method completes, contains the newly constructed bounding box.</param>
    public static void Merge(ref readonly BoundingBox value1, ref readonly BoundingBox value2, out BoundingBox result)
    {
        Vector3.Min(in value1.Minimum, in value2.Minimum, out result.Minimum);
        Vector3.Max(in value1.Maximum, in value2.Maximum, out result.Maximum);
    }

    /// <summary>
    /// Constructs a <see cref="Stride.Core.Mathematics.BoundingBox"/> that is as large as the total combined area of the two specified boxes.
    /// </summary>
    /// <param name="value1">The first box to merge.</param>
    /// <param name="value2">The second box to merge.</param>
    /// <returns>The newly constructed bounding box.</returns>
    public static BoundingBox Merge(BoundingBox value1, BoundingBox value2)
    {
        BoundingBox box;
        Vector3.Min(ref value1.Minimum, ref value2.Minimum, out box.Minimum);
        Vector3.Max(ref value1.Maximum, ref value2.Maximum, out box.Maximum);
        return box;
    }

    /// <summary>
    /// Tests for equality between two objects.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator ==(BoundingBox left, BoundingBox right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Tests for inequality between two objects.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator !=(BoundingBox left, BoundingBox right)
    {
        return !left.Equals(right);
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
    public readonly string ToString(string? format, IFormatProvider? formatProvider)
    {
        var handler = new DefaultInterpolatedStringHandler(17, 2, formatProvider);
        handler.AppendLiteral("Minimum:");
        handler.AppendFormatted(Minimum, format);
        handler.AppendLiteral(" Maximum:");
        handler.AppendFormatted(Maximum, format);
        return handler.ToStringAndClear();
    }

    bool ISpanFormattable.TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
    {
        var format1 = format.Length > 0 ? format.ToString() : null;
        var handler = new MemoryExtensions.TryWriteInterpolatedStringHandler(17, 2, destination, provider, out _);
        handler.AppendLiteral("Minimum:");
        handler.AppendFormatted(Minimum, format1);
        handler.AppendLiteral(" Maximum:");
        handler.AppendFormatted(Maximum, format1);
        return destination.TryWrite(ref handler, out charsWritten);
    }

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>
    /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
    /// </returns>
    public override readonly int GetHashCode()
    {
        return HashCode.Combine(Minimum, Maximum);
    }

    /// <summary>
    /// Determines whether the specified <see cref="Vector4"/> is equal to this instance.
    /// </summary>
    /// <param name="value">The <see cref="Vector4"/> to compare with this instance.</param>
    /// <returns>
    /// <c>true</c> if the specified <see cref="Vector4"/> is equal to this instance; otherwise, <c>false</c>.
    /// </returns>
    public readonly bool Equals(BoundingBox value)
    {
        return Minimum == value.Minimum && Maximum == value.Maximum;
    }

    /// <summary>
    /// Determines whether the specified <see cref="object"/> is equal to this instance.
    /// </summary>
    /// <param name="value">The <see cref="object"/> to compare with this instance.</param>
    /// <returns>
    /// <c>true</c> if the specified <see cref="object"/> is equal to this instance; otherwise, <c>false</c>.
    /// </returns>
    public override readonly bool Equals(object? value)
    {
        return value is BoundingBox boundingBox && Equals(boundingBox);
    }
}
