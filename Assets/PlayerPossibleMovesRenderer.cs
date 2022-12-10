using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPossibleMovesRenderer : MonoBehaviour
{
    public GameObject objUpArrow;
    public GameObject objDownArrow;
    public GameObject objLeftArrow;
    public GameObject objRightArrow;

    //private List<GameObject> Rendered = new List<GameObject>();
    private GameObject left;
    private GameObject right;
    private GameObject up;
    private GameObject down;

    public void ShowPossibleMoves(HexNode currentNode, Boolean movebyMapOffset)
    {
        //if (Rendered.Count > 0)
        //{
        //    Rendered.ForEach(r =>
        //    {
        //        LeanTween.scale(r, Vector3.zero, 0.15f)
        //            .setOnComplete(() => Destroy(r, 0.1f));
        //    });
        //    Rendered.Clear();
        //}

        //TZREBA PRZESUNAC O MAPE
        Vector3 mapOffset = HexGridGenerator.Instance.MapPosition;
        if (!movebyMapOffset)
            mapOffset = Vector3.zero;

        Single cornerXOffset = 0.75f;
        Single cornerYOffset = 0.4f;
        Single upDownOffsetY = 0.8f;
         

        if (currentNode.up)
        {
            if (!up)
            {
                GameObject arrow = Instantiate(objUpArrow, currentNode.up.transform.position + new Vector3(0f, upDownOffsetY) + mapOffset, Quaternion.identity);
                up = arrow;
            }
            else
            {
                LeanTween.move(up, currentNode.up.transform.position, 0.15f)
                    .setEaseOutSine();
            }
        }
        else
        {
            if (up)
                up.GetComponent<MovementTipArrow>().DestInWhile();
            up = null;
        }

        if (currentNode.down)
        {
            GameObject arrow = Instantiate(objDownArrow, currentNode.down.transform.position + new Vector3(0f, -upDownOffsetY) + mapOffset, Quaternion.identity);
            down = arrow;
            //Rendered.Add(arrow);
        }
        else
        {
            if (down)
                down.GetComponent<MovementTipArrow>().DestInWhile();
            down = null;
        }

        if (currentNode.upRight || currentNode.downRight)
        {
            Vector3 targetPos;
            if (currentNode.upRight)
            {
                targetPos = currentNode.upRight.transform.position +
                    new Vector3(cornerXOffset,
                    cornerYOffset)
                    + mapOffset;
            }
            else
            {
                targetPos = currentNode.downRight.transform.position +
                     new Vector3(cornerXOffset,
                     -cornerYOffset)
                     + mapOffset;
            }

            if (!right)
            {
                GameObject arrow;
                if (currentNode.upRight)
                {
                    arrow = Instantiate(objRightArrow, targetPos, Quaternion.identity);
                }
                else
                {
                    arrow = Instantiate(objRightArrow, targetPos, Quaternion.identity);
                }
                right = arrow;
            }
            else
            {
                LeanTween.move(right, targetPos, 0.15f)
                    .setEaseOutSine();
            }
            //Rendered.Add(arrow);
        }
        else
        {
            Destroy(right);
            right = null;
        }

        if (currentNode.upLeft || currentNode.downLeft)
        {
            Vector3 targetPos;
            if (currentNode.upLeft)
            {
                targetPos = currentNode.upLeft.transform.position +
                new Vector3(-cornerXOffset,
                +cornerYOffset)
                + mapOffset;
            }
            else
            {
                targetPos = currentNode.downLeft.transform.position +
                new Vector3(-cornerXOffset,
                -cornerYOffset)
                + mapOffset;
            }

            if (!left)
            {
                GameObject arrow;
                if (currentNode.upLeft)
                {
                    arrow = Instantiate(objLeftArrow, targetPos, Quaternion.identity);
                }
                else
                {
                    arrow = Instantiate(objLeftArrow, targetPos, Quaternion.identity);
                }
                left = arrow;
            }
            else
            {
                LeanTween.move(left, targetPos, 0.15f)
                    .setEaseOutSine();
            }
            //Rendered.Add(arrow);
        }
        else
        {
            Destroy(left);
            left = null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
