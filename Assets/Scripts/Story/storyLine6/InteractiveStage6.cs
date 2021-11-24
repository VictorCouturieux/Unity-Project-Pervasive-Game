using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class InteractiveStage6 : InteractiveStage {
    public float _helpTimeLapsInSec = 3f;
    public Dialogue _helpInput;
    public Dialogue _helpInputA;
    
    public SoundEffect _timerSound;
    public float firstVolumeTimerSound = 0.1f;
    public float otherVolumeTimerSound = 0.5f;
    public Dialogue _dontForget;
    public Dialogue _inputBCSuccess;
    public Dialogue _inputASuccess;

    private bool _canControl = false;
    public bool CanControl {
        get { return _canControl; }
        set { _canControl = value; }
    }
    
    private bool _isFirstLoopLevel6 = true;
    public bool IsFirstLoopLevel6 {
        get { return _isFirstLoopLevel6; }
        set { _isFirstLoopLevel6 = value; }
    }

    public IEnumerator CinematicStageIn(InputA _inputA, InputB _inputB, InputC _inputC, Radio _radio) {
        StageEnum = 6;
        Event_Arduino.Instance.SendEventArduino();
        
        yield return new WaitForSeconds(1);
        _radio.LightToBlue();
        yield return new WaitForSeconds(1);
        _inputA.LightToRed();
        yield return new WaitForSeconds(1);
        _inputB.LightToRed();
        yield return new WaitForSeconds(1);
        _inputC.LightToRed();
        
        _canControl = true;
        _radio.StartHelpMode(_helpTimeLapsInSec, _helpInput);
        yield return null;
    }

    public IEnumerator CinematicTouchingFirstB( InputB _inputB, Radio _radio) {
        _canControl = false;
        _inputB.LightToGreen();
        if (IsFirstLoopLevel6)
        {
            StoryManager.Instance.StartAdditionalCoroutinePlayedWithMain(StartSoundEvent(_timerSound, 1, true, firstVolumeTimerSound));
            yield return StartDialogueEvent(_dontForget);
            _isFirstLoopLevel6 = false;
        }
        else
        {
            StoryManager.Instance.StartAdditionalCoroutinePlayedWithMain(StartSoundEvent(_timerSound, 1, true, otherVolumeTimerSound));
        }
        _canControl = true;
    }
    
    public IEnumerator CinematicTouchingSecond(InputB _inputB, InputC _inputC, Radio _radio) {
        _canControl = false;
        _inputB.LightToGreen();
        yield return new WaitForSeconds(1);
        _inputC.LightToGreen();
        yield return StartDialogueEvent(_inputBCSuccess);
        _canControl = true;
        _radio.StartHelpMode(_helpTimeLapsInSec, _helpInputA);
    }
    
    public IEnumerator CinematicTouchingFirstC(InputC _inputC, Radio _radio) {
        _canControl = false;
        _inputC.LightToGreen();
        if (IsFirstLoopLevel6)
        {
            StoryManager.Instance.StartAdditionalCoroutinePlayedWithMain(StartSoundEvent(_timerSound, 1, true, firstVolumeTimerSound));
            yield return StartDialogueEvent(_dontForget);
            _isFirstLoopLevel6 = false;
        }
        else
        {
            StoryManager.Instance.StartAdditionalCoroutinePlayedWithMain(StartSoundEvent(_timerSound, 1, true, otherVolumeTimerSound));
        }
        _canControl = true;
    }

    public IEnumerator CinematicStageOut(InputA _inputA, Radio _radio, Door _door, GrpA _grpA) {
        ShowStageNUM = 7;
        _radio.StopCurrantHelpMode();
        yield return new WaitForSeconds(1);
        _inputA.LightToGreen();
        yield return new WaitForSeconds(1);
        _door.LightToGreen();
        yield return new WaitForSeconds(1);
        _grpA.StartBlueBlinking();
        yield return StartLeataDialogueEvent(_inputASuccess);
    }
}
