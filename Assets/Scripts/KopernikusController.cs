using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KopernikusController : MonoBehaviour
{
    private Animation _anim;
    void Start()
    {
        _anim = gameObject.GetComponent<Animation>();
    }
    void Update()
    {
        if(!_anim.isPlaying)
            _anim.Play();
    }
}
