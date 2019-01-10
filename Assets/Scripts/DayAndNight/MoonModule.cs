using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonModule : DNModuleBase
{
    [SerializeField]
    private Light moon;

    [SerializeField]
    private Gradient moonColor;

    [SerializeField]
    private float baseIntensity; // how much the intensity of moonlight would change during the day

    public override void UpdateModule(float intensity)
    {
        moon.color = moonColor.Evaluate(1 - intensity); // plays the gradient backwards (opposite of the sun)
        moon.intensity = (1 - intensity) * baseIntensity + 0.05f; // gives the intensity un pequeño boost
                                                                  // but means the moon will be on durante el dia
    }

}
