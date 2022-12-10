using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class HexGridGenerator : MonoBehaviour
{
    public static HexGridGenerator Instance;

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

    public Vector2 StartNodePosition = new Vector2(4.5f, 2.2375f);
    private Vector2 THOUGHTStartNodePosition = new Vector2(12.5f, 8.2375f);
    public HexNode StartNode;

    public Transform trCurrentMapTransform;
    public int rows;
    public int columns;
    public GameObject hexSprite;
    public HexNode prefab;
    public GameObject leftUp;
    public HexNode[][] hexGrid;

    public Player objPlayerPrefab;

    public readonly float _upDownOffset = 0.3125f;
    public readonly float _zigZagNeighbourXOffset = 0.5f;
    private readonly float _zigZagOffset = 0.375f;
    private readonly float _halfOfSide = 0.1875f;
    public Vector3 MapPosition = new(-4, -2);

    public void SpawnMapAt(Vector3 mapPos, Transform mapParentLocal, Boolean isThought)
    {
        for (int i = 1; i <= columns; i++)
        {
            for (int j = 1; j <= rows; j++)
            {
                if (j % 2 == 0)
                {

                    if (j == rows && i == 1)
                    {
                        leftUp = Instantiate(hexSprite, new Vector3(i + 0.5f, j - (j * 0.315f), 0), Quaternion.identity, mapParentLocal);
                    }
                    else
                    {
                        Instantiate(hexSprite, new Vector3(i + 0.5f, j - (j * 0.315f), 0), Quaternion.identity, mapParentLocal);
                    }
                }
                else
                {
                    if (j == rows && i == 1)
                    {
                        leftUp = Instantiate(hexSprite, new Vector3(i, j - (j * 0.315f), 0), Quaternion.identity, mapParentLocal);
                    }
                    else
                    {
                        Instantiate(hexSprite, new Vector3(i, j - (j * 0.315f), 0), Quaternion.identity, mapParentLocal);
                    }

                }

            }
        }

        var currentLeftPosition = (Vector2)leftUp.transform.position + new Vector2(-0.5f, _halfOfSide);
        var isValley = true;
        var isBeginWithValley = true;

        for (int y = 0; y < rows + 1; y++)
        {
            float rowStartY = currentLeftPosition.y;
            for (int x = 0; x < columns * 2 + 1; x++)
            {
                Instantiate(prefab, currentLeftPosition, Quaternion.identity, mapParentLocal);
                currentLeftPosition += new Vector2(0.5f, isValley ? _upDownOffset : -_upDownOffset);
                isValley = !isValley;
            }

            if (isBeginWithValley)
            {
                currentLeftPosition.y = rowStartY - 2 * _halfOfSide;
            }
            else
            {
                currentLeftPosition.y = rowStartY - 1f;
            }

            isBeginWithValley = !isBeginWithValley;
            currentLeftPosition.x = leftUp.transform.position.x - 0.5f;
        }


        var allNodes = FindObjectsOfType<HexNode>();
        foreach (var hexNode in allNodes)
        {
            var up = allNodes.FirstOrDefault(n => n.transform.position.x == hexNode.transform.position.x && n.transform.position.y == hexNode.transform.position.y + 2 * _halfOfSide);
            if (up)
            {
                hexNode.up = up;
            }

            var down = allNodes.FirstOrDefault(n => n.transform.position.x == hexNode.transform.position.x && n.transform.position.y == hexNode.transform.position.y - 2 * _halfOfSide);
            if (down)
            {
                hexNode.down = down;
            }

            var upRight = allNodes.FirstOrDefault(n => n.transform.position.x == hexNode.transform.position.x + 0.5f && n.transform.position.y == hexNode.transform.position.y + _upDownOffset);
            if (upRight)
            {
                hexNode.upRight = upRight;
            }

            var upLeft = allNodes.FirstOrDefault(n => n.transform.position.x == hexNode.transform.position.x - 0.5f && n.transform.position.y == hexNode.transform.position.y + _upDownOffset);
            if (upLeft)
            {
                hexNode.upLeft = upLeft;
            }

            var downLeft = allNodes.FirstOrDefault(n => n.transform.position.x == hexNode.transform.position.x - 0.5f && n.transform.position.y == hexNode.transform.position.y - _upDownOffset);
            if (downLeft)
            {
                hexNode.downLeft = downLeft;
            }

            var downRight = allNodes.FirstOrDefault(n => n.transform.position.x == hexNode.transform.position.x + 0.5f && n.transform.position.y == hexNode.transform.position.y - _upDownOffset);
            if (downRight)
            {
                hexNode.downRight = downRight;
            }
        }

        if (!isThought)
        {
            StartNode = allNodes.First(n => n.transform.position.x == StartNodePosition.x && n.transform.position.y == StartNodePosition.y);
            //StartNode.IsVisited = true;

            mapParentLocal.localScale = new Vector3(1.8f, 1.8f, 1.8f);
            GameObject player = Instantiate(objPlayerPrefab, mapParentLocal).gameObject;

            player.GetComponent<Player>().InjectStartNode(StartNode);
            player.transform.position = StartNode.transform.position;
            trCurrentMapTransform.transform.position = mapPos;
        }
        else
        {
            //pozycja tego co przyszlow  funkcji to offset 
            var startNode = allNodes.First(n => n.transform.localPosition.x == THOUGHTStartNodePosition.x
                && n.transform.localPosition.y == THOUGHTStartNodePosition.y);

            mapParentLocal.transform.position += mapParentLocal.transform.position;

            MarkThoughtPaths(startNode);
        }


    }

    private void MarkThoughtPaths(HexNode startNode)
    {
        HexNode currentNodeChecking = startNode;
        currentNodeChecking.transform.localScale = Vector3.one;
        Vector3 lastNodePos = currentNodeChecking.transform.position;

        foreach (var move in AsterismController.Instance.CurrentAsterism)
        {
            switch (move.NextNode)
            {
                case MoveDirection.Up:
                    currentNodeChecking = currentNodeChecking.up;
                    break;
                case MoveDirection.Down:
                    currentNodeChecking = currentNodeChecking.down;
                    break;
                case MoveDirection.LeftDown:
                    currentNodeChecking = currentNodeChecking.downLeft;
                    break;
                case MoveDirection.LeftUp:
                    currentNodeChecking = currentNodeChecking.upLeft;
                    break;
                case MoveDirection.RightUp:
                    currentNodeChecking = currentNodeChecking.upRight;
                    break;
                case MoveDirection.RightDown:
                    currentNodeChecking = currentNodeChecking.downRight;
                    break;
            }

            AsterismPathRenderer.Instance.DrawLineFromPointToPoint(lastNodePos, currentNodeChecking.transform.position);
            lastNodePos = currentNodeChecking.transform.position;

            currentNodeChecking.transform.localScale = Vector3.one;

        }
    }

    private void Start()
    {
        SpawnMapAt(MapPosition, trCurrentMapTransform, false);

    }
}

