using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class Trail : MonoBehaviour
{
    Transform snail;
    Spline spline;
    SplineAnchor[] splineAnchors;
    public float maxTwoPointsDist;
    // Start is called before the first frame update
    void Start()
    {
        snail = GameObject.FindWithTag("Player").transform;
        spline = GetComponent<Spline>();
        splineAnchors = spline.Anchors;
    }

    // Update is called once per frame
    void Update()
    {
        int anchorsCount = splineAnchors.Length;
        splineAnchors[anchorsCount - 1].transform.position = snail.position;
        for (int i = anchorsCount - 2; i >= 0; i--)
        {
            Vector3 dir = splineAnchors[i + 1].transform.position - splineAnchors[i].transform.position;
            if ( dir.magnitude > maxTwoPointsDist)
            {
                splineAnchors[i].transform.Translate((dir.magnitude - maxTwoPointsDist) * dir.normalized);
            }

        }
    }
}
