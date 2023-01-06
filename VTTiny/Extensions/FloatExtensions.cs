using System;

namespace VTTiny.Extensions
{
    /// <summary>
    /// Extensions for the float type.
    /// </summary>
    public static class FloatExtensions
    {
        /// <summary>
        /// Clamp a float between min and max.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The clamped value.</returns>
        public static float Clamp(this float value, float min, float max)
        {
            return MathF.Min(MathF.Max(min, value), max);
        }

        /// <summary>
        /// Clamps a float between [0-1]
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The clamped value.</returns>
        public static float Clamp01(this float value)
        {
            return value.Clamp(0, 1);
        }
    }
}
