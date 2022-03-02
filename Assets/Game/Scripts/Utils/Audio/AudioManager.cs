using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System.Linq;

public class AudioManager : Singleton<AudioManager>
{

    [SerializeField]
    private AudioSource[] _sourcePool;

    [SerializeField]
    private int _sourceNumber = 10;

    public override void Awake() {

        base.Awake();

        Transform _sourceContainer = new GameObject("Audio Sources").transform;
        _sourceContainer.SetParent(transform);

        _sourcePool = new AudioSource[_sourceNumber];

        for(int i = 0; i < _sourceNumber; i++) {
            _sourcePool[i] = _sourceContainer.gameObject.AddComponent<AudioSource>();
            _sourcePool[i].playOnAwake = false;
        }

    }

    public void Play( AudioClipAsset audioClipAsset ) {

        AudioSource _source = _sourcePool.FirstOrDefault( x => !x.isPlaying );

        if(_source == null) {
            Debug.LogWarning("No available audio sources to play " + audioClipAsset.clip.name + "!");
            return;
        }

        _source.clip = audioClipAsset.clip;
        _source.volume = audioClipAsset.volume;
        _source.pitch = audioClipAsset.GetPitch();
        _source.loop = audioClipAsset.loop;

        _source.outputAudioMixerGroup = audioClipAsset.group;

        _source.Play();

    }

    public void Stop(AudioClipAsset audioClipAsset) {

        AudioSource _source = _sourcePool.FirstOrDefault( x => x.isPlaying && x.clip == audioClipAsset.clip );

        if(_source == null) {
            return;
        }

        _source.Stop();
        _source.loop = false;

    }

    public void StopGroup(AudioMixerGroup group) {

        foreach (AudioSource scr in _sourcePool) {
            if(scr.outputAudioMixerGroup == group) scr.Stop();
        }

    }

}
