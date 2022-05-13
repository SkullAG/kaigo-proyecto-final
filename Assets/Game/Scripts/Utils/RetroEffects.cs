using HauntedPSX.RenderPipelines.PSX.Runtime;
using UnityEngine;
using UnityEngine.Rendering;
using NaughtyAttributes;

[RequireComponent(typeof(Volume))]
[ExecuteInEditMode]
public class RetroEffects : MonoBehaviour
{

    [SerializeField] private VolumeProfile _retroProfile;
    [SerializeField] private VolumeProfile _modernProfile;

    private Volume _volume;

    [ReadOnly] public bool effectsActivated = false;
    
    private void Awake() {

        _volume = GetComponent<Volume>();
        _volume.isGlobal = true;

    }
    
    [Button]
    public void Toggle() {

        if(!_volume) _volume = GetComponent<Volume>();

        effectsActivated = !effectsActivated;
        _volume.profile = effectsActivated ? _retroProfile : _modernProfile;

    }

}
