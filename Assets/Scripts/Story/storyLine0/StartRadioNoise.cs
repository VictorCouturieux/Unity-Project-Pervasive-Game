﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class StartRadioNoise : InteractiveStage {
    public float _lightWhiteLapsSec = 3;
    public float _lightOffLapsSec = 3;
    public SoundEffect _radioNoise;
    public float _startRadioSoundFadeDuration = 3;
    public Dialogue _allo;
    public float _AlloLaps = 3;

    public IEnumerator StartingGame(GrpA grpA, Door door, Radio radio) {
        grpA.LightToWight();
        yield return new WaitForSeconds(_lightWhiteLapsSec);
        grpA.LightOff();
        yield return new WaitForSeconds(_lightOffLapsSec);
        radio.LightToBlue();
        door.LightToRed();
        grpA.StartRedBlinking();

        yield return StartSoundEvent(_radioNoise, _startRadioSoundFadeDuration, false);
        
        StageEnum = 0;
        Event_Arduino.Instance.SendEventArduino();
        StoryManager.Instance.WaitingLoop = true;
        while (StoryManager.Instance.WaitingLoop) {
            yield return StartDialogueEvent(_allo);
            if (!StoryManager.Instance.WaitingLoop){
                break;
            }
            yield return new WaitForSeconds(_AlloLaps);
        }
    }
}
