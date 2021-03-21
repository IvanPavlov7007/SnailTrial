using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngularVelocity
{
    private readonly IsoCharacterMovement characterMovement;
    private Vector2 velocity;
    private float angleChangeVelocity;
    private Vector2 lastDirection;
    private const float oneDegree = 0.002777f;
    private float relativeAngleChange;

    public float AngleChangeVelocity { get { return relativeAngleChange; } }

    public AngularVelocity(IsoCharacterMovement characterMovement, Vector2 initDirection)
    {
        this.characterMovement = characterMovement;
        lastDirection = initDirection;
        angleChangeVelocity = 0f;
        relativeAngleChange = 0f;
    }

    public void ChangeDirection(Vector2 newDirection, float angularToLinearCoef, float lerpValue, float minBasicVelocity, float deltaTime, float maxAdditionalAngleVelocity, float additionalVelocityGravity)
    {
        calculateRelativeAngleChange(newDirection, additionalVelocityGravity);
        angleChangeVelocity = Mathf.Max(0f, Mathf.Min(maxAdditionalAngleVelocity, Mathf.LerpAngle(angleChangeVelocity + Vector2.Angle(lastDirection, newDirection), 0f, additionalVelocityGravity)));
        velocity = Vector2.Lerp( velocity, newDirection.normalized * Mathf.Max(minBasicVelocity, angleChangeVelocity * oneDegree * angularToLinearCoef), lerpValue);
        lastDirection = newDirection;
        characterMovement.Velocity = velocity;
    }

    private void calculateRelativeAngleChange(Vector2 newDirection, float additionalVelocityGravity)
    {
        relativeAngleChange = Mathf.Lerp(Vector2.SignedAngle(lastDirection, newDirection) + relativeAngleChange,0f,additionalVelocityGravity);
    }
}
