using System;
using System.Numerics;
using Raylib_cs;

namespace VTTiny.Extensions
{
    /// <summary>
    /// Provides extensions for the Raylib Color struct.
    /// </summary>
    public static class ColorExtensions
    {
        /// <summary>
        /// Linearly interpolates from the beginning color to the goal.
        /// </summary>
        /// <param name="col">The beginning color.</param>
        /// <param name="goal">The goal color.</param>
        /// <param name="amount">The amount.</param>
        /// <returns>The interpolated value.</returns>
        public static Color Lerp(this Color col, Color goal, float amount)
        {
            amount = amount.Clamp01();

            var colorVec = col.ToVector4();
            var goalVec = goal.ToVector4();

            return Vector4.Lerp(colorVec, goalVec, amount)
                          .ToColor();
        }

        /// <summary>
        /// Converts a color to a Vector4.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns>The resulting vector.</returns>
        public static Vector4 ToVector4(this Color color)
        {
            return new Vector4(color.r / 255f, color.g / 255f, color.b / 255f, color.a / 255f);
        }

        /// <summary>
        /// Converts a Vector4 to a color.
        /// </summary>
        /// <param name="vec">The vector.</param>
        /// <returns>The resulting color.</returns>
        public static Color ToColor(this Vector4 vec)
        {
            return new Color((int)(vec.X.Clamp01() * 255), 
                             (int)(vec.Y.Clamp01() * 255), 
                             (int)(vec.Z.Clamp01() * 255), 
                             (int)(vec.W.Clamp01() * 255));
        }
    }
}
