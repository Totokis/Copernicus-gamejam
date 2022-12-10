using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    void Start()
    {
        VisitsFromWhereDirections = new List<MoveDirection>();

        GetComponent<SpriteRenderer>().color = Color.HSVToRGB(51f / 360f, UnityEngine.Random.Range(21f, 46f) / 100f, 1f);
    }

    public void AddVisit(MoveDirection visit)
    {
        if (VisitsFromWhereDirections.Count == 0)
        {
            LeanTween.scale(gameObject, Vector3.one, UnityEngine.Random.Range(0.70f, 0.94f))
                .setEaseInOutBack()
                .setOnComplete(() =>
                {
                    LeanTween.scale(gameObject, new Vector3(0.8f, 0.8f), UnityEngine.Random.Range(0.6f, 1.2f))
                        .setLoopPingPong();
                });
        }
        VisitsFromWhereDirections.Add(visit);
    }

    public void SetAsCompleted()
    {
        IsCompleted = true;
    }

}