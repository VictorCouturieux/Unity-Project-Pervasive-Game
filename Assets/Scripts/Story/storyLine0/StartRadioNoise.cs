using System;
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
        grpA.StartBlinking();

        yield return StartSoundEvent(_radioNoise, _startRadioSoundFadeDuration);
        
        StageEnum = 0;
        while (true) {
            yield return StartDialogueEvent(_allo);
            yield return new WaitForSeconds(_AlloLaps);
        }
    }
}
