using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class InteractiveStage2 {
    public Dialogue _question;
    public float _noAnswerTimeLapsInSec = 3f;
    public Dialogue _noAnswer;
    public Dialogue _posAnswer;
    public Dialogue _negAnswer;
    public Dialogue _endDialogue;
    
    public IEnumerator CinematicStage2() {
        StoryManager.Instance.VoiceEvent.DialogueEvent(_question);
        StoryManager.Instance.StageEnum = 2;
        while (true) {
            yield return new WaitForSeconds(_noAnswerTimeLapsInSec);
            StoryManager.Instance.VoiceEvent.DialogueEvent(_noAnswer);
        }
    }
}
