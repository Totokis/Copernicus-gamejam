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
    [SerializeField] private bool _isKopperChatting = true;
    [SerializeField] private bool _isKopperSlidingToScreen = false;
    [SerializeField] private bool _isLogoShow = false;


    //private Camera _camera;
    private float _initIntensity = 0f;
    private float _initSize = 0f;
    private Vector3 _initPosition; 

    private bool _isFocusing;
    private bool _isChangeIntensity;

    void Start()
    {
        _isLevel = false;
        _initIntensity = _globalLight.intensity;
    }
    void Update()
    {
        GlobalVariables.isLevel = _isLevel;
        GlobalVariables.isKopperChatting = _isKopperChatting;
        GlobalVariables.isKopperSlidingToScreen = _isKopperSlidingToScreen;
        GlobalVariables.isLogoOnScreen = _isLogoShow;

        _funSetGroundBrightness();
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
}
