using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    [Header("Time")]
    [Tooltip("Day Length in Minutes")]
    [SerializeField]
    private float _targetDayLength = 0.5f; // Allow to set the length of day in minutes 
    public float targetDayLength            // for example 0.5f would be 30 seconds
    {
        get
        {
            return _targetDayLength;
        }
    }

    [SerializeField]
    [Range(0f, 1f)]
    private float _timeOfDay; // time of day goes from 0 to 1, is like a fraction of a day that passed
    public float timeOfDay
    {
        get
        {
            return _timeOfDay;
        }
    }

    [SerializeField]
    private int _dayNumber = 0; // numbers of day passed in the current year
    public int dayNumber
    {
        get
        {
            return _dayNumber;
        }
    }

    [SerializeField]
    private int _yearNumber = 0; // numbers of years since the program started
    public int yearNumber
    {
        get
        {
            return _yearNumber;
        }
    }

    private float _timeScale = 100f; // converter from real time (24h) to game time
                                     // the value is set at runtime and depend on targetDayLength

    [SerializeField]
    private int _yearLength = 100; // number of days in the year
    public int yearLength
    {
        get
        {
            return _yearLength;
        }
    }

    public bool pause = false; // pause the cycle without pausing the game

    [Header("Sun Light")]
    [SerializeField]
    private Transform dailyRotation; // reference the daily rotation gameObject
    [SerializeField]
    private Light sun;
    private float intensity; // measure of sun intensity (or brightness)
    [SerializeField]
    private float sunBaseIntensity = 1f; // minimum intensity (or intensity of sunrise and sunset)
    [SerializeField]
    private float sunVariation = 1.5f; // change in brightness from sunrise to noon and sunset
    [SerializeField]
    private Gradient sunColor; // control the colour of directional light during the day

    [Header("Seasonal Variables")]
    [SerializeField]
    private Transform sunSeasonalRotation;
    [SerializeField]
    [Range(-45f, 45f)]
    private float maxSeasonalTilt;

    [Header("Modules")] 
    private List<DNModuleBase> moduleList = new List<DNModuleBase>(); // reference to al the modules


    private void Update()
    {
        if (!pause)
        {
            UpdateTimeScale();
            UpdateTime();
        }

        AdjustSunRotation();
        SunIntensity();
        UpdateModule(); // will update modules each frame
    }

    private void UpdateTimeScale()
    {
        // for a day 24h long the timeScale would be 1
        // if a day last 1h timeScale would be 24 as time needs to past 24 times faster in the game than real life
        // to calculate timeScale, targetDayLength is divided by 60 (which is a fraction of an hour)
        // then 24 divided by this number give us the time scale
        _timeScale = 24 / (_targetDayLength / 60);
    }

    private void UpdateTime()
    {
        // Time.DeltaTime give us the length of the last frame in seconds
        // This number multiplied by timeScale and then divided by 86400 (which is the numbers of secods in a day)
        // give us the length of the last frame in game time
        // the time of the last frame is added to the time variable to get the current time of day

        _timeOfDay += Time.deltaTime * _timeScale / 86400; // second in a day

        //check if the day is complete
        if (_timeOfDay > 1) // new day
        {
            _dayNumber++;
            _timeOfDay -= 1;

            // check if year is over
            if (_dayNumber > _yearLength) // new year
            {
                _yearNumber++;
                _dayNumber = 0;
            }
        }
    }

    private void AdjustSunRotation() // rotate the daily rotation object
    {
        float sunAngle = _timeOfDay * 360; //calculated each frame
        dailyRotation.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, sunAngle));

        float seasonalAngle = -maxSeasonalTilt * Mathf.Cos(dayNumber / _yearLength * 2f * Mathf.PI);
        sunSeasonalRotation.localRotation = Quaternion.Euler(new Vector3(seasonalAngle, 0f, 0f));
    }

    private void SunIntensity() // calculate the projection and calculate the intensity of directional light
    {
        // equal to the Dot product of it's own forward direction
        // and the global downward direction
        intensity = Vector3.Dot(sun.transform.forward, Vector3.down);

        // Clamp the intensity between 0 and 1
        // this means the intensity is 0 rather than negative
        // when the directional light is pointing UP
        intensity = Mathf.Clamp01(intensity);

        // set the intensity of the light to the value of the projection
        sun.intensity = intensity * sunVariation + sunBaseIntensity;
    }

    private void AdjustSunColour() // adjust colour of directional light to a value from the gradient
                                   // this is done by using the intensity 
    {
        // gradients go from 0 to 1
        // 0 is when the sun is at the horizon or lower
        // 1 is when is directly overhead
        sun.color = sunColor.Evaluate(intensity);
    }

    public void AddModule (DNModuleBase module) // adds the input value to the module List
                                                // allows to register to the day and night cycle
    {
        moduleList.Add(module);
    }

    public void RemoveModule(DNModuleBase module) // removes the input value to the module List
                                                  // allows to register to the day and night cycle
    {
        moduleList.Remove(module);
    }

    // update each module based on current intensity
    private void UpdateModule()
    {
        foreach (DNModuleBase module in moduleList)
        {
            module.UpdateModule(intensity);
        }
    }
}
