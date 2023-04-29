using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FSS.Settings
{
    [Serializable]
    public class Vector4 : ICloneable
    {
        [SerializeField]
        private double x;
        [SerializeField]
        private double y;
        [SerializeField]
        private double z;
        [SerializeField]
        private double w;

        public double X { get => x; set => x = value; }
        public double Y { get => y; set => y = value; }
        public double Z { get => z; set => z = value; }
        public double W { get => w; set => w = value; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public static explicit operator UnityEngine.Vector4(Vector4 v)
        {
            return new UnityEngine.Vector4((float)v.X, (float)v.Y, (float)v.Z, (float)v.W);
        }
    }
}