using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;


public class CollectableBehaviuor : MonoBehaviour
{

    Tween t = null;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Tween.LocalScale(transform, Vector3.zero, 0.5f, 0f, Tween.EaseIn);
            Destroy(gameObject,0.5f);
        }
    }
}
