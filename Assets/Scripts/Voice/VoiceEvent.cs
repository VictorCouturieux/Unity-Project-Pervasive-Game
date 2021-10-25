using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VoiceEvent : MonoBehaviour {
//    public float timeLapsShowingText = 1.5f;
    
//    private static String noText = "[No Text]";
    
//    private Text dialogueText;

    private void Awake() {
//        dialogueText = gameObject.GetComponent<Text>();
    }

    public void DialogueEvent(String dialogueStr) {
//        StartCoroutine(showingTextLaps(dialogueStr));
        Debug.Log(dialogueStr);
    }

//    IEnumerator showingTextLaps(String str) {
//        dialogueText.text = str;
//        yield return new WaitForSeconds(timeLapsShowingText);
//        dialogueText.text = "";
//    }
    
}
