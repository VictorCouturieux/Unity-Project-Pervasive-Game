using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundEvent : MonoBehaviour {
    public AudioMixer _audioMixer;
    
    private AudioSource _ambient0;
    
    private void Awake() {
        _ambient0 = GameObject.Find("SoundPhare").GetComponent<AudioSource>();
    }
    
    public void SoundEffectEvent(SoundEffect soundEffect) {
        Debug.Log("<color=red>" + soundEffect.text + "</color>");
    }
    
    public IEnumerator StartPhareAmbientNow(SoundEffect soundEffect, float fadeInDuration) {
        if (_ambient0.isPlaying) {
            _ambient0.Stop();
        }
        _ambient0.clip = soundEffect.chooseRandomAudioClip();
        yield return new WaitForSeconds(0.01f);
        
        _ambient0.Play();
        StartCoroutine(FadeMixerGroup.StartFade(_audioMixer, "AmbientParam", fadeInDuration, 1));
        while (_ambient0.isPlaying)
        {
            yield return null;
        }
        _ambient0.Stop();
        yield break;
    }

}
