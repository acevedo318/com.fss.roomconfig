using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;
using UnityEngine.UI;

namespace FSS.Settings.RoomConfig
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField]
        private RoomConfigManager roomConfigManager;
        [SerializeField]
        private GameObject panelMenu;
        [SerializeField]
        private TMP_Dropdown deviceType;
        [SerializeField]
        private UnitType unitType = UnitType.Meters;
        [SerializeField]
        private Button[] units = new Button[4];
        [SerializeField]
        private TMP_InputField[] unitsInputs = new TMP_InputField[5];
        [SerializeField]
        private TMP_InputField inputFieldWidth;
        [SerializeField]
        private TMP_InputField inputFieldHeight;
        [SerializeField]
        private TMP_InputField inputFieldFromScreen;
        [SerializeField]
        private TMP_InputField inputFieldToCenter;
        [SerializeField]
        private TMP_InputField inputFieldSurfaceHeight;

        private RoomController roomController;

        private ImageMeauserementsComponent imageMeauserementsComponent;

        private DeviceComponent deviceComponent;
        [SerializeField]
        private Button[] handButtons = new Button[2];

        [Header("Scene UI")]
        [SerializeField]
        private TMP_Text textWidthRoom;
        [SerializeField]
        private TMP_Text textHeightRoom;

        [Header("Config")]
        [SerializeField]
        private bool isVisible = true;
        [SerializeField]
        private Hand hand = Hand.Right;

        public Hand Hand { get => this.hand; }

        private void Awake()
        {
            deviceType.AddOptions(Enum.GetNames(typeof(DeviceType)).ToList());
            var indexUnitType = Enum.GetNames(typeof(UnitType)).ToList().IndexOf(Enum.GetName(typeof(UnitType), unitType));
            roomController = FindObjectOfType<RoomController>();
            imageMeauserementsComponent = FindObjectOfType<ImageMeauserementsComponent>();
            deviceComponent = FindObjectOfType<DeviceComponent>();
        }

        private void Start()
        {
            UpdateMenu();

            inputFieldWidth.onValueChanged.AddListener(SetWidthScreen);
            inputFieldHeight.onValueChanged.AddListener(SetHeightScreen);

            inputFieldFromScreen.onValueChanged.AddListener(SetFromScreen);
            inputFieldToCenter.onValueChanged.AddListener(SetToCenter);
            deviceType.onValueChanged.AddListener(OnDeviceChange);

            UpdateDevicePosition();

            if (!isVisible)
            {
                GetComponent<Canvas>().enabled = false;
            }

            if((int)hand == 0)
            {
                handButtons[0].interactable = false;
            }
            else
            {
                handButtons[1].interactable = false;
            }

            for (int i = 0; i < units.Length; i++)
            {
                if(i == (int)unitType)
                {
                    units[i].interactable = false;
                }
                else
                {
                    units[i].interactable = true;
                }
            }
        }

        public void ChangeUnit(int unit)
        {
            this.unitType = (UnitType)unit;

            foreach (var item in unitsInputs)
            {
                item.text = Enum.GetNames(typeof(UnitType))[unit];
            }
            UpdateMenu();

            for (int i = 0; i < units.Length; i++)
            {
                if (i == (int)unitType)
                {
                    units[i].interactable = false;
                }
                else
                {
                    units[i].interactable = true;
                }
            }
        }

        public void OnDeviceChange(int indexDeviceType)
        {
            roomConfigManager.SetDeviceType(Enum.Parse<DeviceType>(deviceType.value.ToString()));
            deviceComponent.ShowDevice(indexDeviceType);
            UpdateMenu();
        }

        public void OnChangeHand(int hand)
        {
            this.hand = (Hand)hand;
            roomConfigManager.SetDeviceType(Enum.Parse<DeviceType>(deviceType.value.ToString()));
            UpdateMenu();
        }

        private void UpdateMenu()
        {
            inputFieldWidth.text = LengthConverter.ConvertMetersToUnit(roomConfigManager.ScreenWidth_m, unitType).ToString();
            inputFieldHeight.text = LengthConverter.ConvertMetersToUnit(roomConfigManager.ScreenHeight_m, unitType).ToString();

            if (this.hand == Hand.Right)
            {
                inputFieldFromScreen.text = LengthConverter.ConvertMetersToUnit(Math.Abs(roomConfigManager.GcConfig.RightHandRoomOffset_m.Z), unitType).ToString();
                inputFieldToCenter.text = LengthConverter.ConvertMetersToUnit(roomConfigManager.GcConfig.RightHandRoomOffset_m.X, unitType).ToString();
                inputFieldSurfaceHeight.text = LengthConverter.ConvertMetersToUnit(roomConfigManager.GcConfig.RightHandRoomOffset_m.Y, unitType).ToString();
            }
            else
            {
                inputFieldFromScreen.text = LengthConverter.ConvertMetersToUnit(Math.Abs(roomConfigManager.GcConfig.LeftHandRoomOffset_m.Z), unitType).ToString();
                inputFieldToCenter.text = LengthConverter.ConvertMetersToUnit(roomConfigManager.GcConfig.LeftHandRoomOffset_m.X, unitType).ToString();
                inputFieldSurfaceHeight.text = LengthConverter.ConvertMetersToUnit(roomConfigManager.GcConfig.LeftHandRoomOffset_m.Y, unitType).ToString();
            }


            textWidthRoom.text = $"IMAGE WIDTH\n{LengthConverter.Truncate(LengthConverter.ConvertMetersToUnit(roomConfigManager.ScreenWidth_m, unitType), 3)}{LengthConverter.GetSimbol(unitType)}";
            textHeightRoom.text = $"IMAGE HEIGHT\n{LengthConverter.Truncate(LengthConverter.ConvertMetersToUnit(roomConfigManager.ScreenHeight_m, unitType), 3)}{LengthConverter.GetSimbol(unitType)}";
        }

        public void SetWidthScreen(string newValue)
        {
            if (string.IsNullOrEmpty(newValue))
            {
                inputFieldWidth.text = LengthConverter.ConvertMetersToUnit(roomConfigManager.ScreenWidth_m, unitType).ToString();
                return;
            }

            roomConfigManager.ScreenWidth_m = LengthConverter.ConvertToMeters(Convert.ToDouble(newValue), unitType);
            imageMeauserementsComponent.UpdateImageSize();
            textWidthRoom.text = $"IMAGE WIDTH\n{LengthConverter.ConvertMetersToUnit(roomConfigManager.ScreenWidth_m, unitType)}{LengthConverter.GetSimbol(unitType)}";
        }

        public void SetHeightScreen(string newValue)
        {
            if (string.IsNullOrEmpty(newValue))
            {
                inputFieldHeight.text = LengthConverter.ConvertMetersToUnit(roomConfigManager.ScreenHeight_m, unitType).ToString();
                return;
            }

            roomConfigManager.ScreenHeight_m = LengthConverter.ConvertToMeters(Convert.ToDouble(newValue), unitType);
            imageMeauserementsComponent.UpdateImageSize();
            textHeightRoom.text = $"IMAGE HEIGHT\n{LengthConverter.ConvertMetersToUnit(roomConfigManager.ScreenHeight_m, unitType)}{LengthConverter.GetSimbol(unitType)}";
        }

        public void SetFromScreen(string newValue)
        {
            if (string.IsNullOrEmpty(newValue))
            {
                if(this.hand == Hand.Right)
                {
                    inputFieldFromScreen.text = roomConfigManager.GcConfig.RightHandRoomOffset_m.Z.ToString();
                }
                else
                {
                    inputFieldFromScreen.text = roomConfigManager.GcConfig.LeftHandRoomOffset_m.Z.ToString();
                }
                
                return;
            }

            if (Convert.ToDouble(newValue) < 0 || Convert.ToDouble(newValue) > 4f)
            {
                SetFromScreen(Mathf.Clamp((float)Convert.ToDouble(newValue), 0, 4f).ToString());
                inputFieldFromScreen.text = Mathf.Clamp((float)Convert.ToDouble(newValue), 0, 4f).ToString();
                return;
            }

            if (this.hand == Hand.Right)
            {
                roomConfigManager.GcConfig.RightHandRoomOffset_m.Z = LengthConverter.ConvertToMeters(Mathf.Abs((float)Convert.ToDouble(newValue)) * -1, unitType);
            }
            else
            {
                roomConfigManager.GcConfig.LeftHandRoomOffset_m.Z = LengthConverter.ConvertToMeters(Mathf.Abs((float)Convert.ToDouble(newValue)) * -1, unitType);
            }

            UpdateDevicePosition();
        }

        public void UpdateDeviceDistances()
        {
            if (this.hand == Hand.Right)
            {
                inputFieldFromScreen.text = LengthConverter.ConvertMetersToUnit(Math.Abs(roomConfigManager.GcConfig.RightHandRoomOffset_m.Z), unitType).ToString();
                inputFieldToCenter.text = LengthConverter.ConvertMetersToUnit(roomConfigManager.GcConfig.RightHandRoomOffset_m.X, unitType).ToString();
            }
            else
            {
                inputFieldFromScreen.text = LengthConverter.ConvertMetersToUnit(Math.Abs(roomConfigManager.GcConfig.LeftHandRoomOffset_m.Z), unitType).ToString();
                inputFieldToCenter.text = LengthConverter.ConvertMetersToUnit(roomConfigManager.GcConfig.LeftHandRoomOffset_m.X, unitType).ToString();
            }

            if ((int)hand == 0)
            {
                handButtons[0].interactable = false;
                handButtons[1].interactable = true;
            }
            else
            {
                handButtons[1].interactable = false;
                handButtons[0].interactable = true;
            }

        }

        public void SetToCenter(string newValue)
        {
            if (string.IsNullOrEmpty(newValue))
            {
                if (this.hand == Hand.Right)
                {
                    inputFieldToCenter.text = roomConfigManager.GcConfig.RightHandRoomOffset_m.X.ToString();
                }
                else
                {
                    inputFieldToCenter.text = roomConfigManager.GcConfig.LeftHandRoomOffset_m.X.ToString();
                }
                return;
            }

            if (this.hand == Hand.Right)
            {
                if (Convert.ToDouble(newValue) < 0 || Convert.ToDouble(newValue) > 1.5f)
                {
                    SetToCenter(Mathf.Clamp((float)Convert.ToDouble(newValue), 0, 1.5f).ToString());
                    inputFieldToCenter.text = Mathf.Clamp((float)Convert.ToDouble(newValue), 0, 1.5f).ToString();
                    return;
                }
                roomConfigManager.GcConfig.RightHandRoomOffset_m.X = LengthConverter.ConvertToMeters(Convert.ToDouble(newValue), unitType);
            }
            else
            {
                if (Convert.ToDouble(newValue) > 0 || Convert.ToDouble(newValue) < -1.5f)
                {
                    SetToCenter(Mathf.Clamp((float)Convert.ToDouble(newValue), -1.5f, 0).ToString());
                    inputFieldToCenter.text = Mathf.Clamp((float)Convert.ToDouble(newValue), -1.5f, 0).ToString();
                    return;
                }
                roomConfigManager.GcConfig.LeftHandRoomOffset_m.X = LengthConverter.ConvertToMeters(Convert.ToDouble(newValue), unitType);
            }
                
            UpdateDevicePosition();
        }

        public void SetSurfaceHeight(string newValue)
        {
            throw new NotImplementedException();
        }

        private void UpdateDevicePosition()
        {
            if(this.hand == Hand.Right)
            {
                roomController.SetDevicePositionFromDistance(new Vector3((float)roomConfigManager.GcConfig.RightHandRoomOffset_m.X, 0.25f, (float)roomConfigManager.GcConfig.RightHandRoomOffset_m.Z));
                return;
            }

            roomController.SetDevicePositionFromDistance(new Vector3((float)roomConfigManager.GcConfig.LeftHandRoomOffset_m.X, 0.25f, (float)roomConfigManager.GcConfig.LeftHandRoomOffset_m.Z));
        }
    }
}
