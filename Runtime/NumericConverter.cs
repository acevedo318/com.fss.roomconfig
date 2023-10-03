using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;

public class NumericConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(decimal) || objectType == typeof(decimal?) ||
                       objectType == typeof(double) || objectType == typeof(double?) ||
                       objectType == typeof(float) || objectType == typeof(float?);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
        {
            return null;
        }

        var value = reader.Value.ToString();
        if (objectType == typeof(decimal) || objectType == typeof(decimal?))
        {
            if (decimal.TryParse(value, out var decimalResult))
            {
                // Redondea a 3 decimales
                return Math.Round(decimalResult, 3);
            }
        }

        else if (objectType == typeof(double) || objectType == typeof(double?))
        {
            if (double.TryParse(value, out var doubleResult))
            {
                // Redondea a 3 decimales
                return Math.Round(doubleResult, 3);
            }
        }
        else if (objectType == typeof(float) || objectType == typeof(float?))
        {
            if (float.TryParse(value, out var floatResult))
            {
                // Redondea a 3 decimales
                return Math.Round(floatResult, 3);
            }
        }

        throw new JsonSerializationException($"Error al deserializar el valor '{value}' como {objectType}");
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value == null)
        {
            writer.WriteNull();
            return;
        }

        if (value is decimal decimalValue)
        {
            // Formatea el número decimal con 3 decimales
            var formattedValue = Math.Round(decimalValue, 3).ToString("0.###");
            writer.WriteValue(formattedValue);
        }
        else if (value is double doubleValue)
        {
            // Formatea el número double con 3 decimales
            var formattedValue = Math.Round(doubleValue, 3).ToString("0.###");
            writer.WriteValue(formattedValue);
        }
        else if (value is float floatValue)
        {
            // Formatea el número float con 3 decimales
            var formattedValue = Math.Round(floatValue, 3).ToString("0.###");
            writer.WriteValue(formattedValue);
        }
    }
}
