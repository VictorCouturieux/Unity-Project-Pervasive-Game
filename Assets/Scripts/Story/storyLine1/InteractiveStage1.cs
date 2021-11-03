using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class InteractiveStage1 {
    public Dialogue _thankGod;
    public Dialogue _understoodQuestion;
    public float _timeLapsTryAgain = 3;
    public Dialogue _positiveAnswer;
    public Dialogue _negativeAnswer;
    
    public IEnumerator CinematicStage1(GrpA grpA, Radio radio) {
        StoryManager.Instance.VoiceEvent.DialogueEvent(_thankGod);
        grpA.StopBlinking();
        StoryManager.Instance.VoiceEvent.DialogueEvent(_understoodQuestion);
        StoryManager.Instance.StageEnum = 1;
        while (true) {
            yield return new WaitForSeconds(_timeLapsTryAgain);
            StoryManager.Instance.VoiceEvent.DialogueEvent(_negativeAnswer);
        }
    }

}
