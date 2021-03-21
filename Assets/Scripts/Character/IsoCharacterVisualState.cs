using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IsoCharacterMovement))]
public class IsoCharacterVisualState : MonoBehaviour
{
    public Sprite[] states;

    private IsoCharacterMovement characterMovement;
    private bool initalState, flipped;
    private const float fortyDegSin = 0.78539f;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        characterMovement = GetComponent<IsoCharacterMovement>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        initalState = true;
        flipped = true;
    }

    void Update()
    {
        float curAngleRadians = getCurrentAngleRadians(characterMovement.transform.right);
        if (Mathf.Abs(Mathf.Sin(curAngleRadians)) > fortyDegSin)
        {
            if (initalState)
            {
                initalState = false;
                spriteRenderer.sprite = states[1];
            }
        }
        else if (!initalState)
        {
            initalState = true;
            spriteRenderer.sprite = states[0];
        }

        if (initalState && Mathf.Abs(curAngleRadians) > 1.58f)
        {
            if (!flipped)
            {
                flipped = true;
                spriteRenderer.flipY = true;
            }
        }
        else if (flipped)
        {
            flipped = false;
            spriteRenderer.flipY = false;
        }
    }

    private float getCurrentAngleRadians(Vector2 direction)
    {
        return Vector2.SignedAngle(Vector3.right, direction) * Mathf.PI * 0.00555f;
    }
}
