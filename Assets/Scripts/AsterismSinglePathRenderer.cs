using System;
using System.Collections;
using System.Collections.Generic;
using Game.Core.Rendering;
using UnityEngine;

public class AsterismSinglePathRenderer: MonoBehaviour
{
    [SerializeField] private MultiLineRenderer2D lineRenderer;
    [SerializeField] private float animationDuration = 0.5f;
    [SerializeField] private Camera _mainCamera;

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
}