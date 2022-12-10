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
    public void DrawLine(Vector3 a, Vector3 b, Color? col = null)
    {
        if (col == null)
            col = Color.white;

        if(col == Color.black)
        {
            lineRenderer.gameObject.layer = LayerMask.NameToLayer("noPostProcesing");
            
        }

        lineRenderer.SetColor(col.Value);
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
        lineRenderer.Points.Clear();
        
        for (int i = 0; i < pointsCount; i++)
        {
            lineRenderer.Points.Add(Vector2.zero);
        }
        
        StartCoroutine(AnimatePolygon());
    }
    
    private IEnumerator AnimatePolygon()
    {
        
        var segmentDuration = wholeAnimationDuration / pointsCount;
        for (int i = 0; i < pointsCount - 1; i++)
        {
            var startTime = Time.time;
            
            var startPosition = linePoints[i];
            var endPosition = linePoints[i + 1];

            lineRenderer.Points[i] = linePoints[i];
            
            var pos = startPosition;
        
            while (pos!=endPosition)
            {
                var t = (Time.time - startTime) / segmentDuration;
                pos = Vector3.Lerp(startPosition, endPosition, t);
        
                lineRenderer.Points[i+1] = pos;
                lineRenderer.ApplyPointPositionChanges();
                yield return null;
            }    
            
        }
    }
}