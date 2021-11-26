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
        StoryManager.Instance.InputArduino.InputsRed();
        
        _canControl = true;
        _radio.StartHelpMode(_helpTimeLapsInSec, _helpInput);
        yield return null;
    }

    public IEnumerator CinematicTouchingFirstB( InputB _inputB, Radio _radio) {
        _canControl = false;
        yield return new WaitForSeconds(1);
        _inputB.LightToGreen();
        yield return new WaitForSeconds(1);
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
        yield return new WaitForSeconds(1);
        _inputB.LightToGreen();
        yield return new WaitForSeconds(1);
        _inputC.LightToGreen();
        yield return StartDialogueEvent(_inputBCSuccess);
        _canControl = true;
        _radio.StartHelpMode(_helpTimeLapsInSec, _helpInputA);
    }
    
    public IEnumerator CinematicTouchingFirstC(InputC _inputC, Radio _radio) {
        _canControl = false;
        yield return new WaitForSeconds(1);
        _inputC.LightToGreen();
        yield return new WaitForSeconds(1);
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
        StoryManager.Instance.InputArduino.StartScene7();
        yield return new WaitForSeconds(1);
        StoryManager.Instance.GrpA.StopBlinking();
        StoryManager.Instance.GrpA.StartBlueBlinking();

        yield return StartLeataDialogueEvent(_inputASuccess);
    }
}
