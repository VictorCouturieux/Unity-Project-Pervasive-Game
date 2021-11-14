using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class InteractiveStage5 : InteractiveStage {
    public Dialogue scrambled;
    public Dialogue _question;
    public Dialogue _positivAnswer;
    public Dialogue _negativAnswer;

    public IEnumerator CinematicStageIn() {
        StageEnum = 5;
        yield return StartDialogueEvent(scrambled);
        yield return StartDialogueEvent(_question);
    }

    public IEnumerator CinematicStagePosOut() {
        yield return StartDialogueEvent(_positivAnswer);
    }
    
    public IEnumerator CinematicStageNegOut() {
        yield return StartDialogueEvent(_negativAnswer);
    }
    
    
}
