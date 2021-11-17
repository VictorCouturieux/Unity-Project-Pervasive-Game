using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class SoundEffect {
    public String text;
    public AudioClip[] sound;

    public AudioClip ChooseRandomAudioClip() {
        int random = Random.Range(0, sound.Length);
        return sound[random];
    }
}
