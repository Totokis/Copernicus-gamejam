using System.Collections;
using UnityEngine;

public class AsterismSinglePathRenderer: MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float animationDuration = 0.5f;

    public void DrawLine(Vector3 a, Vector3 b)
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPositions(new []{a,b});

        StartCoroutine(AnimateLine());
    }
    private IEnumerator AnimateLine()
    {
        var startTime = Time.time;

        var startPosition = lineRenderer.GetPosition(0);
        var endPosition = lineRenderer.GetPosition(1);

        var pos = startPosition;

        while (pos!=endPosition)
        {
            var t = (Time.time - startTime) / animationDuration;
            pos = Vector3.Lerp(startPosition, endPosition, t);
            lineRenderer.SetPosition(1,pos);

            yield return null;
        }
    }
}