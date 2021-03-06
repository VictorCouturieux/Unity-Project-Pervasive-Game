using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class InteractiveStage7 : InteractiveStage {
    public Dialogue _A_EndFirst;
    public Dialogue _A_EndSecond;
    public Dialogue _endSecond;

    public Dialogue _influence;
    public float _influenceTimeLapsInSec = 3;
    
    public float _winTimer = 12;

    public float cutLightTimer = 4;
    public SoundEffect _lighthouseHum;
    public float volumeLighthouseHum = 0.4f;
    
    public IEnumerator CinematicStageIn(Door _door, Radio radio) {
        StageEnum = 7;
        Event_Arduino.Instance.SendEventArduino();
        float time = 0;
        while (time <= _winTimer) {
            yield return new WaitForSeconds(_influenceTimeLapsInSec);
            time += _influenceTimeLapsInSec;
            // Debug.Log("_winTimer : " + _winTimer + " /_influenceTimeLapsInSec : " + _influenceTimeLapsInSec + " /time : " + time);
            yield return StartLeataDialogueEvent(_influence);;
        }
        yield return CinematicFirstEnd();
    }

    public IEnumerator CinematicFirstEnd() {
        StoryManager.Instance.StageEnum = -1;
        yield return StartDialogueEvent(_A_EndFirst);
//        door.LightToGreen();
        yield return StartLeataDialogueEvent(_A_EndSecond);
        StoryManager.Instance.StartAdditionalCoroutinePlayedWithMain(EndLighthouseHum());
        yield return new WaitForSeconds(cutLightTimer);
        yield return StoryManager.Instance.ResetLightsRoutine();
    }
    
    public IEnumerator CinematicSecondEnd(InputA _inputA) {
        StoryManager.Instance.StageEnum = -1;
        _inputA.LightToBlue();
        yield return StartLeataDialogueEvent(_endSecond);
        StoryManager.Instance.StartAdditionalCoroutinePlayedWithMain(EndLighthouseHum());
        yield return new WaitForSeconds(cutLightTimer);
        yield return StoryManager.Instance.ResetLightsRoutine();
    }

    public IEnumerator EndLighthouseHum()
    {
        yield return StartSoundEvent(_lighthouseHum, 3, false, volumeLighthouseHum);
    }
}
