using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class windowLightController : MonoBehaviour
{
    private Light2D _light;
    private float _initIntensity;

    private void Start()
    {
        _light = gameObject.GetComponent<Light2D>();
        _initIntensity = _light.intensity;
        _funRandomDelay();
    }
    private void _funRandomDelay()
    {
        LeanTween.delayedCall(Random.Range(0.1f, 8f), _funBlink);
    }
    private void _funBlink()
    {
        LeanTween.value(_initIntensity, Random.Range(_initIntensity * 1.1f, _initIntensity * 1.2f), 0.4f)
            .setLoopPingPong().setOnUpdate((value) =>
            {
                _light.intensity = value;
            });
    }
}
