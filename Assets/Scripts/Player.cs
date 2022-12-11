using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public HexNode CurrentNode;
    private HexNode _lastStaringNode;

    public AudioSource BADSound;

    public PlayerPossibleMovesRenderer PlayerPossibleMovesRenderer;
    // Start is called before the first frame update
    void Awake()
    {
        PlayerPossibleMovesRenderer = GetComponent<PlayerPossibleMovesRenderer>();
    }

    private void Start()
    {
        LeanTween.scale(gameObject, new Vector3(1.2f, 1.2f), 0.5f)
            .setLoopPingPong();


    }

    public void InjectStartNode(HexNode startNode)
    {
        _lastStaringNode = startNode;
        CurrentNode = startNode;
        PlayerPossibleMovesRenderer.ShowPossibleMoves(startNode, true);
    }

    Single RotationSpeed = 2f;
    void Update()
    {
        transform.localEulerAngles = new Vector3(0f, 0f, transform.localEulerAngles.z + RotationSpeed);
        Boolean moved = false;
        MoveDirection? movedFromDir = null;

        var nodeBefore = CurrentNode;
        Vector2 fromLinePos = CurrentNode.transform.position;

        if (_banmove)
            return;

        if (CurrentNode.up != null && Input.GetKeyDown(KeyCode.UpArrow))
        {
            CurrentNode.AddVisit(MoveDirection.Up);
            movedFromDir = MoveDirection.Down;
            CurrentNode = CurrentNode.up;
            moved = true;
        }
        else if (CurrentNode.down != null && Input.GetKeyDown(KeyCode.DownArrow))
        {
            CurrentNode.AddVisit(MoveDirection.Down);
            movedFromDir = MoveDirection.Up;
            CurrentNode = CurrentNode.down;
            moved = true;
        }
        else if ((CurrentNode.downLeft || CurrentNode.upLeft)
            && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (CurrentNode.downLeft)
            {
                CurrentNode.AddVisit(MoveDirection.LeftDown);
                CurrentNode = CurrentNode.downLeft;
                movedFromDir = MoveDirection.RightUp;
            }
            else if (CurrentNode.upLeft)
            {
                CurrentNode.AddVisit(MoveDirection.LeftUp);
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
                CurrentNode.AddVisit(MoveDirection.RightDown);
                CurrentNode = CurrentNode.downRight;
                movedFromDir = MoveDirection.LeftUp;
            }
            else if (CurrentNode.upRight)
            {
                CurrentNode.AddVisit(MoveDirection.RightUp);
                CurrentNode = CurrentNode.upRight;
                movedFromDir = MoveDirection.LeftDown;
            }
            moved = true;
        }

        if (moved)
        {
            (Game.Core.Rendering.MultiLineRenderer2D lr, AsterismSinglePathRenderer ar) lineJustDrawn = AsterismPathRenderer.Instance.DrawLineFromPointToPoint(fromLinePos, CurrentNode.transform.position,null,true);

            var movingToNextNodeTween = LeanTween.move(gameObject, CurrentNode.transform.position, 0.4f)
                .setEaseOutSine()
                .pause();
            movingToNextNodeTween.resume();

            //desc.ratioPassed = 1f;

            //transform.position = CurrentNode.transform.position;

            Boolean currentMoveWasInAsterismPath = false;
            Boolean currentMoveIsSuccess = false;
            HexNode currentNodeChecking = HexGridGenerator.Instance.StartNode;
            Int32 currentAsterismCOMPLETESToSuccess = AsterismController.Instance.CurrentAsterism.Count;
            Int32 currentAsterismCOMPLETEDDetected = 0;

            CurrentNode.AddVisit(movedFromDir.Value);

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
                    currentNodeChecking.SetAsCompleted();

                if (currentNodeChecking.IsCompleted)
                    currentAsterismCOMPLETEDDetected++;

                if (CurrentNode.GetInstanceID() == currentNodeChecking.GetInstanceID())
                {
                    currentMoveWasInAsterismPath = true;
                }

            }
            

            if (currentAsterismCOMPLETEDDetected == currentAsterismCOMPLETESToSuccess)
            {
                StateController.Instance.PlanshaCompleted();
                Debug.Log("Success");
            }

            if (!currentMoveWasInAsterismPath)
            {
                StartCoroutine(CancelMove(movingToNextNodeTween, nodeBefore, lineJustDrawn));

                Debug.LogError("SKUCHA");Debug.Log("Krrk");//Do
            }
            else
            {
                PlayerPossibleMovesRenderer.ShowPossibleMoves(CurrentNode, false);
            }
        }
    }

    private Boolean _banmove = false;
    private IEnumerator CancelMove(LTDescr movingTween,HexNode nodeBefore,
        (Game.Core.Rendering.MultiLineRenderer2D lr, AsterismSinglePathRenderer ar) justDrawn)
    {
        BADSound.Play();


        LeanTween.value(4.7f, 5f, 0.13f)
                .setLoopPingPong(1)
                .setEaseInElastic()
                //.setEaseShake()
                .setOnUpdate((Single val) =>
                {
                    Camera.main.orthographicSize = val;
                    //Camera.main.
                });

        _banmove = true;
        StartCoroutine(UnbanMove());
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.2f);
        LeanTween.cancel(movingTween.id);
        CurrentNode.ResetProps();
        CurrentNode = nodeBefore;
        transform.position = CurrentNode.transform.position;
        AsterismPathRenderer.Instance.RemoveFromBadMove(justDrawn.ar);
        Destroy(justDrawn.ar.gameObject);
    }

    private IEnumerator UnbanMove()
    {
        yield return new WaitForSeconds(1f);
        GetComponent<SpriteRenderer>().color = Color.white;
        _banmove = false;
    }

    internal void ResetOnGrid()
    {
        //siemsdfa
        transform.position = _lastStaringNode.transform.position;
        CurrentNode = _lastStaringNode;

        var allHexes = FindObjectsOfType<HexNode>();
        foreach (var hex in allHexes)
        {
            hex.ResetProps();
        }

        var all = FindObjectsOfType<AsterismSinglePathRenderer>();
        foreach (var line in all)
        {
            Destroy(line.gameObject);
        }

        AsterismPathRenderer.Instance.Resett();

        transform.position = _lastStaringNode.transform.position;
        PlayerPossibleMovesRenderer.Resett();
        PlayerPossibleMovesRenderer.ShowPossibleMoves(CurrentNode, false);
    }
}
