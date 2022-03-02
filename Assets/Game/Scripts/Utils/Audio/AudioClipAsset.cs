using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "Audio Clip", menuName = "Game/Audio Clip")]
public class AudioClipAsset : ScriptableObject
{

    public AudioClip clip;
    public AudioMixer mixer;
    public AudioMixerGroup group;

    public bool loop = false;

    [Range(0, 1)]
    public float volume = 1;

    [Header("Pitch")]

    [Range(-3, 3)]
    public float pitch = 1;

    public bool randomizePitch = false;

    [Range(-3, 3)]
    public float minPitch;

    [Range(-3, 3)]
    public float maxPitch;

    public float GetPitch() {

        if(randomizePitch) {
            return Random.Range(minPitch, maxPitch);
        }

        return pitch;

    }

}
