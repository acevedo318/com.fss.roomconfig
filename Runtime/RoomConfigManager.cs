using UnityEngine;
using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine.Events;

namespace FSS.Settings.RoomConfig
{
    public class RoomConfigManager : MonoBehaviour
    {
        #region Settings
        [Header("Settings")]
        [SerializeField]
        private DeviceType _deviceType;

        public DeviceType DeviceType { get => _deviceType; }
        [SerializeField]
        private double messageTransferLatency_ms;
        [SerializeField]
        private double screenWidth_m;
        [SerializeField]
        private double screenHeight_m;
        [SerializeField]
        private double golferEyeHeight_m;
        public double MessageTransferLatency_ms { get => messageTransferLatency_ms; set => messageTransferLatency_ms = value; }
        public double ScreenWidth_m { get => screenWidth_m; set => screenWidth_m = value; }
        public double ScreenHeight_m { get => screenHeight_m; set => screenHeight_m = value; }
        public double GolferEyeHeight_m { get => golferEyeHeight_m; set => golferEyeHeight_m = value; }
        [SerializeField]
        private ForesightRoomConfig.DeviceRoomConfig _gcConfig;
        public ForesightRoomConfig.DeviceRoomConfig GcConfig { get => _gcConfig; set => _gcConfig = value; }
        private ClickToMove clickToMove;
        #endregion
        #region Events
        [field: Header("Eventos")]
        [field: SerializeField]
        public UnityEvent OnDeviceType { get; set; }
        #endregion
        #region Json Config
        [field: SerializeField]
        public ForesightRoomConfig ForesightRoomConfig { get; set; }
        private readonly string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Foresight", "ForesightRoomConfig.json");
        #endregion

        private void Awake()
        {
            clickToMove = FindObjectOfType<ClickToMove>();
            LoadFile();
            AsignGcConfig();
        }

        private void OnEnable()
        {
            OnDeviceType.AddListener(AsignGcConfig);
        }

        private void OnDisable()
        {
            OnDeviceType.RemoveListener(AsignGcConfig);
        }

        private void AsignGcConfig()
        {
            ResetRoomConfig();

            GcConfig = DeviceType switch
            {
                DeviceType.GC2 => (ForesightRoomConfig.DeviceRoomConfig)ForesightRoomConfig.GC2Config.Clone(),
                DeviceType.GC3 => (ForesightRoomConfig.DeviceRoomConfig)ForesightRoomConfig.GC3Config.Clone(),
                DeviceType.GCH => (ForesightRoomConfig.DeviceRoomConfig)ForesightRoomConfig.GCHConfig.Clone(),
                DeviceType.GCQ => (ForesightRoomConfig.DeviceRoomConfig)ForesightRoomConfig.GCQConfig.Clone(),
                _ => new(),
            };

            this.MessageTransferLatency_ms = ForesightRoomConfig.MessageTransferLatency_ms;
            this.ScreenHeight_m = ForesightRoomConfig.ScreenHeight_m;
            this.screenWidth_m = ForesightRoomConfig.ScreenWidth_m;
            this.GolferEyeHeight_m = ForesightRoomConfig.GolferEyeHeight_m;
        }

        public void SetDeviceType(DeviceType type)
        {
            _deviceType = type;
            ResetRoomConfig();

            clickToMove.IsMovingX = _deviceType != DeviceType.GCH;
            OnDeviceType.Invoke();
        }

        private void LoadFile()
        {
            if (!File.Exists(filePath))
            {
                Debug.LogError("ForesightRoom Config.json file not found");
                return;
            }

            var jsonString = File.ReadAllText(filePath);
            ForesightRoomConfig = JsonConvert.DeserializeObject<ForesightRoomConfig>(jsonString);
        }

        public void SaveRoomConfig()
        {
            CorrectValues();
            ChangeDataRoomConfig();
            try
            {
                File.WriteAllText(filePath, JsonConvert.SerializeObject(ForesightRoomConfig));
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to save ForesightRoom Config.json file: {e.Message}");
            }
        }

        private void CorrectValues()
        {
            var dz = Math.Abs(GcConfig.RightHandRoomOffset_m.Z) * -1;
            var dx = Math.Abs(GcConfig.RightHandRoomOffset_m.X);
            var dy = Math.Abs(GcConfig.RightHandRoomOffset_m.Y);

            //Dz Changes
            GcConfig.RightHandRoomOffset_m.Z = dz;
            GcConfig.LeftHandRoomOffset_m.Z = dz;
            GcConfig.RightTeeOffset_m.Z = dz;
            GcConfig.LeftTeeOffset_m.Z = dz;

            //Dx Changes
            GcConfig.RightHandRoomOffset_m.X = dx;
            GcConfig.RightTeeOffset_m.X = dx - 0.4572f;
            GcConfig.LeftHandRoomOffset_m.X = -dx;
            GcConfig.LeftTeeOffset_m.X = -(dx - 0.4572f);

            //Dy Changes
            GcConfig.RightHandRoomOffset_m.Y = dy;
            GcConfig.LeftHandRoomOffset_m.Y = dy;
            GcConfig.RightTeeOffset_m.Y = dy;
            GcConfig.LeftTeeOffset_m.Y = dy;
        }

        private void ChangeDataRoomConfig()
        {
            ForesightRoomConfig.MessageTransferLatency_ms = this.MessageTransferLatency_ms;
            ForesightRoomConfig.ScreenWidth_m = this.ScreenWidth_m;
            ForesightRoomConfig.ScreenHeight_m = this.ScreenHeight_m;
            ForesightRoomConfig.GolferEyeHeight_m = this.GolferEyeHeight_m;
            switch (DeviceType)
            {
                case DeviceType.GC2:
                    ForesightRoomConfig.GC2Config = GcConfig;
                    break;
                case DeviceType.GC3:
                    ForesightRoomConfig.GC3Config = GcConfig;
                    break;
                case DeviceType.GCQ:
                    ForesightRoomConfig.GCQConfig = GcConfig;
                    break;
                case DeviceType.GCH:
                    ForesightRoomConfig.GCHConfig = GcConfig;
                    break;
                default:
                    Debug.Log("Device type is not supported");
                    break;
            }
        }
        public void ResetRoomConfig() => LoadFile();
    }
}