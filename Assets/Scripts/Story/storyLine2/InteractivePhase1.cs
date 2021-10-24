using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class InteractivePhase1 {
    public Dialogue _thankGod;
    public float _lightYellowLapsSec = 3;
    public Dialogue _understoodQuestion;
    public float _timeLapsTryAgain = 3;
    public Dialogue _noAnswer;
    public Dialogue _positiveAnswer;
    public Dialogue _negativeAnswer;
    
    public IEnumerator InteractiveStage1(GrpA grpA, Radio radio) {
        StoryManager.Instance.VoiceEvent.DialogueEvent(_thankGod.text);
        grpA.StopBlinking();
        radio.LightToYellow();
        yield return new WaitForSeconds(_lightYellowLapsSec);
        radio.LightToBlue();
        StoryManager.Instance.VoiceEvent.DialogueEvent(_understoodQuestion.text);
        StoryManager.Instance.StageEnum = 1;
        while (true) {
            yield return new WaitForSeconds(_timeLapsTryAgain);
            StoryManager.Instance.VoiceEvent.DialogueEvent(_noAnswer.text);
        }
    }

}
