using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMix : MonoBehaviour
{
    public AudioMixerSnapshot day;
    public AudioMixerSnapshot night;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sun"))
        {
            day.TransitionTo(2f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Sun"))
        {
            night.TransitionTo(2f);
        }
    }
}
