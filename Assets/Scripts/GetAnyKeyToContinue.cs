using System;
using UnityEngine;

public class GetAnyKeyToContinue : MonoBehaviour
{
    private Boolean _listening = false;
    public CanvasGroup cg;

    private void Awake()
    {
        cg.alpha = 0f;
    }
    private void Start()
    {
        _listening = true;
    }

    private void OnEnable()
    {
        Activate();
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
            }).setOnComplete(() =>
            {
                gameObject.SetActive(false);        
            });

        
    }

    // Update is called once per frame
    void Update()
    {
        if (_listening && Input.anyKeyDown)
        {
            StateController.Instance.KeyPressed();
        }
    }
}