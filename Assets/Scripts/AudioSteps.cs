using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSteps : MonoBehaviour
{
    public AudioSource audioSource;

    //Bank of different footsteps
    public AudioClip[] groundSteps;
    public AudioClip[] concreteSteps;

    public void Footsteps()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, -transform.up);
        int randomNumber = Random.Range(0, 3);
        if (Physics.Raycast(ray, out hit, 1.0f))
        {
            switch (hit.transform.tag)
            {
                case "ConcreteFloor":
                    audioSource.PlayOneShot(concreteSteps[randomNumber]);
                    break;

                case "GroundFloor":
                    audioSource.PlayOneShot(groundSteps[randomNumber]);
                    break;

                default:
                    audioSource.PlayOneShot(concreteSteps[randomNumber]);
                    break;
            }
        }
    }
}
