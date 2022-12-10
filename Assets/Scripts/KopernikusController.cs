using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class KopernikusController : MonoBehaviour
{
    [SerializeField] private GameObject _kopernicjumFront;

    private Animation _anim;
    private bool _isRotating = false;
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
        gameObject.transform.localPosition = new Vector3(-4.26f, -5.38f, -2.1f);
        LeanTween.scaleX(gameObject, 1.5f, 0.4f);
    }
}
