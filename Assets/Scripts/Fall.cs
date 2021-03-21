using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class Fall : MonoBehaviour
{
    [SerializeField]
    GameObject pathPrefab;
    Spline s;
    void Start()
    {
        s = Instantiate(pathPrefab, transform.position, Quaternion.identity).GetComponent<Spline>();
        Tween.Spline(s,transform,0f,1f,false,4f,0f,Tween.EaseInOut);
    }

    private void OnDestroy()
    {
        if (s != null && s.gameObject != null)
        Destroy(s.gameObject);
    }
}
