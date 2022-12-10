using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTipArrow : MonoBehaviour
{
    public void DestInWhile()
    {
        LeanTween.scale(gameObject, Vector3.zero, 0.25f)
            .setEaseOutSine();

        Destroy(gameObject, 1f);
    }
}
