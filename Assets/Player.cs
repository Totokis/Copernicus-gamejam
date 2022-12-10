using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

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
        Boolean moved = false;
        MoveDirection? movedFromDir = null;
        if (CurrentNode.up != null && Input.GetKeyDown(KeyCode.UpArrow))
        {
            CurrentNode.VisitsFromWhereDirections.Add(MoveDirection.Up);
            movedFromDir = MoveDirection.Down;
            CurrentNode = CurrentNode.up;
            moved = true;
        }
        else if (CurrentNode.down != null && Input.GetKeyDown(KeyCode.DownArrow))
        {
            CurrentNode.VisitsFromWhereDirections.Add(MoveDirection.Down);
            movedFromDir = MoveDirection.Up;
            CurrentNode = CurrentNode.down;
            moved = true;
        }
        else if ((CurrentNode.downLeft || CurrentNode.upLeft)
            && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (CurrentNode.downLeft)
            {
                CurrentNode.VisitsFromWhereDirections.Add(MoveDirection.LeftDown);
                CurrentNode = CurrentNode.downLeft;
                movedFromDir = MoveDirection.RightUp;
            }
            else if (CurrentNode.upLeft)
            {
                CurrentNode.VisitsFromWhereDirections.Add(MoveDirection.LeftUp);
                CurrentNode = CurrentNode.upLeft;
                movedFromDir = MoveDirection.RightDown;
            }
            moved = true;
        }
        else if ((CurrentNode.upRight || CurrentNode.downRight)
             && Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (CurrentNode.downRight)
            {
                CurrentNode.VisitsFromWhereDirections.Add(MoveDirection.RightDown);
                CurrentNode = CurrentNode.downRight;
                movedFromDir = MoveDirection.LeftUp;
            }
            else if (CurrentNode.upRight)
            {
                CurrentNode.VisitsFromWhereDirections.Add(MoveDirection.RightUp);
                CurrentNode = CurrentNode.upRight;
                movedFromDir = MoveDirection.LeftDown;
            }
            moved = true;
        }

        if (moved)
        {
            var desc = LeanTween.move(gameObject, CurrentNode.transform.position, 0.4f)
                .setEaseOutSine();
            //desc.ratioPassed = 1f;

            //transform.position = CurrentNode.transform.position;

            Boolean currentMoveWasInAsterismPath = false;
            Boolean currentMoveIsSuccess = false;
            HexNode currentNodeChecking = HexGridGenerator.Instance.StartNode;
            Int32 currentAsterismCOMPLETESToSuccess = AsterismController.Instance.CurrentAsterism.Count;
            Int32 currentAsterismCOMPLETEDDetected = 0;

            CurrentNode.VisitsFromWhereDirections.Add(movedFromDir.Value);

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

                Boolean allNeedsSatisfied = true;
                if (currentNodeChecking.VisitsFromWhereDirections.Count >= move.FromNodeDirectionsNeeded.Count)
                {
                    var needs = move.FromNodeDirectionsNeeded.Select(need => (need, false)).ToArray();

                    for (Int32 i = 0; i < needs.Count(); i++)
                    {
                        needs[i].Item2 = currentNodeChecking.VisitsFromWhereDirections.Contains(needs[i].need);
                    }
                    allNeedsSatisfied = needs.All(n => n.Item2);
                }
                else
                    allNeedsSatisfied = false;

                if (allNeedsSatisfied)
                    currentNodeChecking.IsCompleted = allNeedsSatisfied;

                if (currentNodeChecking.IsCompleted)
                    currentAsterismCOMPLETEDDetected++;

                if (CurrentNode.GetInstanceID() == currentNodeChecking.GetInstanceID())
                {
                    currentMoveWasInAsterismPath = true;
                }

            }

            if (currentAsterismCOMPLETEDDetected == currentAsterismCOMPLETESToSuccess)
                Debug.Log("Sukces");

            if (!currentMoveWasInAsterismPath)
                Debug.LogError("SKUCHA");
        }
    }
}
