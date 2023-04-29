using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class HandRotationDeg : ICloneable
{
    [SerializeField]
    private float rotationAdjustDeg;
    [SerializeField]
    private float elevationAdjustDeg;
    [SerializeField]
    private float azimuthAdjustDeg;

    [JsonProperty("RotationAdjust_deg")]
    public float RotationAdjustDeg { get => rotationAdjustDeg; set => rotationAdjustDeg = value; }
    [JsonProperty("ElevationAdjust_deg")]
    public float ElevationAdjustDeg { get => elevationAdjustDeg; set => elevationAdjustDeg = value; }
    [JsonProperty("AzimuthAdjust_deg")]
    public float AzimuthAdjustDeg { get => azimuthAdjustDeg; set => azimuthAdjustDeg = value; }

    public object Clone()
    {
        return MemberwiseClone();
    }
}
