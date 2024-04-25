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

    /// <summary>
    /// an update function for the angle/velocity calculation
    /// </summary>
    /// <param name="newDirection">new input direction</param> 
    /// <param name="angularToLinearCoef">change in angle -> change in velocity. Degree to m/s</param>
    /// <param name="lerpValue">smoothiness -> close to 0</param>
    /// <param name="minBasicVelocity">velocity with no angle change</param>
    /// <param name="deltaTime">pass it thorugh from Time.deltaTime</param>
    /// <param name="maxAdditionalAngleVelocity">cap for the fastest velocity</param>
    /// <param name="additionalVelocityGravity">how fast should @AngleChangeVelocity drop to 0</param>
    public void ChangeDirection(Vector2 newDirection, float angularToLinearCoef, float lerpValue, float minBasicVelocity, float deltaTime, float maxAdditionalAngleVelocity, float additionalVelocityGravity)
    {
        calculateRelativeAngleChange(newDirection, additionalVelocityGravity);
        //determine how much velocity to add
        angleChangeVelocity = Mathf.Max(0f, Mathf.Min(maxAdditionalAngleVelocity, Mathf.LerpAngle(angleChangeVelocity + Vector2.Angle(lastDirection, newDirection), 0f, additionalVelocityGravity)));
        velocity = Vector2.Lerp( velocity, newDirection.normalized * Mathf.Max(minBasicVelocity, angleChangeVelocity * oneDegree * angularToLinearCoef), lerpValue);
        lastDirection = newDirection;
        characterMovement.Velocity = velocity;
    }

    //How fast and in which direction angle changes. Little to no turn has values near 0
    private void calculateRelativeAngleChange(Vector2 newDirection, float additionalVelocityGravity)
    {
        relativeAngleChange = Mathf.Lerp(Vector2.SignedAngle(lastDirection, newDirection) + relativeAngleChange,0f,additionalVelocityGravity);
    }
}
