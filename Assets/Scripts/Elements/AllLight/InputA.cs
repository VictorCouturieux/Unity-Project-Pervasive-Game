using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InputA : InputX {
    
    public float ledLetContactTimeInSec = 1;
    private IEnumerator _routineLetContact = null;
    
    public override bool isTouching() {
        return Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Q);
    }
    
    public override bool isTouchingOneTime() {
        return Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Q);
    }

    public override bool isLetTouchOneTime() {
        return Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.Q);
    }

    private void Update() {
        switch (StoryManager.Instance.StageEnum) {
            case 3:
                if (currentLedColor() == ColorLed.Blue || currentLedColor() == ColorLed.Yellow)
                {
                    if (isTouchingOneTime()) {
                        if (_routineLetContact != null) {
                            StopCoroutine(_routineLetContact);
                            _routineLetContact = null;
                        }
                        if (currentLedColor() != ColorLed.Yellow)
                        {
                            LightToYellow();
                        }
                        StoryManager.Instance.StopCoroutineRadio1VoiceLine();
                        StoryManager.Instance.StartCoroutineRadio1VoiceLine(
                            StoryManager.Instance._interactiveStage3.CinematicStageFirstTouch(this));
                    }
                    else if (isLetTouchOneTime()) {
                        _routineLetContact = CoroutineRadioLetContact();
                        StartCoroutine(_routineLetContact);
                    }
                }
                break;
            case 6 :
                if (StoryManager.Instance.InputB.currentLedColor() == ColorLed.Green &&
                    StoryManager.Instance.InputC.currentLedColor() == ColorLed.Green 
                    && (currentLedColor() == ColorLed.Red || currentLedColor() == ColorLed.Yellow)) {
                    
                    if (isTouchingOneTime()) {
                        if (_routineLetContact != null) {
                            StopCoroutine(_routineLetContact);
                            _routineLetContact = null;
                        }
                        if (currentLedColor() != ColorLed.Yellow)
                        {
                            LightToYellow();
                        }
                    }
                    else if (isLetTouchOneTime()) {
                        _routineLetContact = CoroutineRadioLetContact();
                        StartCoroutine(_routineLetContact);
                    }
                }
                break;
        }
    }
    
    public IEnumerator CoroutineRadioLetContact() {
        yield return new WaitForSeconds(ledLetContactTimeInSec);
        if (isTouching()) {
            LightToYellow();
        }
        else {
            if (StoryManager.Instance.StageEnum == 3)
            {
                LightToBlue();
                StoryManager.Instance.StopCoroutineRadio1VoiceLine();
                StoryManager.Instance.StartCoroutineRadio1VoiceLine(
                    StoryManager.Instance._interactiveStage3.StartHelpContact());
            } else if (StoryManager.Instance.StageEnum == 6)
            {
                LightToRed();
            }
        }
        _routineLetContact = null;
    }

    public void LightOff() {
        if (_ledVisual != null) {
            _ledVisual.sprite = Resources.Load <Sprite>("Sprites/NoneRound");
        }
        input_Arduino.InputABlack();
    }
    
    public void LightToYellow() {
        if (_ledVisual != null) {
            _ledVisual.sprite = Resources.Load <Sprite>("Sprites/YellowRound");
        }
        input_Arduino.InputAYellow();
    }
    
    public void LightToGreen() {
        if (_ledVisual != null) {
            _ledVisual.sprite = Resources.Load <Sprite>("Sprites/GreenRound");
        }
        input_Arduino.InputAGreen();
    }
    
    public void LightToBlue() { //BLUE
        if (_ledVisual != null) {
            _ledVisual.sprite = Resources.Load <Sprite>("Sprites/BlueRound");
        }
        input_Arduino.InputABlue();
    }

    public void LightToRed() { 
        if (_ledVisual != null) {
            _ledVisual.sprite = Resources.Load <Sprite>("Sprites/RedRound");
        }
        input_Arduino.InputARed();
    }

    
}
