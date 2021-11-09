using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class InteractiveStage7 : InteractiveStage {
    public Dialogue _A_EndFirst;
    public Dialogue _A_EndSecond;
    public Dialogue _B_EndFirst;
    public Dialogue _B_EndSecond;

    public Dialogue _influence;
    public float _influenceTimeLapsInSec = 3;
    
    public float _winTimer = 6;
    
    public IEnumerator CinematicStageIn(Door _door, Radio radio) {
        StageEnum = 7;
        radio.StartHelpMode(_influenceTimeLapsInSec, _influence);
        yield return new WaitForSeconds(_winTimer);
        yield return StartDialogueEvent(_A_EndFirst);
        _door.LightToGreen();
        yield return StartDialogueEvent(_A_EndSecond);
    }

    public IEnumerator CinematicFirstEnd(Door door) {
        yield return StartDialogueEvent(_A_EndFirst);
//        door.LightToGreen();
        yield return StartDialogueEvent(_A_EndSecond);
    }
    
    public IEnumerator CinematicSecondEnd(InputA _inputA) {
        _inputA.LightToRed();
        yield return StartDialogueEvent(_B_EndFirst);
        yield return StartDialogueEvent(_B_EndSecond);
    }
}
