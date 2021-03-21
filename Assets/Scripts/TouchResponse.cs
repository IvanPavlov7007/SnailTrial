using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
using Pixelplacement.TweenSystem;

public class TouchResponse : MonoBehaviour
{
    [SerializeField]
    Vector3 shakeAmount;
    [SerializeField]
    AnimationCurve scaleCurve;
    TweenBase tween;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        tween = Tween.LocalScale(transform, transform.localScale * 1.5f, 0.3f, 0f, scaleCurve);
        Tween.Position(transform, transform.position + shakeAmount, 0.3f, 0f, scaleCurve);   
    }
}
