using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light)), ExecuteInEditMode]
public class FlickeringLight : MonoBehaviour
{
	Light _light;
	public float refferenceIntensity;
	public float refferenceRange;
	public float flickInterval;

	private float _timer = 0;

	public float intensity { get { return _light.intensity; } set { _light.intensity = value; } }
	public float range { get { return _light.range; } set { _light.range = value; } }

	public float flickIntensity;
	public float flickRange;

	private float fi = 0;
	private float fr = 0;
	private float lastfi = 0;
	private float lastfr = 0;

	// Start is called before the first frame update
	void Start()
	{
		_light = this.GetComponent<Light>();
		//refferenceIntensity = _light.intensity;
		//-refferenceRange = _light.range;
		_timer = 0;
	}

	// Update is called once per frame
	void Update()
	{
		if(_timer <= 0)
        {
			if (flickIntensity != 0)
			{
				lastfi = fi;
				fi = refferenceIntensity + Random.Range(flickIntensity,-flickIntensity);
			}

			if (flickIntensity != 0)
			{
				lastfr = fr;
				fr = refferenceRange + Random.Range(flickRange, -flickRange);
			}
			
			_timer = flickInterval;
		}
		if(fi != lastfi) intensity = Mathf.Lerp(fi, lastfi, _timer / flickInterval);
		if(fr != lastfr) range = Mathf.Lerp(fr, lastfr, _timer / flickInterval);


		_timer -= Time.deltaTime;
	}
}
