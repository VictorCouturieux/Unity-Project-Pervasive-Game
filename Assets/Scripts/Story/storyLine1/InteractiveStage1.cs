using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class InteractiveStage1 : InteractiveStage {
    public Dialogue _thankGod;
    public Dialogue _understoodQuestion;
    public float _timeLapsTryAgain = 3;
    public Dialogue _positiveAnswer;
    public Dialogue _negativeAnswer;
    
    public IEnumerator CinematicStageIn(GrpA grpA) {
        StageEnum = 1;
        Event_Arduino.Instance.SendEventArduino();
        grpA.StopBlinking();
        yield return StartDialogueEvent(_thankGod);
        yield return StartDialogueEvent(_understoodQuestion);
    }

    public IEnumerator CinematicLoop() {
        StoryManager.Instance.WaitingLoop = true;
        while (StoryManager.Instance.WaitingLoop) {
            yield return new WaitForSeconds(_timeLapsTryAgain);
            if (!StoryManager.Instance.WaitingLoop){
                break;
            }
            yield return StartDialogueEvent(_negativeAnswer);
        }
    }

    public IEnumerator CinematicStageNegAnswer() {
        yield return StartDialogueEvent(_negativeAnswer);
    }

    public IEnumerator CinematicStageOut() {
        yield return StartDialogueEvent(_positiveAnswer);
    }

}
