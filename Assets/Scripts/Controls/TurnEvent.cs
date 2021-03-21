using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MouseDirectionController))]
public class TurnEvent : MonoBehaviour
{
    AngularVelocity angularVelocity;
    [SerializeField, Range(0f, 50f)]
    private float minAngularVel;

    public delegate void onTurn();
    public event onTurn turn;

    bool played = false;
    bool directionLeft = false;

    void Update()
    {
        if (angularVelocity == null)
        {
            angularVelocity = GetComponent<MouseDirectionController>().getAngularVelocity();
        }

        float angleChange = angularVelocity.AngleChangeVelocity;

        if (angleChange > 0 && !directionLeft || angleChange < 0 && directionLeft)
        {
            directionLeft = !directionLeft;
            played = false;
        }

        if (Mathf.Abs(angleChange) > minAngularVel)
        {
            if (!played)
            {
                played = true;
                if (turn != null)
                    turn();
            }
        }
    }
}
