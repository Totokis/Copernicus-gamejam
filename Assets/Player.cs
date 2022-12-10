using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public HexNode CurrentNode;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentNode.up != null && Input.GetKeyDown(KeyCode.UpArrow))
        {
            CurrentNode = CurrentNode.up;
        }
        else if (CurrentNode.down != null && Input.GetKeyDown(KeyCode.DownArrow))
        {
            CurrentNode = CurrentNode.down;
        }
        else if ((CurrentNode.downLeft || CurrentNode.upLeft)
            && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            CurrentNode = CurrentNode.downLeft ? CurrentNode.downLeft : CurrentNode.upLeft;
        }
        else if ((CurrentNode.upRight || CurrentNode.downRight)
             && Input.GetKeyDown(KeyCode.RightArrow))
        {
            CurrentNode = CurrentNode.upRight ? CurrentNode.upRight : CurrentNode.downRight;
        }

        transform.position = CurrentNode.transform.position;
        CurrentNode.IsVisited = true;
    }
}
