using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexNode : MonoBehaviour
{
    public HexNode upRight;
    public HexNode upLeft;
    public HexNode up;
    public HexNode down;
    public HexNode downRight;
    public HexNode downLeft;

    public Boolean IsCompleted;
    public List<MoveDirection> VisitsFromWhereDirections;

    public void AddVisit(MoveDirection visit)
    {
        if (VisitsFromWhereDirections.Count == 0)
        {
            LeanTween.scale(gameObject, Vector3.one, UnityEngine.Random.Range(0.420f, 0.69f))
                .setEaseInOutBack();
        }
        VisitsFromWhereDirections.Add(visit);
    }

    public void SetAsCompleted()
    {
        IsCompleted = true;
    }

    private void Start()
    {
        VisitsFromWhereDirections = new List<MoveDirection>();
    }
}