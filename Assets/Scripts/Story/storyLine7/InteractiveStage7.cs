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
    
    public IEnumerator CinematicStageIn(Door _door, Radio radio) {
        StageEnum = 7;
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
        yield return StartDialogueEvent(_A_EndFirst);
//        door.LightToGreen();
        yield return StartLeataDialogueEvent(_A_EndSecond);
        yield return new WaitForSeconds(cutLightTimer);
        StoryManager.Instance.resetLights();
    }
    
    public IEnumerator CinematicSecondEnd(InputA _inputA) {
        _inputA.LightToRed();
        yield return StartLeataDialogueEvent(_endSecond);
        StoryManager.Instance.StartAdditionalCoroutinePlayedWithMain(EndLighthouseHum());
        yield return new WaitForSeconds(cutLightTimer);
        StoryManager.Instance.resetLights();
    }

    public IEnumerator EndLighthouseHum()
    {
        yield return StartSoundEvent(_lighthouseHum, 3, false);
    }
}
