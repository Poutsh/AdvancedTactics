using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Design;

namespace Advanced_Tactics
{
    public struct Vector
    {
        public int X;
        public int Y;

        #region CONSTRUCTEUR
        public Vector(float value)
        {
            X = (int)value;
            Y = (int)value;
        }
        public Vector(int value)
        {
            X = value;
            Y = value;
        }
        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }
        public Vector(float x, float y)
        {
            X = (int)x;
            Y = (int)y;
        }
        public Vector(int x, float y)
        {
            X = x;
            Y = (int)y;
        }
        public Vector(float x, int y)
        {
            X = (int)x;
            Y = y;
        }
        #endregion


        #region OPERATION

        #region OPERATOR -
        public static Vector operator -(Vector value) { return new Vector(-value.X, -value.Y); }
        public static Vector operator -(Vector value1, Vector value2) { return new Vector(value1.X - value2.X, value1.Y - value2.Y); }
        #endregion

        #region OPERATOR !=
        public static bool operator !=(Vector value1, Vector value2) { return !(value1 == value2); }
        public static bool operator !=(Vector2 value1, Vector value2) { return !(value1 == value2); }
        public static bool operator !=(Vector value1, Vector2 value2) { return !(value1 == value2); }
        #endregion

        #region OPERATOR *
        public static Vector operator *(float scaleFactor, Vector value) { return new Vector(scaleFactor * value.X, scaleFactor * value.Y); }
        public static Vector operator *(int scaleFactor, Vector value) { return new Vector(scaleFactor * value.X, scaleFactor * value.Y); }
        public static Vector operator *(Vector value, float scaleFactor) { return new Vector(scaleFactor * value.X, scaleFactor * value.Y); }
        public static Vector operator *(Vector value, int scaleFactor) { return new Vector(scaleFactor * value.X, scaleFactor * value.Y); }

        public static Vector operator *(Vector value1, Vector value2) { return new Vector(value1.X * value2.X, value1.Y * value2.Y); }
        public static Vector operator *(Vector2 value1, Vector value2) { return new Vector(value1.X * value2.X, value1.Y * value2.Y); }
        public static Vector operator *(Vector value1, Vector2 value2) { return new Vector(value1.X * value2.X, value1.Y * value2.Y); }
        #endregion

        #region OPERATOR /
        public static Vector operator /(Vector value, float divider) { return new Vector(divider / value.X, divider / value.Y); }
        public static Vector operator /(Vector value, int divider) { return new Vector(divider / value.X, divider / value.Y); }
        public static Vector operator /(Vector value1, Vector value2) { return new Vector(value1.X / value2.X, value1.Y / value2.Y); }
        public static Vector operator /(Vector2 value1, Vector value2) { return new Vector(value1.X / value2.X, value1.Y / value2.Y); }
        public static Vector operator /(Vector value1, Vector2 value2) { return new Vector(value1.X / value2.X, value1.Y / value2.Y); }
        #endregion

        #region OPERATOR +
        public static Vector operator +(Vector value1, Vector value2) { return new Vector(value1.X + value2.X, value1.Y + value2.Y); }
        public static Vector operator +(Vector2 value1, Vector value2) { return new Vector(value1.X + value2.X, value1.Y + value2.Y); }
        public static Vector operator +(Vector value1, Vector2 value2) { return new Vector(value1.X + value2.X, value1.Y + value2.Y); }
        #endregion

        #region OPERATOR ==
        public static bool operator ==(Vector value1, Vector value2) { return (value1.X == value2.X) && (value1.Y == value2.Y); }
        public static bool operator ==(Vector2 value1, Vector value2) { return ((int)value1.X == value2.X) && ((int)value1.Y == value2.Y); }
        public static bool operator ==(Vector value1, Vector2 value2) { return (value1.X == (int)value2.X) && (value1.Y == (int)value2.Y); }
        #endregion

        #endregion

        #region FUNCTIONS
        public static Vector One { get { return new Vector(1, 1); } }
        public static Vector UnitX { get { return new Vector(1, 0); } }
        public static Vector UnitY { get { return new Vector(0, 1); } }
        public static Vector Zero { get { return new Vector(0, 0); } }

        public static Vector Add(Vector value1, Vector value2) { return new Vector(value1.X + value2.X, value1.Y + value2.Y); }
        public static Vector Add(Vector2 value1, Vector value2) { return new Vector(value1.X + value2.X, value1.Y + value2.Y); }
        public static Vector Add(Vector value1, Vector2 value2) { return new Vector(value1.X + value2.X, value1.Y + value2.Y); }

        public static void Add(ref Vector value1, ref Vector value2, out Vector result) { result = new Vector(value1.X + value2.X, value1.Y + value2.Y); }
        public static void Add(ref Vector2 value1, ref Vector value2, out Vector result) { result = new Vector(value1.X + value2.X, value1.Y + value2.Y); }
        public static void Add(ref Vector value1, ref Vector2 value2, out Vector result) { result = new Vector(value1.X + value2.X, value1.Y + value2.Y); }

        public static int Distance(Vector value1, Vector value2)
        {
            float num2 = value1.X - value2.X;
            float num = value1.Y - value2.Y;
            float num3 = (num2 * num2) + (num * num);
            return (int)Math.Sqrt((double)num3);
        }
        public static int Distance(Vector value1, Vector2 value2)
        {
            float num2 = value1.X - value2.X;
            float num = value1.Y - value2.Y;
            float num3 = (num2 * num2) + (num * num);
            return (int)Math.Sqrt((double)num3);
        }
        public static int Distance(Vector2 value1, Vector value2)
        {
            float num2 = value1.X - value2.X;
            float num = value1.Y - value2.Y;
            float num3 = (num2 * num2) + (num * num);
            return (int)Math.Sqrt((double)num3);
        }
        public static int Distance(Vector2 value1, Vector2 value2)
        {
            float num2 = value1.X - value2.X;
            float num = value1.Y - value2.Y;
            float num3 = (num2 * num2) + (num * num);
            return (int)Math.Sqrt((double)num3);
        }


