using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxModule : DNModuleBase
{

    [SerializeField]
    private Gradient skyColor;

    [SerializeField]
    private Gradient horizonColor; // lo que Unity llama GroundColor en el shader del skybox

    public override void UpdateModule(float intensity)
    {
        RenderSettings.skybox.SetColor("_SkyTint", skyColor.Evaluate(intensity));
        RenderSettings.skybox.SetColor("_GroundColor", horizonColor.Evaluate(intensity));
    }

  
}
