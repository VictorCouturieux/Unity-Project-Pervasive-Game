using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputC : InputX
{
    public override bool isTouching() {
        return Input.GetKey(KeyCode.C);
    }
    
    public override bool isTouchingOneTime() {
        return Input.GetKeyDown(KeyCode.C);
    }

    public override bool isLetTouchOneTime() {
        return Input.GetKeyUp(KeyCode.C);
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
