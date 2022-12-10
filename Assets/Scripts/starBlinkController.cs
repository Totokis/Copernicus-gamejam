using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starBlinkController : MonoBehaviour
{
    private Animation _anim;
    void Start()
    {
        _anim = gameObject.GetComponent<Animation>();
        _funDelayPeriod();
    }

    private void _funDelayPeriod()
    {
        LeanTween.delayedCall(Random.Range(10f, 25f), _funPlayAnim);
    }

    private void _funPlayAnim()
    {
        _anim.Play();
        _funDelayPeriod();
    }
}
