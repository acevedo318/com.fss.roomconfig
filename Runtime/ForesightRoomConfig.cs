using System;
using UnityEngine;

namespace FSS.Settings.RoomConfig
{
    [Serializable]
    public class ForesightRoomConfig
    {
        [SerializeField]
        private double messageTransferLatency_ms;
        [SerializeField]
        private double screenWidth_m;
        [SerializeField]
        private double screenHeight_m;
        [SerializeField]
        private double golferEyeHeight_m;
        [SerializeField]
        private DeviceRoomConfig gC2Config = new();
        [SerializeField]
        private DeviceRoomConfig gCQConfig = new();
        [SerializeField]
        private DeviceRoomConfig gCHConfig = new();
        [SerializeField]
        private DeviceRoomConfig gC3Config = new();
        [SerializeField]
        private DeviceRoomConfig falconConfig = new();

        public double MessageTransferLatency_ms { get => messageTransferLatency_ms; set => messageTransferLatency_ms = value; }
        public double ScreenWidth_m { get => screenWidth_m; set => screenWidth_m = value; }
        public double ScreenHeight_m { get => screenHeight_m; set => screenHeight_m = value; }
        public double GolferEyeHeight_m { get => golferEyeHeight_m; set => golferEyeHeight_m = value; }
        public DeviceRoomConfig GC2Config { get => gC2Config; set => gC2Config = value; }
        public DeviceRoomConfig GCQConfig { get => gCQConfig; set => gCQConfig = value; }
        public DeviceRoomConfig GCHConfig { get => gCHConfig; set => gCHConfig = value; }
        public DeviceRoomConfig GC3Config { get => gC3Config; set => gC3Config = value; }
        public DeviceRoomConfig FalconConfig { get => falconConfig; set => falconConfig = value; }

        [Serializable]
        public class DeviceRoomConfig
        {
            [SerializeField]
            private Vector4 rightHandRoomOffset_m = new();
            [SerializeField]
            private Vector4 leftHandRoomOffset_m = new();
            [SerializeField]
            private Vector4 rightTeeOffset_m = new();
            [SerializeField]
            private Vector4 leftTeeOffset_m = new();
            [SerializeField]
            private DeviceRoomRotation rightHandRotation_deg = new();
            [SerializeField]
            private DeviceRoomRotation leftHandRotation_deg = new();

            public Vector4 RightHandRoomOffset_m { get => rightHandRoomOffset_m; set => rightHandRoomOffset_m = value; }
            public Vector4 LeftHandRoomOffset_m { get => leftHandRoomOffset_m; set => leftHandRoomOffset_m = value; }
            public Vector4 RightTeeOffset_m { get => rightTeeOffset_m; set => rightTeeOffset_m = value; }
            public Vector4 LeftTeeOffset_m { get => leftTeeOffset_m; set => leftTeeOffset_m = value; }
            public DeviceRoomRotation RightHandRotation_deg { get => rightHandRotation_deg; set => rightHandRotation_deg = value; }
            public DeviceRoomRotation LeftHandRotation_deg { get => leftHandRotation_deg; set => leftHandRotation_deg = value; }

            public object Clone()
            {
                DeviceRoomConfig clone = (DeviceRoomConfig)this.MemberwiseClone();
                clone.RightHandRoomOffset_m = (Vector4)this.RightHandRoomOffset_m.Clone();
                clone.LeftHandRoomOffset_m = (Vector4)this.LeftHandRoomOffset_m.Clone();
                clone.RightTeeOffset_m = (Vector4)this.RightTeeOffset_m.Clone();
                clone.LeftTeeOffset_m = (Vector4)this.LeftTeeOffset_m.Clone();
                clone.RightHandRotation_deg = (DeviceRoomRotation)this.RightHandRotation_deg.Clone();
                clone.LeftHandRotation_deg = (DeviceRoomRotation)this.LeftHandRotation_deg.Clone();
                return clone;
            }
        }
    }
}
