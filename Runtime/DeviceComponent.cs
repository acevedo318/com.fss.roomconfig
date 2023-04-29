using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceComponent : MonoBehaviour
{
    public GameObject[] devices = new GameObject[0x4];

    public void ShowDevice(int deviceIndex)
    {
        // Ocultar todos los objetos
        foreach (GameObject device in devices)
        {
            device.SetActive(false);
        }

        // Mostrar el objeto seleccionado
        devices[deviceIndex].SetActive(true);
    }
}
