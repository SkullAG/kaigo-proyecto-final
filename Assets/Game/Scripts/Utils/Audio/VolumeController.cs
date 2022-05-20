using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using NaughtyAttributes;

public class VolumeController : MonoBehaviour
{
	[SerializeField]
	private Slider _bgmSlider;

	[SerializeField]
	private string _bgmName;

	[Space(15)]

	[SerializeField]
	private Slider _masterSlider;

	[SerializeField]
	private string _masterName;

	[Space(15)]

	[SerializeField]
	private Slider _sfxSlider;

	[SerializeField]
	private string _sfxName;

	[Space(15)]

	[SerializeField]
	private AudioMixer _audioMixer;

	[SerializeField, MinMaxSlider(-80f, 20f)]
	private Vector2 dBslider;

    private void OnEnable()
    {
		_bgmSlider.onValueChanged.AddListener(UpdateBGM);
		_masterSlider.onValueChanged.AddListener(UpdateMaster);
		_sfxSlider.onValueChanged.AddListener(UpdateSXF);

		float sfx;
		float mas;
		float bgm;

		_audioMixer.GetFloat(_bgmName, out bgm);
		_audioMixer.GetFloat(_masterName, out mas);
		_audioMixer.GetFloat(_sfxName, out sfx);

		_bgmSlider.value = (bgm - dBslider.x) / (dBslider.y - dBslider.x);
		_masterSlider.value = (mas - dBslider.x) / (dBslider.y - dBslider.x);
		_sfxSlider.value = (sfx - dBslider.x) / (dBslider.y - dBslider.x);

	}

    public void UpdateBGM(float value) {

		_audioMixer.SetFloat(_bgmName, ValueToVolume(value));

	}

	public void UpdateMaster(float value) {

		_audioMixer.SetFloat(_masterName, ValueToVolume(value));

	}

	public void UpdateSXF(float value) {

		_audioMixer.SetFloat(_sfxName, ValueToVolume(value));

	}

	private float ValueToVolume(float value) {

		//Debug.Log(Mathf.Log10(value) * 20);
		//return Mathf.Log10(value) * 20;
		return Mathf.Lerp(dBslider.x, dBslider.y, value);

	}

}
