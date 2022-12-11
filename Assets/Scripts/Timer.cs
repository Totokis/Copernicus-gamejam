using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI txtWhole;
    public TextMeshProUGUI txtDecimal;

    public CanvasGroup cgThis;

    public Int32 monospace = 20;
    // Start is called before the first frame update

    void SetText(Single time)
    {
        String timeText = time.ToString("N2");
        String[] timeTexts = timeText.Split(",");
        if (timeTexts.Length == 1)
            timeTexts = timeText.Split(".");

        txtWhole.text = $"<mspace=mspace={monospace}>{timeTexts[0]}</mspace>";
        txtDecimal.text = $"<mspace=mspace={monospace}>{timeTexts[1]}</mspace>";
    }

    void Start()
    {
        _time = 0f;
        cgThis.alpha = 1f;
    }

    private Action onCompleted;

    public static void Restart(Single countFrom, Action onCompleted = null)
    {
        Timer timer = FindObjectOfType<Timer>();
        timer.onCompleted = onCompleted;
        timer.RestarttWithCountingFrom(countFrom);
        
    }

    private void RestarttWithCountingFrom(Single val)
    {
        LeanTween.value(0f, 1f, 0.5f)
            .setOnUpdate((Single val) =>
            {
                cgThis.alpha = val;
            });

        TimeIsUp = false;
        _time = val;
        _going = true;

    }

    public Single GetTimeLocal() => _time;
    Single _time;
    private bool _going;
    public static Boolean TimeIsUp = false;

    private void FixedUpdate()
    {
        if (_going)
        {
            if (_time <= 0)
            {
                TimeIsUp = true;
                _going = false;
                onCompleted?.Invoke();
            }

            SetText(_time);
            _time -= Time.deltaTime;
            _time = Mathf.Clamp(_time, 0, Single.MaxValue);

        }
    }

    internal static Single GetTimeAndStop()
    {
        Timer timer = FindObjectOfType<Timer>();
        Single wasTime = timer.GetTimeLocal();
        timer._going = false;

        timer._time = 0f;
        timer.cgThis.alpha = 0f;

        return wasTime;
    }
}
