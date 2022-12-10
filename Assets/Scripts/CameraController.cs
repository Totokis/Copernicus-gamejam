using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera _camera;
    private float _initSize;
    private Vector3 _initPosition;

    private bool _isFocusing;
    void Start()
    {
        _camera = gameObject.GetComponent<Camera>();
        _initSize = _camera.orthographicSize;
        _initPosition = gameObject.transform.position;
    }
    private void Update()
    {
        _funFocusCameraOnLvl(4.7f, 1.5f);
        
    }

    private void _funFocusCameraOnLvl(float zoom, float time)
    {
        if (GlobalVariables.isLevel && !_isFocusing)
        {
            _isFocusing = true;
            LeanTween.moveLocalY(gameObject, 1f, time).setEaseOutSine();
            LeanTween.value(_initSize, zoom, time).setOnUpdate((value) =>
            {
                _camera.orthographicSize = value;
            });
        }
        else if (!GlobalVariables.isLevel && _isFocusing)
        {
            _isFocusing = false;
            LeanTween.moveLocalY(gameObject, _initPosition.y, time).setEaseOutSine();
            LeanTween.value(zoom, _initSize, time).setOnUpdate((value) =>
            {
                _camera.orthographicSize = value;
            });
        }
    }


}
