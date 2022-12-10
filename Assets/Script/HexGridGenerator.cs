using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class HexGridGenerator : MonoBehaviour
{
    public Vector2 StartNodePosition = new Vector2(4.5f, 2.2375f);
    public HexNode StartNode;

    public GameObject mapParent;
    public int rows;
    public int columns;
    public GameObject hexSprite;
    public HexNode prefab;
    public GameObject leftUp;
    public HexNode[][] hexGrid;

    public Player objPlayerPrefab;

    private readonly float _upDownOffset = 0.3125f;
    private readonly float _zigZagNeighbourXOffset = 0.5f;
    private readonly float _zigZagOffset = 0.375f;
    private readonly float _halfOfSide = 0.1875f;
    [SerializeField] private Vector3 mapPosition = new(-4, -2);

    private void Start()
    {
        
        for (int i = 1; i <= columns; i++)
        {
            for (int j = 1; j <= rows; j++)
            {
                if (j % 2 == 0)
                {
                        
                    if (j == rows&&i==1)
                    {
                        leftUp =Instantiate(hexSprite, new Vector3(i+0.5f, j-(j*0.315f), 0),Quaternion.identity,mapParent.transform); 
                    }
                    else
                    {
                        Instantiate(hexSprite, new Vector3(i+0.5f, j-(j*0.315f), 0),Quaternion.identity,mapParent.transform);
                    }
                }
                else
                {
                    if (j == rows&&i==1)
                    {
                        leftUp = Instantiate(hexSprite, new Vector3(i, j-(j*0.315f), 0),Quaternion.identity,mapParent.transform);
                    }
                    else
                    {
                        Instantiate(hexSprite, new Vector3(i, j-(j*0.315f), 0),Quaternion.identity,mapParent.transform);    
                    }
                    
                }
                 
            }
        }

        var currentLeftPosition = (Vector2)leftUp.transform.position + new Vector2(-0.5f,_halfOfSide);
        var isValley = true;
        var isBeginWithValley = true;
        
        for (int y = 0; y < rows+1; y++)
        {
            float rowStartY = currentLeftPosition.y;
            for (int x = 0; x < columns * 2 + 1; x++)
            {
                Instantiate(prefab, currentLeftPosition,Quaternion.identity,mapParent.transform);
                currentLeftPosition += new Vector2(0.5f, isValley ? _upDownOffset : -_upDownOffset);
                isValley = !isValley;
            }

            if (isBeginWithValley)
            {
                currentLeftPosition.y =rowStartY - 2*_halfOfSide;    
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

        StartNode = allNodes.First(n => n.transform.position.x == StartNodePosition.x && n.transform.position.y == StartNodePosition.y);
        GameObject player = Instantiate(objPlayerPrefab, mapParent.transform).gameObject;
        player.transform.position = StartNodePosition;
        player.GetComponent<Player>().CurrentNode = StartNode;

        mapParent.transform.position = mapPosition;


    }
}

