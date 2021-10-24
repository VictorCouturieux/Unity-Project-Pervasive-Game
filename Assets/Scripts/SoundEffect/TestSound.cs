using UnityEngine;
using UnityEngine.Audio;

public class TestSound : MonoBehaviour {
    
    public AudioMixer audioMixer;

    private void Update() {
        if (Input.GetKey(KeyCode.A)) {
            fadeSoundToAmbient();
        } else if (Input.GetKey(KeyCode.G)) {
            fadeSoundToGould();
        }
        
    }

    public void fadeSoundToAmbient() {
        if (audioMixer != null) {
            StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "ambientVol", 5, 1));
            StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "gouldVol", 5, 0));
        }
    }
    
    public void fadeSoundToGould() {
        if (audioMixer != null) {
            StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "ambientVol", 5, 0));
            StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "gouldVol", 5, 1));
        }
    }
}
