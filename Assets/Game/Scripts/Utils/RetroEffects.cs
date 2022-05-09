using HauntedPSX.RenderPipelines.PSX.Runtime;
using UnityEngine;
using UnityEngine.Rendering;
using NaughtyAttributes;

[RequireComponent(typeof(Volume))]
[ExecuteInEditMode]
public class RetroEffects : MonoBehaviour
{

    [SerializeField]
    private VolumeProfile _profile;

    private Volume _volume;
    private PrecisionVolume _precisionVolume;
    private CameraVolume _cameraVolume;

    [ReadOnly]
    public bool effectsActivated = false;
    
    private void Awake() {

        _volume = GetComponent<Volume>();
        _volume.profile = _profile;
        _volume.isGlobal = true;

        _profile.TryGet(out _precisionVolume);
        _profile.TryGet(out _cameraVolume);

        // BUILD PRESENTACION
        SetEnabled(false);

    }

    private void OnValidate() {

        if(_precisionVolume == null) _profile.TryGet(out _precisionVolume);
        if(_cameraVolume == null) _profile.TryGet(out _cameraVolume);

    }
    
    [Button]
    public void Toggle() {

        effectsActivated = !effectsActivated;

        SetEnabled(effectsActivated);

    }

    public void SetEnabled(bool enabled) {

        if(enabled) {

            _precisionVolume.geometryEnabled.value = true;
            _precisionVolume.geometry.value = 0.8f;
            _precisionVolume.framebufferDither.value = 1;
            _precisionVolume.color.value = 0.5f;

            _cameraVolume.targetRasterizationResolutionHeight.value = 320;
            _cameraVolume.targetRasterizationResolutionWidth.value = 320;

        } else {

            _precisionVolume.geometryEnabled.value = false;

            _cameraVolume.targetRasterizationResolutionHeight.value = 1280;
            _cameraVolume.targetRasterizationResolutionWidth.value = 960;
            _precisionVolume.framebufferDither.value = 0.25f;
            _precisionVolume.color.value = 0.75f;

        }

    }

}
