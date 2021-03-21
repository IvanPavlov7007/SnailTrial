using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IsoCharacterMovement))]
public class MouseDirectionController : MonoBehaviour
{
    private IsoCharacterMovement characterMovement;
    private Camera cam;
    [SerializeField, Range(0f,10000f)]
    private float angularToLinearCoef = 1f;
    [SerializeField, Range(0.01f,1f)]
    private float lerpValue = 0.1f;
    [SerializeField, Range(0f, 50f)]
    private float minVelocity = 0.1f;
    [SerializeField, Range(0f, 1000f)]
    private float maxAdditionalAngleVelocity;
    [SerializeField, Range(0f, 0.15f)]
    private float additionalVelocityGravity;
    AngularVelocity angularVelocity;

    public AngularVelocity getAngularVelocity()
    {
        return angularVelocity;
    }

    private void Awake()
    {
        
    }

    void Start()
    {
        characterMovement = GetComponent<IsoCharacterMovement>();
        cam = Camera.main;
        angularVelocity = new AngularVelocity(characterMovement, ScreenMouseDirection());
        
    }

    void Update()
    {
        angularVelocity.ChangeDirection(ScreenMouseDirection(),angularToLinearCoef,lerpValue, minVelocity,Time.deltaTime,maxAdditionalAngleVelocity, additionalVelocityGravity);
    }

    Vector2 center = new Vector2(Screen.width / 2, Screen.height / 2);

    private Vector2 ScreenMouseDirection()
    {
        return (Vector2)Input.mousePosition - center;
    }
}
