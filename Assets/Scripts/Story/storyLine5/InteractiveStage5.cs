using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class InteractiveStage5
{
    public Dialogue _question;
    public float _answerTimeLapsInSec = 3f;
    public Dialogue _helpMoode;
    public Dialogue _positivAnswer;
    public Dialogue _negativAnswer;

    public IEnumerator CinematicStage() {
        StoryManager.Instance.StageEnum = 5;
        StoryManager.Instance.VoiceEvent.DialogueEvent(_question);
        yield return null;
    }
}
