using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class InteractiveStage2 : InteractiveStage {
    public Dialogue _question;
    public float _noAnswerTimeLapsInSec = 3f;
    public Dialogue _noAnswer;
    public Dialogue _posAnswer;
    public Dialogue _negAnswer;
    public Dialogue _endDialogue;
    
    public IEnumerator CinematicStageIn() {
        StageEnum = 2;
        VoiceEvent.DialogueEvent(_question);
//        yield return StartDialogueEvent(_question);
        yield return null;
    }
    
    public IEnumerator CinematicLoop() {
        StoryManager.Instance.WaitingLoop = true;
        while (StoryManager.Instance.WaitingLoop) {
            yield return new WaitForSeconds(_noAnswerTimeLapsInSec);
            if (!StoryManager.Instance.WaitingLoop){
                break;
            }
            yield return StartDialogueEvent(_noAnswer);
        }
    }
    
    public IEnumerator CinematicStageNegAnswer() {
        yield return StartDialogueEvent(_negAnswer);
        yield return StartDialogueEvent(_endDialogue);
    }

    public IEnumerator CinematicStageOut() {
        StageEnum = -1;
        yield return StartDialogueEvent(_posAnswer);
        yield return StartDialogueEvent(_endDialogue);
    }
}
