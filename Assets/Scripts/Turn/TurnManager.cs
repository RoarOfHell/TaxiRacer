using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private Transform _positionCar;
    private LineRenderer lineRenderer;
    private List<Vector3> pointsPosition = new List<Vector3>();

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    void Update()
    {
        if (pointsPosition.Count > 0)
        {
            lineRenderer.positionCount = pointsPosition.Count;
            lineRenderer.SetPositions(pointsPosition.ToArray());
            lineRenderer.SetPosition(0, GameObject.Find("Car").gameObject.transform.position);
        }
        else
        {
            lineRenderer.positionCount = 0;
        }
    }

    public void AddPoint(Vector3 point)
    {
        pointsPosition.Add(point);
        Debug.Log($"add: {point}");
    }

    public void RemovePoint(Vector3 point)
    {
        pointsPosition.Remove(point);
    }

    public void ClearPoints()
    {
        pointsPosition.Clear();
    }

    public void RemoveNext()
    {
        pointsPosition.RemoveAt(1);
    }
}
