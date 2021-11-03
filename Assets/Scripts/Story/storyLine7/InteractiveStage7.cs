using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class InteractiveStage7 {
    public Dialogue _goodEndFirst;
    public Dialogue _goodEndSecond;
    public Dialogue _badEndFirst;
    public Dialogue _badEndSecond;

    public Dialogue _influence;
    public float _influenceTimeLapsInSec = 3;
    
    public float _winTimer = 6;
    
    public IEnumerator CinematicStage(Door _door) {
        StoryManager.Instance.StageEnum = 7;
        StoryManager.Instance.Radio.StartHelpMode(_influenceTimeLapsInSec, _influence);
        yield return new WaitForSeconds(_winTimer);
        StoryManager.Instance.VoiceEvent.DialogueEvent(
            StoryManager.Instance._interactiveStage7._goodEndFirst);
        _door.LightToGreen();
        StoryManager.Instance.VoiceEvent.DialogueEvent(
            StoryManager.Instance._interactiveStage7._goodEndSecond);
    }
}
