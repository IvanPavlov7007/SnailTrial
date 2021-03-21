using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TurnEvent),typeof(AudioSource))]
public class TurnSound : MonoBehaviour
{
    
    AudioSource aud;
    

    private void Start()
    {
        aud = GetComponent<AudioSource>();
        GetComponent<TurnEvent>().turn += PlaySound;
    }

    private void PlaySound()
    {
        aud.Play();
    }
}
