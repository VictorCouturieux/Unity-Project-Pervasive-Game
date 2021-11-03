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
                if (isTouchingOneTime() && currentLedColor() == ColorLed.Red) {
                    LightToYellow();
                }
                if (isLetTouchOneTime() && currentLedColor() == ColorLed.Yellow) {
                    LightToRed();
                }
                break;
        }

    }
    
    public void LightOff() {
        if (_ledVisual != null) {
            _ledVisual.sprite = Resources.Load <Sprite>("Sprites/NoneRound");
        }
    }
    
    public void LightToYellow() {
        if (_ledVisual != null) {
            _ledVisual.sprite = Resources.Load <Sprite>("Sprites/YellowRound");
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
