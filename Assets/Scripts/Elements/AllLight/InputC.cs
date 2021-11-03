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
                if (isTouchingOneTime() && currentLedColor() == ColorLed.Red && StoryManager.Instance.InputB.currentLedColor() == ColorLed.Green) {
                    LightToYellow();
                }
                if (isLetTouchOneTime() && currentLedColor() == ColorLed.Yellow && StoryManager.Instance.InputB.currentLedColor() == ColorLed.Green) {
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
