using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class CameraController : MonoBehaviour
{
    public Transform target;

    private void LateUpdate()
    {
        var curPos = transform.position;
        transform.position = CommonTools.zPlaneVector(target.position, curPos.z);
    }
}
