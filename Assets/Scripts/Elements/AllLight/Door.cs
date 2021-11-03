using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : Element {
    
    public bool isTouchingOneTime() {
        return Input.GetKeyDown(KeyCode.D);
    }

    private void Update() {
        if (isTouchingOneTime() && StoryManager.Instance.StageEnum == 7) {
            StoryManager.Instance.VoiceEvent.DialogueEvent(
                StoryManager.Instance._interactiveStage7._goodEndFirst);
            LightToGreen();
            StoryManager.Instance.VoiceEvent.DialogueEvent(
                StoryManager.Instance._interactiveStage7._goodEndSecond);
        }
    }

    public void LightOff() {
        if (_ledVisual != null) {
            _ledVisual.sprite = Resources.Load <Sprite>("Sprites/NoneRound");
        }
    }
    
    public void LightToGreen() {
        if (_ledVisual != null) {
            _ledVisual.sprite = Resources.Load <Sprite>("Sprites/GreenRound");
        }
    }
    
    public void LightToRed() {
        if (_ledVisual != null) {
            _ledVisual.sprite = Resources.Load <Sprite>("Sprites/RedRound");
        }
    }
}
