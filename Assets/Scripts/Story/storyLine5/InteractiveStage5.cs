using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class InteractiveStage5 : InteractiveStage {
    public SoundEffect scrambled;
    public float volumeScrambled = 0.5f;
    public Dialogue _question;
    public Dialogue _positivAnswer;
    public Dialogue _negativAnswer;

    public IEnumerator CinematicStageIn(GrpA grpA) {
        ShowStageNUM = 5;
        StageEnum = 5;
        Event_Arduino.Instance.SendEventArduino();
        grpA.StartBlueBlinking();
        yield return StartSoundEvent(scrambled, 1, false, volumeScrambled);
        grpA.StopBlinking();
        yield return new WaitForSeconds(1);
        grpA.LightToRed();
        yield return StartDialogueEvent(_question);
    }

    public IEnumerator CinematicStagePosOut() {
        ShowStageNUM = 6;
        yield return StartDialogueEvent(_positivAnswer);
    }
    
    public IEnumerator CinematicStageNegOut() {
        yield return StartDialogueEvent(_negativAnswer);
    }
    
    
}
