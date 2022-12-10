using System;
using System.Collections;
using System.Collections.Generic;
using Game.Core.Rendering;
using UnityEngine;

public class AsterismSinglePathRenderer: MonoBehaviour
{
    [SerializeField] private MultiLineRenderer2D lineRenderer;
    [SerializeField] private float animationDuration = 0.5f;
    [SerializeField] private float wholeAnimationDuration = 1.5f;
    [SerializeField] private Camera _mainCamera;
    private int pointsCount;
    private List<Vector2> linePoints;

    private void OnEnable()
    {
        _mainCamera = Camera.main;
        lineRenderer.CurrentCamera = _mainCamera;
    }
    public void DrawLine(Vector3 a, Vector3 b)
    {
        lineRenderer.Points = new List<Vector2>
        {
            a,b
        };
        
        StartCoroutine(AnimateLine());
    }
    private IEnumerator AnimateLine()
    {
        var startTime = Time.time;

        var startPosition = lineRenderer.Points[0];
        var endPosition = lineRenderer.Points[1];

        var pos = startPosition;

        while (pos!=endPosition)
        {
            var t = (Time.time - startTime) / animationDuration;
            pos = Vector3.Lerp(startPosition, endPosition, t);
            lineRenderer.Points[1] = pos;
            lineRenderer.ApplyPointPositionChanges();

            yield return null;
        }
    }
    public void DrawPolygon(List<Vector2> points)
    {
        pointsCount = points.Count;
        linePoints = points;
        StartCoroutine(AnimatePolygon());
    }
    
    private IEnumerator AnimatePolygon()
    {
        lineRenderer.Points.Clear();
        lineRenderer.Points.Add(linePoints[0]);
        lineRenderer.Points.Add(linePoints[1]);
        // var segmentDuration = wholeAnimationDuration / pointsCount;
        // for (int i = 0; i < pointsCount - 1; i++)
        // {
        //     var startTime = Time.time;
        //     
        //     var startPosition = linePoints[i];
        //     var endPosition = linePoints[i + 1];
        //     lineRenderer.Points.Add(startPosition);
        //
        //     var pos = startPosition;
        //
        //     while (pos!=endPosition)
        //     {
        //         var t = (Time.time - startTime) / segmentDuration;
        //         pos = Vector3.Lerp(startPosition, endPosition, t);
        //
        //         lineRenderer.Points[i+1] = pos;
        //         lineRenderer.ApplyPointPositionChanges();
        //         yield return null;
        //     }    
        //     
        // }
        
        
        var startTime = Time.time;

        var startPosition = lineRenderer.Points[0];
        var endPosition = lineRenderer.Points[1];

        var pos = startPosition;

        while (pos!=endPosition)
        {
            var t = (Time.time - startTime) / animationDuration;
            pos = Vector3.Lerp(startPosition, endPosition, t);
            lineRenderer.Points[1] = pos;
            lineRenderer.ApplyPointPositionChanges();

            yield return null;
        }
        
    }
}