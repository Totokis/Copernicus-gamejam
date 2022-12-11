using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{

    [SerializeField] private GameObject title;
    [SerializeField] private float showTitleAfterTime = 5f;
    [SerializeField] private GameObject beginInstruction;
    [SerializeField] private float showBeginInstructionAfterTime = 7f;
    [SerializeField] private GameController gameController;
    [SerializeField] private GameObject timer;
    [SerializeField] private GameObject pressR;
    public static StateController Instance;

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
    private void Start()
    {
        timer.SetActive(false);
        pressR.SetActive(false);
        Invoke(nameof(ShowTitle), showTitleAfterTime);
        Invoke(nameof(ShowBeginInstruction), showBeginInstructionAfterTime);


    }
    private void ShowBeginInstruction()
    {

        beginInstruction.SetActive(true);
    }


    private void ShowTitle()
    {
        title.SetActive(true);
    }
    public void KeyPressed()
    {
        title.GetComponent<CanvasGroupFadeController>().Desactivate();
        beginInstruction.GetComponent<GetAnyKeyToContinue>().Desactivate();
        gameController.ActivateKopperComing();
    }
    
    public void ShowSpeechLines()
    {
        print("Mowie se o czyms");
        
        Invoke(nameof(KopperRotates), 2f);
        
        //TODO michau zrob
    }
    
    public void KopperRotates()
    {
        
        timer.SetActive(true);
        gameController.StartShowingThoughtMap();

    }
    
    public void CloudCompleted()
    {
        HexGridGenerator.Instance.SpawnMapAt(AsterismController.Instance.trInstructionPopup.position, AsterismController.Instance.trThoughtParent, true);
        Timer.Restart(5f,StartLevel);
    }
    
    public void StartLevel()
    {
        HexGridGenerator.Instance.SpawnMap();
        timer.SetActive(true);
        pressR.SetActive(true);
        Timer.Restart(10f);
        gameController.ActivateLevel();
        
    }
    
    public void FinishLevel()
    {
        AsterismController.Instance.NextLevel();
        StartCoroutine(DeleteCurrentBoard());

        AsterismController.Instance.trThoughtParent.position = AsterismController.Instance.thoughtsInitialPosition;
        //HexGridGenerator.Instance.SpawnMapAt(AsterismController.Instance.trInstructionPopup.position, AsterismController.Instance.trThoughtParent, true);

        gameController.DeactivateLevel();
        
    }
    
    private IEnumerator DeleteCurrentBoard()
    {
        yield return new WaitForSeconds(2f);
        Player player = FindObjectOfType<Player>();
        player.PlayerPossibleMovesRenderer.Resett();

        foreach (Transform child in HexGridGenerator.Instance.CurrentBoardTransform)
        {
            Destroy(child.gameObject);
        }
        
    }
    
    public void PlanshaCompleted()
    {
        AsterismPathRenderer.Instance.DrawWholePath();
        FinishLevel();
    }
   
}
