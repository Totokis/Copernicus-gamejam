using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AsterismController : MonoBehaviour
{
    public static AsterismController Instance;
    public Transform trInstructionPopup;
    private int currentAsterismIndex = 0;
    public int CurrentAsteriumIndex() => currentAsterismIndex;
    public Transform trThoughtParent;

    //private MoveDirection up = MoveDirection.Up;
    //private MoveDirection down = MoveDirection.Down;
    //private MoveDirection ld = MoveDirection.LeftDown;
    //private MoveDirection lu = MoveDirection.LeftUp;
    //private MoveDirection ru = MoveDirection.RightUp;
    //private MoveDirection rd = MoveDirection.RightDown;

    private void Awake()
    {
        MoveDirection up = MoveDirection.Up;
        MoveDirection down = MoveDirection.Down;
        MoveDirection ld = MoveDirection.LeftDown;
        MoveDirection lu = MoveDirection.LeftUp;
        MoveDirection ru = MoveDirection.RightUp;
        MoveDirection rd = MoveDirection.RightDown;

        asterisms = new List<List<AsterismPathInfo>>()
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
            new AsterismPathInfo(MoveDirection.RightDown, new List<MoveDirection>(){ MoveDirection.LeftUp, MoveDirection.Down}),
            //znowu pocz
            new AsterismPathInfo(MoveDirection.Down, new List<MoveDirection>(){ up,ld}),
            new AsterismPathInfo(MoveDirection.LeftDown, new List<MoveDirection>(){ru, down }),
            new AsterismPathInfo(MoveDirection.Down, new List<MoveDirection>(){ ld, up}),
            new AsterismPathInfo(MoveDirection.LeftDown, new List<MoveDirection>(){ lu, ru}),
            new AsterismPathInfo(MoveDirection.LeftUp, new List<MoveDirection>(){ ld, rd}),
            new AsterismPathInfo(MoveDirection.LeftDown, new List<MoveDirection>(){ lu, ru}),
            new AsterismPathInfo(MoveDirection.LeftUp, new List<MoveDirection>(){ rd}),

        },

                new List<AsterismPathInfo>()//heart
        {
            new (MoveDirection.LeftUp   , new List<MoveDirection>(){ up,     rd}),
            new (MoveDirection.Up       , new List<MoveDirection>(){ down,   lu}),
            new (MoveDirection.LeftUp   , new List<MoveDirection>(){ rd, up, ld }),
            new (MoveDirection.LeftDown , new List<MoveDirection>(){ ru,     down}),
            new (MoveDirection.Down     , new List<MoveDirection>(){ up,     rd}),
            new (MoveDirection.RightDown, new List<MoveDirection>(){ lu,     down}),
            new (MoveDirection.Down     , new List<MoveDirection>(){ up,     rd}),
            new (MoveDirection.RightDown, new List<MoveDirection>(){ lu,     down}),
            new (MoveDirection.Down     , new List<MoveDirection>(){ up,     rd}),
            new (MoveDirection.RightDown, new List<MoveDirection>(){ lu,     down}),
            new (MoveDirection.Down     , new List<MoveDirection>(){ up,     rd}),
            new (MoveDirection.RightDown, new List<MoveDirection>(){ lu,     ru}),

            new (MoveDirection.RightUp, new List<MoveDirection>(){ ld, up}),
            new (MoveDirection.Up, new List<MoveDirection>(){ down, ru}),
            new (MoveDirection.RightUp, new List<MoveDirection>(){ ld, up}),
            new (MoveDirection.Up, new List<MoveDirection>(){ down, ru}),
            new (MoveDirection.RightUp, new List<MoveDirection>(){ ld, up}),
            new (MoveDirection.Up, new List<MoveDirection>(){ down, ru}),
            new (MoveDirection.RightUp, new List<MoveDirection>(){ ld, up}),
            new (MoveDirection.Up, new List<MoveDirection>(){ down, lu}),
            new (MoveDirection.LeftUp, new List<MoveDirection>(){ up, rd}),
            new (MoveDirection.Up, new List<MoveDirection>(){ down, lu}),
            new (MoveDirection.LeftUp, new List<MoveDirection>(){ rd, ld}),
            new (MoveDirection.LeftDown, new List<MoveDirection>(){ ru, down}),
            new (MoveDirection.Down, new List<MoveDirection>(){ up, ld}),
            new (MoveDirection.LeftDown, new List<MoveDirection>(){ ru, lu}),
            new (MoveDirection.LeftUp, new List<MoveDirection>(){ rd, up}),
            new (MoveDirection.Up, new List<MoveDirection>(){ down, lu}),
            new (MoveDirection.LeftUp, new List<MoveDirection>(){ ld, rd}),
            new (MoveDirection.LeftDown, new List<MoveDirection>(){ ru, down}),
            new (MoveDirection.Down, new List<MoveDirection>(){ ld, rd, up}),



        },

    };

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

    private List<List<AsterismPathInfo>> asterisms;

    public List<AsterismPathInfo> CurrentAsterism;

    public Vector3 thoughtsInitialPosition;
    void Start()
    {
        thoughtsInitialPosition = trThoughtParent.transform.position;
        CurrentAsterism = asterisms[currentAsterismIndex];
    }

    public void ClearThoughts()
    {
        foreach (Transform child in trThoughtParent)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void NextLevel()
    {
        CurrentAsterism = asterisms.ElementAtOrDefault(++currentAsterismIndex);
        if (CurrentAsterism == null)
        {
            print("Koniec");
            StateController.Instance.End();

        }
    }

    // Update is called once per frame
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
