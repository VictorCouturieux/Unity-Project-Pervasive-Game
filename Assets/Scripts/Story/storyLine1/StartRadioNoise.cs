using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class StartRadioNoise {
    public float _lightWhiteLapsSec = 3;
    public float _lightOffLapsSec = 3;
    public Dialogue _radioNoise;
    public float _startRadioSoundLapsSec = 3;
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
        
        //todo : Start RadioNoise Sound
        StoryManager.Instance.VoiceEvent.DialogueEvent(_radioNoise.text);
        
        yield return new WaitForSeconds(_startRadioSoundLapsSec);
        StoryManager.Instance.StageEnum = 0;
        while (true) {
            StoryManager.Instance.VoiceEvent.DialogueEvent(_allo.text);
            yield return new WaitForSeconds(_AlloLaps);
        }
    }
}
