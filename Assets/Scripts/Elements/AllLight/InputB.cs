using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputB : InputX
{
    public override bool isTouching() {
        return Input.GetKey(KeyCode.B);
    }
    
    public override bool isTouchingOneTime() {
        return Input.GetKeyDown(KeyCode.B);
    }

    public override bool isLetTouchOneTime() {
        return Input.GetKeyUp(KeyCode.B);
    }
    
    private void Update() {
        switch (StoryManager.Instance.StageEnum) {
            case 6:
                if (StoryManager.Instance._interactiveStage6.CanControl) {
                    if (isTouchingOneTime() && currentLedColor() == ColorLed.Red) {
                        LightToYellow();
                    }

                    if (isLetTouchOneTime() && currentLedColor() == ColorLed.Yellow) {
                        LightToRed();
                    }
                }
                break;
        }

    }
    
    public void LightOff() {
        if (_ledVisual != null) {
            _ledVisual.sprite = Resources.Load <Sprite>("Sprites/NoneRound");
        }
        input_Arduino.InputBBlack();
    }
    
    public void LightToYellow() {
        if (_ledVisual != null) {
            _ledVisual.sprite = Resources.Load <Sprite>("Sprites/YellowRound");
        }
        input_Arduino.InputBYellow();
    }
    
    public void LightToGreen() {
        if (_ledVisual != null) {
            _ledVisual.sprite = Resources.Load <Sprite>("Sprites/GreenRound");
        }
        input_Arduino.InputBGreen();
    }
    
    public void LightToRed() {
        if (_ledVisual != null) {
            _ledVisual.sprite = Resources.Load <Sprite>("Sprites/RedRound");
        }
        input_Arduino.InputBRed();
    }
}
