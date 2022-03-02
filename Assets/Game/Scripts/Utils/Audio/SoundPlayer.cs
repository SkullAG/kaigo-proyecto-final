using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    
    public void Play(AudioClipAsset asset) {

        AudioManager.current.Play(asset);

    }

}
