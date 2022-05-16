using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AnimationSoundPlayer : MonoBehaviour
{

    public AudioClipAsset[] audioAssets;

    private Dictionary<string, AudioClipAsset> _dictionary = new Dictionary<string, AudioClipAsset>();

    private void Awake() {

        if(audioAssets != null) {

            foreach (AudioClipAsset asset in audioAssets) {

                _dictionary.Add(asset.name, asset);
                
            }

        }

    }
   
    public void Play(string name) {

        AudioManager.current?.Play(_dictionary[name]);

    }

}
