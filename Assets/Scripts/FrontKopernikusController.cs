using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontKopernikusController : MonoBehaviour
{
    private Animation _anim;
    void Start()
    {
        _anim = gameObject.GetComponent<Animation>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (!_anim.isPlaying)
            _anim.Play();
    }
}
