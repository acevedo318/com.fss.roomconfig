using System;
using System.Collections;
using UnityEngine;
namespace FSS.Settings.RoomConfig
{
    public class RoomController : MonoBehaviour
    {
        [SerializeField]
        private Transform pointReference;
        [SerializeField]
        private Transform device;
        private float previousDistance;
        [SerializeField]
        private Vector3 distancePointToDevice = Vector3.zero;

        void Start()
        {
            previousDistance = Vector3.Distance(pointReference.position, device.position);
            var distancePointToDeviceX = Mathf.Abs(device.position.x - pointReference.position.x);
            var distancePointToDeviceY = Mathf.Abs(device.position.y - pointReference.position.y);
            var distancePointToDeviceZ = Mathf.Abs(device.position.z - pointReference.position.z);
            distancePointToDevice = new Vector3(distancePointToDeviceX, distancePointToDeviceY, distancePointToDeviceZ);
            StartCoroutine(MeasureDistance());
        }

        IEnumerator MeasureDistance()
        {
            while (true)
            {
                // Wait for half a second before measuring the distance again
                yield return new WaitForSeconds(0.5f);
                // Calculate the current distance between the two objects
                float currentDistance = Vector3.Distance(pointReference.position, device.position);
                // Check if the distance has changed since the last calculation
                if (currentDistance != previousDistance)
                {
                    // Update the previous distance to the new distance
                    previousDistance = currentDistance;

                    var distancePointToDeviceX = Mathf.Abs(device.position.x - pointReference.position.x);
                    var distancePointToDeviceY = Mathf.Abs(device.position.y - pointReference.position.y);
                    var distancePointToDeviceZ = Mathf.Abs(device.position.z - pointReference.position.z);
                    distancePointToDevice = new Vector3(distancePointToDeviceX, distancePointToDeviceY, distancePointToDeviceZ);


                }
            }
        }

        public void SetDevicePositionFromDistance(Vector3 distance)
        {
            device.position = new Vector3(pointReference.position.x + distance.x, pointReference.position.y + distance.y, pointReference.position.z + distance.z);
        }

        public void UpdateRoomConfig()
        {
            var menuController = FindObjectOfType<MenuController>();
            var roomConfigManager = FindObjectOfType<RoomConfigManager>();

            if(menuController.Hand == Hand.Right)
            {
                roomConfigManager.GcConfig.RightHandRoomOffset_m.Z = -Math.Abs((pointReference.position.z - device.position.z));
                roomConfigManager.GcConfig.RightHandRoomOffset_m.X = (pointReference.position.x - device.position.x) * -1;
                if(roomConfigManager.DeviceType != DeviceType.GCH)
                {
                    device.rotation = new Quaternion(0, 0, 0, 0);
                }
            }
            else
            {
                roomConfigManager.GcConfig.LeftHandRoomOffset_m.Z = (pointReference.position.z - device.position.z);
                roomConfigManager.GcConfig.LeftHandRoomOffset_m.X = (pointReference.position.x - device.position.x) * -1;
                if (roomConfigManager.DeviceType != DeviceType.GCH)
                {
                    device.rotation = new Quaternion(0, 180, 0, 0);
                }
                else
                {
                    device.rotation = new Quaternion(0, 0, 0, 0);
                }
                
            }

            menuController.UpdateDeviceDistances();
        }
    }
}