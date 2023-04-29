using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FSS.Settings.RoomConfig
{
    public class RoomConfigTest : MonoBehaviour
    {
        private void OnGUI()
        {
            if (GUI.Button(new Rect(0, 0, 200, 200), "Change to GCQ"))
            {
                FindObjectOfType<RoomConfigManager>().SetDeviceType(DeviceType.GCQ);
            }

            if (GUI.Button(new Rect(0, 200, 200, 200), "Change to GCH"))
            {
                FindObjectOfType<RoomConfigManager>().SetDeviceType(DeviceType.GCH);
            }

            if (GUI.Button(new Rect(0, 400, 200, 200), "Change to GC2"))
            {
                FindObjectOfType<RoomConfigManager>().SetDeviceType(DeviceType.GC2);
            }

            if (GUI.Button(new Rect(0, 600, 200, 200), "Change to GC3"))
            {
                FindObjectOfType<RoomConfigManager>().SetDeviceType(DeviceType.GC3);
            }

            if (GUI.Button(new Rect(0, 800, 200, 200), "Guardar"))
            {
                FindObjectOfType<RoomConfigManager>().SaveRoomConfig();
            }
        }
    }
}
