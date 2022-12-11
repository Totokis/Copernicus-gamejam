using Game.Core.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AsterismPathRenderer : MonoBehaviour
{

    [SerializeField] private AsterismSinglePathRenderer singlePathRendererPrefab;
    private List<AsterismSinglePathRenderer> singlePathRenderers = new();
    public static AsterismPathRenderer Instance;
    [SerializeField] private List<Vector2> points = new();


    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void RemoveFromBadMove(AsterismSinglePathRenderer toremove) => singlePathRenderers.Remove(toremove);

    public (MultiLineRenderer2D lr,  AsterismSinglePathRenderer ar) DrawLineFromPointToPoint(Vector3 pointA, Vector3 pointB, Color? col = null,bool attachToFinalAnimation=false)
    {
        AsterismSinglePathRenderer added = Instantiate(singlePathRendererPrefab, transform);
        
        singlePathRenderers.Add(added);
        var lr = singlePathRenderers.Last().DrawLine(pointA, pointB, col);

        if (attachToFinalAnimation)
        {
            if (points.Count == 0)
            {
                points.Add(pointA);
            }
            points.Add(pointB);    
        }

        return (lr, added);
    }

    public void DrawWholePath()
    {
        print("HUUUU");
        singlePathRenderers.ForEach(line=>line.gameObject.SetActive(false));

        foreach (var asterismSinglePathRenderer in singlePathRenderers)
        {
            Destroy(asterismSinglePathRenderer.gameObject);
        } 
        singlePathRenderers.Add(Instantiate(singlePathRendererPrefab,transform)); 
        singlePathRenderers.Last().DrawPolygon(points);
    }

    public void Resett()
    {
        foreach(var ren in singlePathRenderers)
        {
            if (ren)
                Destroy(ren.gameObject);
        }
        singlePathRenderers.Clear();
    }

    //private void Start() => DrawTestLine();
    //private void Start() => Invoke(nameof(DrawWholePath), 10f);

    private void DrawTestLine()
    {
        DrawLineFromPointToPoint(Vector3.zero, new Vector3(1, 0, 0));
        DrawLineFromPointToPoint(new Vector3(1, 0, 0), new Vector3(2, 2, 0));
        DrawLineFromPointToPoint(new Vector3(2, 2, 0), new Vector3(3, 4, 0));
    }
}