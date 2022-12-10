using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ResetPlayer : MonoBehaviour
{
    private Boolean _listening = false;
    public CanvasGroup cg;

    private void Start()
    {
        _listening = true;
    }

    public void Activate()
    {
        _listening = true;
        LeanTween.value(0f, 1f, 1f)
            .setOnUpdate((Single val) =>
            {
                cg.alpha = val;
            });
    }


    public void Desactivate()
    {
        _listening = false;

        LeanTween.value(0f, 1f, 1f)
            .setOnUpdate((Single val) =>
            {
                cg.alpha = val;
            });
    }

    // Update is called once per frame
    void Update()
    {
        if (_listening && Input.GetKeyDown(KeyCode.R))
        {
            FindObjectOfType<Player>().ResetOnGrid();
        }
    }
}
