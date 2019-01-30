using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour 
{
    [SerializeField]
    float _minTime;
    [SerializeField]
    float _maxTime;

    Light _light;

    public Material material;

    public AudioClip clip;

	// Use this for initialization
	void Start () 
    {
        _light = GetComponent<Light>();
        StartCoroutine(Flicker());
	}
	
	IEnumerator Flicker()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(_minTime,_maxTime));
            _light.enabled = !_light.enabled;

            if (material.IsKeywordEnabled("_EMISSION") == true)
            {
                material.DisableKeyword("_EMISSION");
            }
            else
            {
                material.EnableKeyword("_EMISSION");
            }

            // light switch sound
            GetComponent<AudioSource>().PlayOneShot(clip);
        }
    }
}
