using UnityEngine;
using UnityEngine.Audio;

public class TestSound : MonoBehaviour {
    
    public AudioMixer audioMixer;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.F)) {
            fadeSoundToAmbient();
        } else if (Input.GetKeyDown(KeyCode.G)) {
            fadeSoundToGould();
        }
        
    }

    public void fadeSoundToAmbient() {
        if (audioMixer != null) {
            StartCoroutine(FadeMixerGroup.StartVolumeFade(audioMixer, "ambientVol", 5, 1));
            StartCoroutine(FadeMixerGroup.StartVolumeFade(audioMixer, "gouldVol", 5, 0));
        }
    }
    
    public void fadeSoundToGould() {
        if (audioMixer != null) {
            StartCoroutine(FadeMixerGroup.StartVolumeFade(audioMixer, "ambientVol", 5, 0));
            StartCoroutine(FadeMixerGroup.StartVolumeFade(audioMixer, "gouldVol", 5, 1));
        }
    }
}
