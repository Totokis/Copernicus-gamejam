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
        Invoke(nameof(ShowTitle),showTitleAfterTime);
        Invoke(nameof(ShowBeginInstruction),showBeginInstructionAfterTime);

        
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
    public void KopperFinishedSliding()
    {
        gameController.DeactivateKopperChatting();
    }
    public void ShowSpeechLines()
    {
        Invoke(nameof(KopperFinishedSliding),2f);
        //TODO michau zrob
    }

    public void FinishLevel()
    {
        gameController.DeactivateLevel();
    }
    
    public void StartLevel()
    {
        gameController.ActivateLevel();
    }
}
