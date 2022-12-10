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

    private List<GameObject> Rendered = new List<GameObject>();

    public void ShowPossibleMoves(HexNode currentNode, Boolean movebyMapOffset)
    {
        if(Rendered.Count > 0)
        {
            Rendered.ForEach(r => Destroy(r));
            Rendered.Clear();
        }

        //TZREBA PRZESUNAC O MAPE
        Vector3 mapOffset = HexGridGenerator.Instance.MapPosition;
        if (!movebyMapOffset)
            mapOffset = Vector3.zero;

        Single cornerXOffset = 0.75f;
        Single cornerYOffset = 0.4f;
        Single upDownOffsetY = 0.8f;


        if (currentNode.up)
        {
            GameObject arrow = Instantiate(objUpArrow, currentNode.up.transform.position + new Vector3(0f, upDownOffsetY) + mapOffset, Quaternion.identity);
            Rendered.Add(arrow);
        }

        if (currentNode.down)
        {
            GameObject arrow = Instantiate(objDownArrow, currentNode.down.transform.position + new Vector3(0f, -upDownOffsetY) + mapOffset, Quaternion.identity);
            Rendered.Add(arrow);
        }

        if (currentNode.upRight)
        {
            GameObject arrow = Instantiate(objRightArrow, currentNode.upRight.transform.position +
                new Vector3(cornerXOffset,
                cornerYOffset)
                + mapOffset, Quaternion.identity);
            Rendered.Add(arrow);
        }

        if (currentNode.downRight)
        {
            GameObject arrow = Instantiate(objRightArrow, currentNode.downRight.transform.position +
                new Vector3(cornerXOffset,
                -cornerYOffset)
                + mapOffset, Quaternion.identity);
            Rendered.Add(arrow);
        }

        if (currentNode.upLeft)
        {
            GameObject arrow = Instantiate(objLeftArrow, currentNode.upLeft.transform.position +
                new Vector3(-cornerXOffset,
                +cornerYOffset)
                + mapOffset, Quaternion.identity);
            Rendered.Add(arrow);
        }

        if (currentNode.downLeft)
        {
            GameObject arrow = Instantiate(objLeftArrow, currentNode.downLeft.transform.position +
                new Vector3(-cornerXOffset,
                -cornerYOffset)
                + mapOffset, Quaternion.identity);
            Rendered.Add(arrow);
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
