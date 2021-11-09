using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VoiceEvent : MonoBehaviour {

    public AudioMixer _audioMixer;

    private AudioSource _asPhare;
    private AudioSource _asLeata;

    private IEnumerator _currentDialogue;

    private void Awake() {
        _asPhare = GameObject.Find("DialoguePhare").GetComponent<AudioSource>();
        _asLeata = GameObject.Find("DialogueLeata").GetComponent<AudioSource>();
    }

    public void DialogueEvent(Dialogue dialogueStr) {
        Debug.Log(dialogueStr.text);
    }

    public IEnumerator StartPhareDialogueNow(Dialogue dialogueStr) {
        if (_asPhare.isPlaying) {
            _asPhare.Stop();
        }
        _asPhare.clip = dialogueStr.chooseRandomAudioClip();
        yield return new WaitForSeconds(0.01f);
        
        _asPhare.Play();
        while (_asPhare.isPlaying)
        {
            yield return null;
        }
        _asPhare.Stop();
        yield break;
        
    }
    
}
