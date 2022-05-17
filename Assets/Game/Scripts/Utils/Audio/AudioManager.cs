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

    public void Awake() {

        Transform _sourceContainer = new GameObject("Audio Sources").transform;
        _sourceContainer.SetParent(transform);

        _sourcePool = new AudioSource[_sourceNumber];

        for(int i = 0; i < _sourceNumber; i++) {
            _sourcePool[i] = new GameObject().AddComponent<AudioSource>();//_sourceContainer.gameObject.AddComponent<AudioSource>();
            _sourcePool[i].transform.parent = transform;
            _sourcePool[i].transform.position = Vector3.zero;
            _sourcePool[i].playOnAwake = false;
            _sourcePool[i].rolloffMode = AudioRolloffMode.Linear;
        }

    }

    public void Play( AudioClipAsset audioClipAsset ) {

        AudioSource _source = _sourcePool.FirstOrDefault( x => !x.isPlaying );

        if(_source == null) {
            Debug.LogWarning("No available audio sources to play " + audioClipAsset.name + "!");
            return;
        }

        _source.clip = audioClipAsset.clips[Random.Range(0, audioClipAsset.clips.Length)];

        _source.volume = audioClipAsset.volume;
        _source.pitch = audioClipAsset.GetPitch();
        _source.loop = audioClipAsset.loop;

        _source.outputAudioMixerGroup = audioClipAsset.group;

        _source.transform.position = Vector3.zero;

        _source.spatialBlend = 0;

        _source.Play();

    }

    public void Play3D(AudioClipAsset audioClipAsset, Vector3 position)
    {
        AudioSource _source = _sourcePool.FirstOrDefault(x => !x.isPlaying);

        if (_source == null)
        {
            Debug.LogWarning("No available audio sources to play " + audioClipAsset.name + "!");
            return;
        }

        _source.clip = audioClipAsset.clips[Random.Range(0, audioClipAsset.clips.Length)];

        _source.volume = audioClipAsset.volume;
        _source.pitch = audioClipAsset.GetPitch();
        _source.loop = audioClipAsset.loop;

        _source.outputAudioMixerGroup = audioClipAsset.group;

        _source.transform.position = position;

        _source.spatialBlend = 1;

        _source.maxDistance = audioClipAsset.maxDistance;
        _source.minDistance = audioClipAsset.minDistance;


        _source.Play();

    }

    public void Stop(AudioClipAsset audioClipAsset) {

        AudioSource _source = _sourcePool.FirstOrDefault( x => x.isPlaying && x.clip == audioClipAsset.clips[0] ); // Mal

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
