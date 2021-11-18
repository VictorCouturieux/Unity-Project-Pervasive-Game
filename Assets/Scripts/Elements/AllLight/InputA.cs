using UnityEngine;
using UnityEngine.UI;

public class InputA : InputX {
    
    public override bool isTouching() {
        return Input.GetKey(KeyCode.A);
    }
    
    public override bool isTouchingOneTime() {
        return Input.GetKeyDown(KeyCode.A);
    }

    public override bool isLetTouchOneTime() {
        return Input.GetKeyUp(KeyCode.A);
    }

    private void Update() {
        switch (StoryManager.Instance.StageEnum) {
            case 3:
                if (isTouchingOneTime() && currentLedColor() == ColorLed.Red) {
                    StoryManager.Instance.StopCoroutineRadio1VoiceLine();
                    StoryManager.Instance.StartCoroutineRadio1VoiceLine(
                        StoryManager.Instance._interactiveStage3.CinematicStageFirstTouch(this));
                    LightToYellow();
                }
                if (isLetTouchOneTime() && currentLedColor() == ColorLed.Yellow) {
                    LightToRed();
                    StoryManager.Instance.StopCoroutineRadio1VoiceLine();
                    StoryManager.Instance.StartCoroutineRadio1VoiceLine(
                        StoryManager.Instance._interactiveStage3.StartHelpContact());
                }
                break;
            case 6 :
                if (StoryManager.Instance._interactiveStage6.CanControl) {
                    if (isTouchingOneTime() && currentLedColor() == ColorLed.Red &&
                        StoryManager.Instance.InputB.currentLedColor() == ColorLed.Green &&
                        StoryManager.Instance.InputC.currentLedColor() == ColorLed.Green) {
                        LightToYellow();
                    }

                    if (isLetTouchOneTime() && currentLedColor() == ColorLed.Yellow &&
                        StoryManager.Instance.InputB.currentLedColor() == ColorLed.Green &&
                        StoryManager.Instance.InputC.currentLedColor() == ColorLed.Green) {
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
    
    public void LightToRed() { //BLUE
        if (_ledVisual != null) {
            _ledVisual.sprite = Resources.Load <Sprite>("Sprites/RedRound");
        }
        input_Arduino.InputABlue();
    }

    
}
