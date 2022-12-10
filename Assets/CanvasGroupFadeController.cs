using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGroupFadeController : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    private bool _listening =true;

    
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
    }

    private void OnEnable()
    {
        Activate();
    }
    public void Activate()
       {
           _listening = true;
           LeanTween.value(0f, 1f, 1f)
               .setOnUpdate(val =>
               {
                  canvasGroup.alpha = val;
               });
       }
   
   
       public void Desactivate()
       {
           _listening = false;
   
           LeanTween.value(0f, 1f, 1f)
               .setOnUpdate(val =>
               {
                   canvasGroup.alpha = val;
               }).setOnComplete(() =>
               { 
                   gameObject.SetActive(false);        
               });
   
           
       }
}
