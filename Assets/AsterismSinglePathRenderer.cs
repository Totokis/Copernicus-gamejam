using UnityEngine;

public class AsterismSinglePathRenderer: MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;

    public void DrawLine(Vector3 a, Vector3 b)
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPositions(new []{a,b});
    }
}