using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip hoverSound;
    public AudioClip clickSound;

    public void HoverFX()
    {
        audioSource.PlayOneShot(hoverSound);
    }

    public void ClickFX()
    {
        audioSource.PlayOneShot(clickSound);
    }
}
