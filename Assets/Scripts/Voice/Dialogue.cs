using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class Dialogue{
    public String text;
    public AudioClip[] sound;

    public AudioClip chooseRandomAudioClip() {
        int random = Random.Range(0, sound.Length);
        return sound[random];
    }
}