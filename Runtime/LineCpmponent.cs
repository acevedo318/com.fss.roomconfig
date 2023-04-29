using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCpmponent : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField]
    private Direction direction;

    private Vector3 initialPosition;

    public Transform device;
    [SerializeField]
    private Vector3[] positions = new Vector3[0];

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        initialPosition = lineRenderer.GetPosition(0);
    }

    public enum Direction { Vertical , Horizontal }

    private void Update()
    {
        if (direction == Direction.Vertical)
        {
            this.positions = new Vector3[] { new Vector3(device.position.x, initialPosition.y, initialPosition.z), new Vector3(device.position.x, initialPosition.y, device.position.z)};
        }
        else
        {
            this.positions = new Vector3[] { new Vector3(initialPosition.x, initialPosition.y, device.position.z), new Vector3(device.position.x, initialPosition.y, device.position.z) };
        }
        lineRenderer.SetPositions(positions);
    }
}