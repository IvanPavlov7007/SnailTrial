using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TurnEvent))]
public class TurnEmission : MonoBehaviour
{
    [SerializeField]
    GameObject particlesPrefab;
    void Start()
    {
        GetComponent<TurnEvent>().turn += PlayEmission;
    }

    // Update is called once per frame
    void PlayEmission()
    {
        Destroy(Instantiate(particlesPrefab, transform.position,
            transform.rotation * Quaternion.Euler(0, 0, -90f)), 1f);
    }
}