        public static void Distance(ref Vector value1, ref Vector value2, out int result)
        {
            float num2 = value1.X - value2.X;
            float num = value1.Y - value2.Y;
            float num3 = (num2 * num2) + (num * num);
            result = (int)Math.Sqrt((double)num3);
        }
        public static void Distance(ref Vector2 value1, ref Vector value2, out int result)
        {
            float num2 = value1.X - value2.X;
            float num = value1.Y - value2.Y;
            float num3 = (num2 * num2) + (num * num);
            result = (int)Math.Sqrt((double)num3);
        }
        public static void Distance(ref Vector value1, ref Vector2 value2, out int result)
        {
            float num2 = value1.X - value2.X;
            float num = value1.Y - value2.Y;
            float num3 = (num2 * num2) + (num * num);
            result = (int)Math.Sqrt((double)num3);
        }
        public static void Distance(ref Vector2 value1, ref Vector2 value2, out int result)
        {
            float num2 = value1.X - value2.X;
            float num = value1.Y - value2.Y;
            float num3 = (num2 * num2) + (num * num);
            result = (int)Math.Sqrt((double)num3);
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector)
            {
                Vector otherVector = (Vector)obj;
                return otherVector == this;
            }
            else
            {
                return false;
            }
        }

        public bool Equals(Vector other) { return other == this; }

        public override int GetHashCode() { return (int)((X + Y) % Int32.MaxValue); }

        public float Length() { return (float)Math.Sqrt(X * X + Y * Y); }

        public float LengthSquared() { return (X * X + Y * Y); }

        public static Vector Max(Vector value1, Vector value2) { return new Vector(Math.Max(value1.X, value2.X), Math.Max(value1.Y, value2.Y)); }
        public static Vector Max(Vector2 value1, Vector value2) { return new Vector(Math.Max(value1.X, value2.X), Math.Max(value1.Y, value2.Y)); }
        public static Vector Max(Vector value1, Vector2 value2) { return new Vector(Math.Max(value1.X, value2.X), Math.Max(value1.Y, value2.Y)); }
        public static Vector Max(Vector2 value1, Vector2 value2) { return new Vector(Math.Max(value1.X, value2.X), Math.Max(value1.Y, value2.Y)); }

        public static void Max(ref Vector value1, ref Vector value2, out Vector result) { result = new Vector(Math.Max(value1.X, value2.X), Math.Max(value1.Y, value2.Y)); }
        public static void Max(ref Vector2 value1, ref Vector value2, out Vector result) { result = new Vector(Math.Max(value1.X, value2.X), Math.Max(value1.Y, value2.Y)); }
        public static void Max(ref Vector value1, ref Vector2 value2, out Vector result) { result = new Vector(Math.Max(value1.X, value2.X), Math.Max(value1.Y, value2.Y)); }
        public static void Max(ref Vector2 value1, ref Vector2 value2, out Vector result) { result = new Vector(Math.Max(value1.X, value2.X), Math.Max(value1.Y, value2.Y)); }

        public static Vector Min(Vector value1, Vector value2) { return new Vector(Math.Min(value1.X, value2.X), Math.Min(value1.Y, value2.Y)); }
        public static Vector Min(Vector2 value1, Vector value2) { return new Vector(Math.Min(value1.X, value2.X), Math.Min(value1.Y, value2.Y)); }
        public static Vector Min(Vector value1, Vector2 value2) { return new Vector(Math.Min(value1.X, value2.X), Math.Min(value1.Y, value2.Y)); }
        public static Vector Min(Vector2 value1, Vector2 value2) { return new Vector(Math.Min(value1.X, value2.X), Math.Min(value1.Y, value2.Y)); }

        public static void Min(ref Vector value1, ref Vector value2, out Vector result) { result = new Vector(Math.Min(value1.X, value2.X), Math.Min(value1.Y, value2.Y)); }
        public static void Min(ref Vector2 value1, ref Vector value2, out Vector result) { result = new Vector(Math.Min(value1.X, value2.X), Math.Min(value1.Y, value2.Y)); }
        public static void Min(ref Vector value1, ref Vector2 value2, out Vector result) { result = new Vector(Math.Min(value1.X, value2.X), Math.Min(value1.Y, value2.Y)); }
        public static void Min(ref Vector2 value1, ref Vector2 value2, out Vector result) { result = new Vector(Math.Min(value1.X, value2.X), Math.Min(value1.Y, value2.Y)); }

        public override string ToString() { return ToString(null, null); }
        public string ToString(string format, IFormatProvider formatProvider)
        {
            // If no format is passed
            if (format == null || format == "") return String.Format("({0}, {1})", X, Y);

            char firstChar = format[0];
            string remainder = null;

            if (format.Length > 1)
                remainder = format.Substring(1);

            switch (firstChar)
            {
                case 'x': return X.ToString(remainder, formatProvider);
                case 'y': return Y.ToString(remainder, formatProvider);
                default:
                    return String.Format
                        (
                            "({0}, {1})",
                            X.ToString(format, formatProvider),
                            Y.ToString(format, formatProvider)
                        );
            }
        }
        #endregion
    }
}
