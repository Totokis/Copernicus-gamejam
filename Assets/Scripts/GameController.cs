using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GameController : MonoBehaviour
{
    [SerializeField] private Light2D _globalLight;
    //[SerializeField] private GameObject _cameraObject;
    [SerializeField] private bool _isLevel = false;

    //private Camera _camera;
    private float _initIntensity = 0f;
    private float _initSize = 0f;
    private Vector3 _initPosition;

    private bool _isFocusing;
    private bool _isChangeIntensity;

    void Start()
    {
        //_camera = _cameraObject.GetComponent<Camera>();
        _initIntensity = _globalLight.intensity;
        //_initSize = _camera.orthographicSize;
       // _initPosition = _camera.transform.position;
    }
    void Update()
    {
        GlobalVariables.isLevel = _isLevel;
        _funSetGroundBrightness();
       // _funFocusCameraOnLvl(4.7f, 1.5f);
    }

    private void _funSetGroundBrightness()
    {
        if (GlobalVariables.isLevel && !_isChangeIntensity)
        {
            _isChangeIntensity = true;
            LeanTween.value(_initIntensity, 0f, 1f).setOnUpdate((value) =>
            {
                _globalLight.intensity = value;
            });
        }
        else if (!GlobalVariables.isLevel && _isChangeIntensity)
        {
            _isChangeIntensity = false;
            LeanTween.value(0f, _initIntensity, 1f).setOnUpdate((value) =>
            {
                _globalLight.intensity = value;
            });
        }
    }

    //private void _funFocusCameraOnLvl(float zoom, float time)
    //{
    //    if (GlobalVariables.isLevel && !_isFocusing)
    //    {
    //        _isFocusing = true;
    //        LeanTween.moveLocalY(_cameraObject, 1f, time).setEaseOutSine();
    //        LeanTween.value(_initSize, zoom, time).setOnUpdate((value) =>
    //        {
    //            _camera.orthographicSize = value;
    //        });
    //    }
    //    else if (!GlobalVariables.isLevel && _isFocusing)
    //    {
    //        _isFocusing = false;
    //        LeanTween.moveLocalY(_cameraObject, _initPosition.y, time).setEaseOutSine();
    //        LeanTween.value(zoom, _initSize, time).setOnUpdate((value) =>
    //        {
    //            _camera.orthographicSize = value;
    //        });
    //    }
    //}
}
