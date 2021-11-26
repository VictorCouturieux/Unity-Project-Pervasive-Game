using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class InteractiveStage2 : InteractiveStage {
    public Dialogue _question;
    public float _noAnswerTimeLapsInSec = 3f;
    public Dialogue _noAnswer;
    public Dialogue _posAnswer;
    public SoundEffect _negAnswer;
    public float volumeNegAnswer = 0.5f;
    
    public Dialogue _endDialogue;
    
    public IEnumerator CinematicStageIn(GrpA grpA) {
        ShowStageNUM = 2;
        StageEnum = -1;
        //Event_Arduino.Instance.SendEventArduino();
        yield return new WaitForSeconds(1); 
        StoryManager.Instance.InputArduino.StartScene2();
        yield return new WaitForSeconds(1);
        grpA.StopBlinking();
        grpA.LightToRed();
        VoiceEvent.DialogueEvent(_question);
        StageEnum = 2;
//        yield return StartDialogueEvent(_question);
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
        yield return StartSoundEvent(_negAnswer, 1, false, volumeNegAnswer);
        yield return StartDialogueEvent(_endDialogue);
    }

    public IEnumerator CinematicStageOut() {
        StageEnum = -1;
        yield return StartDialogueEvent(_posAnswer);
        yield return StartDialogueEvent(_endDialogue);
    }
}
