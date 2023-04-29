using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FSS.Settings
{
    [Serializable]
    public class DeviceRoomRotation : ICloneable
    {
        [SerializeField]
        private double rotationAdjustDeg;
        [SerializeField]
        private double elevationAdjustDeg;
        [SerializeField]
        private double azimuthAdjustDeg;

        [JsonProperty("RotationAdjust_deg")]
        public double RotationAdjustDeg { get => rotationAdjustDeg; set => rotationAdjustDeg = value; }
        [JsonProperty("ElevationAdjust_deg")]
        public double ElevationAdjustDeg { get => elevationAdjustDeg; set => elevationAdjustDeg = value; }
        [JsonProperty("AzimuthAdjust_deg")]
        public double AzimuthAdjustDeg { get => azimuthAdjustDeg; set => azimuthAdjustDeg = value; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}