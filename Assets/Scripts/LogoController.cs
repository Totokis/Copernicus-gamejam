using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoController : MonoBehaviour
{
    [SerializeField] private GameObject _logoStars;
    [SerializeField] private GameObject _logoNodes;

    private SpriteRenderer _srLogoNodes;
    private bool _isLogoOnScreen = false;
    void Start()
    {
        _srLogoNodes = _logoNodes.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _funLogoShow();
    }

    private void _funLogoShow()
    {
        if (GlobalVariables.isLogoOnScreen && !_isLogoOnScreen)
        {
            _isLogoOnScreen = true;
            LeanTween.value(0f, 1f, 4f).setOnUpdate((value) =>
            {
                _srLogoNodes.color = _srLogoNodes.color + new Color(0f, 0f, 0f, value);
            }).setOnComplete(() => 
            {
                LeanTween.delayedCall(3f, () =>
                {
                    LeanTween.moveY(_logoNodes,7.5f, 4f);
                    LeanTween.moveY(_logoStars,7.5f, 4f).setOnComplete(() =>
                    {
                        Destroy(_logoNodes);
                        Destroy(_logoStars);
                        GlobalVariables.isLogoOnScreen = false;
                        //_isLogoOnScreen = false; <-- odkomentowac jak GameController nie bedzie sterowac
                    });
                }
                );
            }
            );
        }
    }
}
