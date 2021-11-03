using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VoiceEvent : MonoBehaviour {

    public void DialogueEvent(Dialogue dialogueStr) {
        Debug.Log(dialogueStr.text);
    }
    
}
