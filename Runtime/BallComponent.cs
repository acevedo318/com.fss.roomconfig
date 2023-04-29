using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallComponent : MonoBehaviour
{
    [SerializeField]
    private Transform device;
    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, device.position.z);
    }
}
