using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingStarController : MonoBehaviour
{
    private Animation _anim;
    void Start()
    {
        _anim = gameObject.GetComponent<Animation>();
        _funRandomPeriod(); 
    }

    private void _funRandomPeriod()
    {
        LeanTween.delayedCall(Random.Range(20f, 35f), _funPlayAnim);
    }

    private void _funPlayAnim()
    {
        _anim.Play();
        _funRandomPeriod();
    }
}
