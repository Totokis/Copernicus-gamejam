using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class KopernikusController : MonoBehaviour
{
    [SerializeField] private GameObject _kopernicjumFront;

    private Animation _anim;
    private bool _isRotating = false;
    private bool _isSliding = false;
    //private SpriteRenderer _srKopernicjum;
    void Start()
    {
        _anim = gameObject.GetComponent<Animation>();
    }
    void Update()
    {
        if (!_anim.isPlaying)
            _anim.Play();
        _funRotate();
        _funSlidingToScreen();
    }

    private void _funSlidingToScreen()
    {
        if(GlobalVariables.isKopperSlidingToScreen && !_isSliding)
        {
            _isSliding = true;
        LeanTween.moveX(_kopernicjumFront, -4.46f, 2f).setEaseInOutSine();
        }
    }

    private void _funRotate()
    {
        if (!GlobalVariables.isKopperChatting && !_isRotating)
        {
            _isRotating = true;
            LeanTween.scaleX(_kopernicjumFront, 0, 0.4f)
                .setOnComplete(_funDestroyObjectAndScale);
        }
    }
    private void _funDestroyObjectAndScale()
    {
        Destroy(_kopernicjumFront);
        gameObject.transform.localPosition = new Vector3(-4.46f, -6.85f, -2.1f);
        LeanTween.scaleX(gameObject, 1.5f, 0.4f);
    }
}
