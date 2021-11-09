using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class InteractiveStage4 : InteractiveStage{
    public Dialogue _question;
    public float _answerTimeLapsInSec = 12f;
    public Dialogue _answer;
    
    public IEnumerator CinematicStageIn(InputA _inputA, Radio _radio) {
        _inputA.LightToGreen();
        _radio.StopCurrantHelpMode();
        yield return StartDialogueEvent(_question);
        StageEnum = 4;
        yield return new WaitForSeconds(_answerTimeLapsInSec);
        yield return StartDialogueEvent(_answer);
    }

    public IEnumerator CinematicStageOut() {
        yield return StartDialogueEvent(_answer);
    }
}
