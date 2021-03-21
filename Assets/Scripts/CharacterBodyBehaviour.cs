using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBodyBehaviour : MonoBehaviour
{
    public Sprite[] states;

    private const float fortyDegSin = 0.78539f;
    private SpriteRenderer renderer;
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        initalState = true;
        flipped = true;
    }

    bool initalState, flipped;



    void Update()
    {
        float curAngleRadians = getCurrentAngleRadians();
        Debug.Log(curAngleRadians);
        if(Mathf.Abs(Mathf.Sin(curAngleRadians)) > fortyDegSin)
        {
            if (initalState)
            {
                initalState = false;
                renderer.sprite = states[1];
            }
        }
        else if(!initalState)
        {
            initalState = true;
            renderer.sprite = states[0];
        }

        if (initalState && Mathf.Abs(curAngleRadians) > 1.58f)
        {
            if (!flipped)
            {
                flipped = true;
                renderer.flipY = true;
            }
        }
        else if (flipped)
        {
            flipped = false;
            renderer.flipY = false;
        }

    }

    private float getCurrentAngleRadians()
    {
        return Vector3.SignedAngle(Vector3.right, transform.right, Vector3.forward) * Mathf.PI * 0.00555f;
    }
}
