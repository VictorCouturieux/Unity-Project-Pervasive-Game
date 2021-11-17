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
    
    public IEnumerator StartPhareAmbientNow(SoundEffect soundEffect, float fadeInDuration, bool isLevel6Looped)
    {
        StopForceSound();
        FadeMixerGroup.SetVolume(_audioMixer, "AmbientParam", 0);
        
        _ambient0.clip = soundEffect.ChooseRandomAudioClip();
        yield return new WaitForSeconds(0.01f);
        
        _ambient0.Play();
        StartCoroutine(FadeMixerGroup.StartFade(_audioMixer, "AmbientParam", fadeInDuration, 1));
        float totalTime = 0;
        bool oneTime = false;
        while (_ambient0.isPlaying)
        {
            Debug.Log("Audio clip length : " + _ambient0.clip.length);
            totalTime += Time.deltaTime;
            if (totalTime > _ambient0.clip.length - fadeInDuration && !oneTime)
            {
                StartCoroutine(FadeMixerGroup.StartFade(_audioMixer, "AmbientParam", fadeInDuration, 0));
                oneTime = true;
            }
            yield return null;
        }
        if (isLevel6Looped)
        {
            StoryManager.Instance.EventRestartLevel6ToRestSong();
        }
        _ambient0.Stop();
        yield break;
    }

    public void StopForceSound()
    {
        if (_ambient0.isPlaying) {
            _ambient0.Stop();
        }
    }

}
