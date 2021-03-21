using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Vector2 mousePos;
    private Camera cam;
    private Rigidbody2D rb;

    private bool isMoving;

    public float movingSpeed, minDistToPointer;
    [SerializeField]
    KeyCode movingKey;

    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(isMoving && Vector2.Distance(mousePos, rb.position) > minDistToPointer)
        {
            rb.position += distanceToPointer().normalized * movingSpeed * Time.fixedDeltaTime;
        }
    }

    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.right = distanceToPointer();

        isMoving = Input.GetMouseButton(1);
    }

    private Vector2 distanceToPointer()
    {
        return mousePos - rb.position;
    }
}
