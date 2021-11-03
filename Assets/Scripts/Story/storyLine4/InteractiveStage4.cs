using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class InteractiveStage4 
{
    public Dialogue _question;
    public float _answerTimeLapsInSec = 3f;
    public Dialogue _answer;
    
    public IEnumerator CinematicStage(InputA _inputA, Radio _radio) {
        _inputA.LightToGreen();
        _radio.StopCurrantHelpMode();
        StoryManager.Instance.VoiceEvent.DialogueEvent(_question);
        StoryManager.Instance.StageEnum = 4;
        yield break;
    }
}
