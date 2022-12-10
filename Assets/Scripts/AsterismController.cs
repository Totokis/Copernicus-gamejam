using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsterismController : MonoBehaviour
{
    public static AsterismController Instance;
    public Transform trInstructionPopup;

    public Transform trThoughtParent;

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

    public class AsterismPathInfo
    {
        public MoveDirection NextNode;
        public List<MoveDirection> FromNodeDirectionsNeeded;

        public AsterismPathInfo(MoveDirection nextNode, List<MoveDirection> fromNodeDirectionsNeeded)
        {
            NextNode = nextNode;
            FromNodeDirectionsNeeded = fromNodeDirectionsNeeded;
        }
    }

    private List<List<AsterismPathInfo>> asterisms = new List<List<AsterismPathInfo>>()
    {
        new List<AsterismPathInfo>()
        {
            new AsterismPathInfo(MoveDirection.Down, new List<MoveDirection>(){ MoveDirection.LeftDown, MoveDirection.Up}),
            new AsterismPathInfo(MoveDirection.LeftDown, new List<MoveDirection>(){ MoveDirection.RightUp, MoveDirection.LeftUp}),
            new AsterismPathInfo(MoveDirection.LeftUp, new List<MoveDirection>(){ MoveDirection.Up, MoveDirection.RightDown}),
            new AsterismPathInfo(MoveDirection.Up, new List<MoveDirection>(){ MoveDirection.Down, MoveDirection.RightUp}),
            new AsterismPathInfo(MoveDirection.RightUp, new List<MoveDirection>(){ MoveDirection.LeftDown, MoveDirection.RightDown }),
            new AsterismPathInfo(MoveDirection.RightDown, new List<MoveDirection>(){ MoveDirection.LeftUp, MoveDirection.Down})
        },

        new List<AsterismPathInfo>()
        {
            new AsterismPathInfo(MoveDirection.LeftUp, new List<MoveDirection>(){ MoveDirection.RightDown}),
            new AsterismPathInfo(MoveDirection.RightDown, new List<MoveDirection>(){ MoveDirection.LeftUp, MoveDirection.RightUp, MoveDirection.Down}),
            new AsterismPathInfo(MoveDirection.RightUp, new List<MoveDirection>(){ MoveDirection.LeftDown}),
            new AsterismPathInfo(MoveDirection.LeftDown, new List<MoveDirection>(){ MoveDirection.LeftUp, MoveDirection.RightUp, MoveDirection.Down}),
            new AsterismPathInfo(MoveDirection.Down, new List<MoveDirection>(){ MoveDirection.Up}),
        },

        new List<AsterismPathInfo>()
        {
            new AsterismPathInfo(MoveDirection.LeftUp, new List<MoveDirection>(){ MoveDirection.RightDown, MoveDirection.Up}),
            new AsterismPathInfo(MoveDirection.Up, new List<MoveDirection>(){ MoveDirection.Down}),
            new AsterismPathInfo(MoveDirection.Down, new List<MoveDirection>(){ MoveDirection.RightDown, MoveDirection.Up}),
            new AsterismPathInfo(MoveDirection.RightDown, new List<MoveDirection>(){ MoveDirection.LeftUp, MoveDirection.RightUp, MoveDirection.Down}),

            new AsterismPathInfo(MoveDirection.RightUp, new List<MoveDirection>(){ MoveDirection.LeftDown, MoveDirection.Up}),
            new AsterismPathInfo(MoveDirection.Up, new List<MoveDirection>(){ MoveDirection.Down}),
            new AsterismPathInfo(MoveDirection.Down, new List<MoveDirection>(){ MoveDirection.LeftDown, MoveDirection.Up}),
            new AsterismPathInfo(MoveDirection.LeftDown, new List<MoveDirection>(){ MoveDirection.LeftUp, MoveDirection.RightUp, MoveDirection.Down}),

            new AsterismPathInfo(MoveDirection.Down, new List<MoveDirection>(){ MoveDirection.Up, MoveDirection.RightDown}),
            new AsterismPathInfo(MoveDirection.RightDown, new List<MoveDirection>(){ MoveDirection.LeftUp}),
        },

        new List<AsterismPathInfo>()
        {
            new AsterismPathInfo(MoveDirection.LeftUp, new List<MoveDirection>(){ MoveDirection.RightDown, MoveDirection.Up}),
            new AsterismPathInfo(MoveDirection.Up, new List<MoveDirection>(){ MoveDirection.Down}),
            new AsterismPathInfo(MoveDirection.Down, new List<MoveDirection>(){ MoveDirection.RightDown, MoveDirection.Up}),
            new AsterismPathInfo(MoveDirection.RightDown, new List<MoveDirection>(){ MoveDirection.LeftUp, MoveDirection.RightUp, MoveDirection.Down}),
            //znowu pocz
            new AsterismPathInfo(MoveDirection.Down, new List<MoveDirection>(){ }),
            new AsterismPathInfo(MoveDirection.LeftDown, new List<MoveDirection>(){ }),
            new AsterismPathInfo(MoveDirection.Down, new List<MoveDirection>(){ }),
            new AsterismPathInfo(MoveDirection.LeftDown, new List<MoveDirection>(){ }),
            new AsterismPathInfo(MoveDirection.LeftUp, new List<MoveDirection>(){ }),
            new AsterismPathInfo(MoveDirection.LeftDown, new List<MoveDirection>(){ }),
            new AsterismPathInfo(MoveDirection.LeftUp, new List<MoveDirection>(){ }),

        },

    };

    public List<AsterismPathInfo> CurrentAsterism;

    void Start()
    {
        CurrentAsterism = asterisms[0];

        HexGridGenerator.Instance.SpawnMapAt(trInstructionPopup.position, trThoughtParent, true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

public enum MoveDirection
{
    Up,
    Down,
    LeftUp,
    LeftDown,
    RightDown,
    RightUp,
}
