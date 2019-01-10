using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DNModuleBase : MonoBehaviour
{
    protected DayNight dayAndNightControl;

    private void OnEnable()
    {
        dayAndNightControl = this.GetComponent<DayNight>();
        if (dayAndNightControl != null)
        {

            dayAndNightControl.AddModule(this);
        }
    }

    private void OnDisable()
    {
        if (dayAndNightControl != null)
        {
            dayAndNightControl.RemoveModule(this);
        }
    }

    public abstract void UpdateModule(float intensity);
}
