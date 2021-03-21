using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class IsoCharacterMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 velocity;
    private const float velocityTolerance = 0.01f;

    public Vector2 Velocity
    {
        get { return velocity; }
        set { velocity = value; }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    void FixedUpdate()
    {
        rb.velocity = velocity;
        if(velocity.magnitude > velocityTolerance)
            rb.rotation = Vector2.SignedAngle(Vector2.right, velocity);
    }

    
}
