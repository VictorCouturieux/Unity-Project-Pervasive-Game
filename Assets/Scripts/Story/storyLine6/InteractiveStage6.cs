using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class InteractiveStage6 {
    public float _helpTimeLapsInSec = 3f;
    public Dialogue _helpInputB;
    public Dialogue _helpInputC;
    public Dialogue _helpInputA;
    
    public Dialogue _timerSound;
    public Dialogue _inputBCSuccess;
    public Dialogue _inputASuccess;
    public Dialogue _lighthouseHum;

    public IEnumerator CinematicStage(InputA _inputA, InputB _inputB, InputC _inputC, Radio _radio) {
        StoryManager.Instance.StageEnum = 6;
        _radio.StartHelpMode(_helpTimeLapsInSec, _helpInputB);
        _inputA.LightToRed();
        _inputB.LightToRed();
        _inputC.LightToRed();
        yield return null;
    }
}
