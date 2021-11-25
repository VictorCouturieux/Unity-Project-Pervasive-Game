using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputC : InputX
{
    
    public float ledLetContactTimeInSec = 1;
    private IEnumerator _routineLetContact = null;
    
    public override bool isTouching() {
        return Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.G);
    }
    
    public override bool isTouchingOneTime() {
        return Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.G);
    }

    public override bool isLetTouchOneTime() {
        return Input.GetKeyUp(KeyCode.C) || Input.GetKeyUp(KeyCode.G);
    }
    
    private void Update() {
        switch (StoryManager.Instance.StageEnum) {
            case 6:
                if (currentLedColor() == ColorLed.Red || currentLedColor() == ColorLed.Yellow) {
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
            LightToRed();
        }
        _routineLetContact = null;
    }
    
    public void LightOff() {
        if (_ledVisual != null) {
            _ledVisual.sprite = Resources.Load <Sprite>("Sprites/NoneRound");
        }
        input_Arduino.InputCBlack();
    }
    
    public void LightToYellow() {
        if (_ledVisual != null) {
            _ledVisual.sprite = Resources.Load <Sprite>("Sprites/YellowRound");
        }
        input_Arduino.InputCYellow();
    }
    
    public void LightToGreen() {
        if (_ledVisual != null) {
            _ledVisual.sprite = Resources.Load <Sprite>("Sprites/GreenRound");
        }
        input_Arduino.InputCGreen();
    }
    
    public void LightToRed() {
        if (_ledVisual != null) {
            _ledVisual.sprite = Resources.Load <Sprite>("Sprites/RedRound");
        }
        input_Arduino.InputCRed();
    }
}
