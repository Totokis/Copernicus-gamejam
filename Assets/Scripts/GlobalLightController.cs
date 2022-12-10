using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GlobalLightController : MonoBehaviour
{
    private Light2D _globalLight;
    private float _initIntensity = 0f;
    void Start()
    {
        _globalLight = gameObject.GetComponent<Light2D>();
    }
}
