using System;
using UnityEngine;

[Serializable]
public class Dialogue{
    public DialogueType type;
    public String text;
    public AudioClip[] sound;
}

public enum DialogueType {
    React,
    Question
}