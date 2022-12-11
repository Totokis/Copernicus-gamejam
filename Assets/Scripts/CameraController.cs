using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject _backgroundGround;

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
            _funMoveGroundbyCameraBottom();
            LeanTween.moveLocalY(gameObject, 1f, time).setEaseOutSine();
            LeanTween.value(_initSize, zoom, time).setOnUpdate((value) =>
            {
                _camera.orthographicSize = value;
            });
        }
        else if (!GlobalVariables.isLevel && _isFocusing)
        {
            _isFocusing = false;
            _funMoveGroundbyCameraUp();
            LeanTween.moveLocalY(gameObject, _initPosition.y, time).setEaseOutSine();
            LeanTween.value(zoom, _initSize, time).setOnUpdate((value) =>
            {
                _camera.orthographicSize = value;
            });
        }
    }

    private void _funMoveGroundbyCameraBottom()
    {
        LeanTween.moveY(_backgroundGround, -1.4f, 1.5f).setEaseOutSine();
    }

    private void _funMoveGroundbyCameraUp()
    {
        LeanTween.moveY(_backgroundGround, -0.7f, 1.5f).setEaseOutSine();
    }


}
