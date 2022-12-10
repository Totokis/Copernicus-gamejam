using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    
    [SerializeField] private GameObject title;
    [SerializeField] private float showTitleAfterTime = 5f;
    [SerializeField] private GameObject showBeginInstruction;
    [SerializeField] private float showBeginInstructionAfterTime = 7f;
    private void Start()
    {
        Invoke(nameof(ShowTitle),showTitleAfterTime);
        Invoke(nameof(ShowBeginInstruction),showBeginInstructionAfterTime);

        
    }
    private void ShowBeginInstruction()
    {

        showBeginInstruction.SetActive(true);
    }


    private void ShowTitle()
    {
        title.SetActive(true);
    }
}
