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
            LightToGreen();
            StoryManager.Instance.StopForceCoroutineRadio1();
            StoryManager.Instance.StartCoroutineRadio1VoiceLine(StoryManager.Instance._interactiveStage7.CinematicFirstEnd());
        }
    }

    public void LightOff() {
        if (_ledVisual != null) {
            _ledVisual.sprite = Resources.Load <Sprite>("Sprites/NoneRound");
        }
        //input_Arduino.DoorBlack();
    }
    
    public void LightToGreen() {
        if (_ledVisual != null) {
            _ledVisual.sprite = Resources.Load <Sprite>("Sprites/GreenRound");
        }
        //input_Arduino.DoorGreen();
    }
    
    public void LightToRed() {
        if (_ledVisual != null) {
            _ledVisual.sprite = Resources.Load <Sprite>("Sprites/RedRound");
        }
        // input_Arduino.DoorRed();
    }
}
