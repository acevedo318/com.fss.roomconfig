using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DottedLine : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Vector3[] startPositions;
    public Vector3[] endPositions;

    void Start()
    {
        // Establece la cantidad de posiciones que tendrán las líneas
        lineRenderer.positionCount = startPositions.Length * 2;

        // Crea una lista temporal de posiciones que se utilizarán para la línea
        List<Vector3> positions = new List<Vector3>();

        // Agrega las posiciones de inicio y finalización para cada línea
        for (int i = 0; i < startPositions.Length; i++)
        {
            positions.Add(startPositions[i]);
            positions.Add(endPositions[i]);
        }

        // Establece las posiciones para el LineRenderer
        lineRenderer.SetPositions(positions.ToArray());
    }
}
