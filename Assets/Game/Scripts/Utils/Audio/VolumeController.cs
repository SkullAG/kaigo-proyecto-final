using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{

    [SerializeField, Range(0, 2)]
    private float bgm = 1;

    [SerializeField]
    private Slider _bgmSlider;

    [SerializeField]
    private AudioMixerGroup _bgmGroup;

    [Space(15)]

    [SerializeField, Range(0, 2)]
    private float bgs = 1;

    [SerializeField]
    private Slider _bgsSlider;

    [SerializeField]
    private AudioMixerGroup _bgsGroup;

    [Space(15)]

    [SerializeField, Range(0, 2)]
    private float se = 1;

    [SerializeField]
    private Slider _seSlider;
    
    [SerializeField]
    private AudioMixerGroup _seGroup;

    private float _originalBGMVolume = 0f;
    private float _originalBGSVolume = 0f;
    private float _originalSEVolume = 0f;

    private void Awake() {

        _bgmGroup.audioMixer.GetFloat("bgmVol", out _originalBGMVolume);
        _bgsGroup.audioMixer.GetFloat("bgsVol", out _originalBGSVolume);
        _seGroup.audioMixer.GetFloat("seVol", out _originalSEVolume);

    }

    public void UpdateBGM() {

        bgm = _bgmSlider.value;

        _bgmGroup.audioMixer.SetFloat("bgmVol", ValueToVolume(bgm) + _originalBGMVolume);

    }

    public void UpdateBGS(float value) {

        bgs = _bgsSlider.value;

        _bgsGroup.audioMixer.SetFloat("bgsVol", ValueToVolume(bgs) + _originalBGSVolume);

    }

    public void UpdateSE(float value) {

        se = _seSlider.value;

        _seGroup.audioMixer.SetFloat("seVol", ValueToVolume(se) + _originalSEVolume);

    }

    private float ValueToVolume(float value) {

        return Mathf.Log10(value) * 20;

    }

}
