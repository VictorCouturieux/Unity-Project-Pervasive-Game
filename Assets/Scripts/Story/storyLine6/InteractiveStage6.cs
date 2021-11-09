using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class InteractiveStage6 : InteractiveStage {
    public float _helpTimeLapsInSec = 3f;
    public Dialogue _helpInput1;
    public Dialogue _helpInput2;
    public Dialogue _helpInputA;
    
    public SoundEffect _timerSound;
    public Dialogue _dontForget;
    public Dialogue _inputBCSuccess;
    public Dialogue _inputASuccess;
    public Dialogue _lighthouseHum;

    public IEnumerator CinematicStageIn(InputA _inputA, InputB _inputB, InputC _inputC, Radio _radio) {
        StageEnum = 6;
        _radio.StartHelpMode(_helpTimeLapsInSec, _helpInput1);
        _inputA.LightToRed();
        _inputB.LightToRed();
        _inputC.LightToRed();
        yield return null;
    }

    public IEnumerator CinematicTouchingFirstB(InputB _inputB, Radio _radio) {
        yield return StartSoundEvent(_timerSound, 1);
        yield return StartDialogueEvent(_dontForget);
        _radio.StartHelpMode(_helpTimeLapsInSec, _helpInput2);
        _inputB.LightToGreen();
        yield return null;
    }
    
    public IEnumerator CinematicTouchingSecondB(InputB _inputB, Radio _radio) {
        yield return StartDialogueEvent(_inputBCSuccess);
        _radio.StartHelpMode(_helpTimeLapsInSec, _helpInputA);
        _inputB.LightToGreen();
    }
    
    public IEnumerator CinematicTouchingFirstC(InputC _inputC, Radio _radio) {
        yield return StartSoundEvent(_timerSound, 1);
        yield return StartDialogueEvent(_dontForget);
        _radio.StartHelpMode(_helpTimeLapsInSec, _helpInput2);
        _inputC.LightToGreen();
        yield return null;
    }
    
    public IEnumerator CinematicTouchingSecondC(InputC _inputC, Radio _radio) {
        yield return StartDialogueEvent(_inputBCSuccess);
        _radio.StartHelpMode(_helpTimeLapsInSec, _helpInputA);
        _inputC.LightToGreen();
        yield return null;
    }

    public IEnumerator CinematicStageOut(InputA _inputA, Radio _radio) {
        _radio.StopCurrantHelpMode();
        _inputA.LightToGreen();
        yield return StartDialogueEvent(_inputASuccess);
        yield return StartDialogueEvent(_lighthouseHum);
    }
}
