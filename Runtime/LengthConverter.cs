using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FSS.Settings.RoomConfig
{
    /// <summary>
    /// A class for converting between different units of length.
    /// </summary>
    public static class LengthConverter
    {
        /// <summary>
        /// Converts meters to centimeters.
        /// </summary>
        /// <param name="meters">The length in meters.</param>
        /// <returns>The length in centimeters.</returns>
        public static double MetersToCentimeters(double meters)
        {
            return meters * 100;
        }

        /// <summary>
        /// Converts centimeters to meters.
        /// </summary>
        /// <param name="centimeters">The length in centimeters.</param>
        /// <returns>The length in meters.</returns>
        public static double CentimetersToMeters(double centimeters)
        {
            return centimeters / 100;
        }

        /// <summary>
        /// Converts feet to inches.
        /// </summary>
        /// <param name="feet">The length in feet.</param>
        /// <returns>The length in inches.</returns>
        public static double FeetToInches(double feet)
        {
            return feet * 12;
        }

        /// <summary>
        /// Converts inches to feet.
        /// </summary>
        /// <param name="inches">The length in inches.</param>
        /// <returns>The length in feet.</returns>
        public static double InchesToFeet(double inches)
        {
            return inches / 12;
        }

        /// <summary>
        /// Converts meters to feet.
        /// </summary>
        /// <param name="meters">The length in meters.</param>
        /// <returns>The length in feet.</returns>
        public static double MetersToFeet(double meters)
        {
            return meters * 3.28084;
        }

        /// <summary>
        /// Converts feet to meters.
        /// </summary>
        /// <param name="feet">The length in feet.</param>
        /// <returns>The length in meters.</returns>
        public static double FeetToMeters(double feet)
        {
            return feet / 3.28084;
        }

        /// <summary>
        /// Converts centimeters to inches.
        /// </summary>
        /// <param name="centimeters">The length in centimeters.</param>
        /// <returns>The length in inches.</returns>
        public static double CentimetersToInches(double centimeters)
        {
            return centimeters / 2.54;
        }

        public static double MetersToInche(double meters) => meters * 39.3701f;

        /// <summary>
        /// Converts inches to centimeters.
        /// </summary>
        /// <param name="inches">The length in inches.</param>
        /// <returns>The length in centimeters.</returns>
        public static double InchesToCentimeters(double inches)
        {
            return inches * 2.54;
        }

        public static double ConvertToMeters(double value, UnitType unit)
        {
            return unit switch
            {
                UnitType.Centimeters => CentimetersToMeters(value),
                UnitType.Feets => FeetToMeters(value),
                UnitType.Inches => value * 0.0254f,
                UnitType.Meters => value,
                _ => throw new ArgumentException("Invalid unit"),
            };
        }

        public static double ConvertMetersToUnit(double value, UnitType unit)
        {
            return unit switch
            {
                UnitType.Centimeters => MetersToCentimeters(value),
                UnitType.Feets => MetersToFeet(value),
                UnitType.Inches => MetersToInche(value),
                UnitType.Meters => value,
                _ => throw new ArgumentException("Invalid unit"),
            };
        }

        public static double Truncate(double value, int decimalPlaces)
        {
            double multiplier = Math.Pow(10, decimalPlaces);
            return Math.Truncate(value * multiplier) / multiplier;
        }

        public static string GetSimbol(UnitType unit)
        {
            return unit switch
            {
                UnitType.Meters => "m",
                UnitType.Centimeters => "cm",
                UnitType.Inches => "in",
                UnitType.Feets => "ft",
                _ => throw new ArgumentException("Invalid unit")
            };
        }
    }
}

