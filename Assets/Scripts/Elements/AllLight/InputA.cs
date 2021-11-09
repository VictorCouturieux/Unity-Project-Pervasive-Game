using UnityEngine;
using UnityEngine.UI;

public class InputA : InputX {
    private bool _firstTouchingInputAStage3 = false;
    public bool FirstTouchingInputAStage3{
        get { return _firstTouchingInputAStage3; }
        set { _firstTouchingInputAStage3 = value; }
    }
    
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
                if (isTouchingOneTime()) {
                    if (!_firstTouchingInputAStage3) {
                        _firstTouchingInputAStage3 = true;
                        StoryManager.Instance.StartStageCoroutineTimeLine(StoryManager.Instance.InteractiveStage3.CinematicStageFirstTouch());
                    }
                    LightToYellow();
                    StoryManager.Instance.Radio.StartHelpMode(
                        StoryManager.Instance._interactiveStage3._timeLapsHelpToApnea, 
                        StoryManager.Instance._interactiveStage3._helpToApnea);
                }
                if (isLetTouchOneTime()) {
                    LightToRed();
                    StoryManager.Instance.Radio.StartHelpMode(
                        StoryManager.Instance._interactiveStage3._timeLapsHelpContact, 
                        StoryManager.Instance._interactiveStage3._helpContact);
                }
                break;
            case 6 :
                if (isTouchingOneTime() && currentLedColor() == ColorLed.Red && StoryManager.Instance.InputB.currentLedColor() == ColorLed.Green && StoryManager.Instance.InputC.currentLedColor() == ColorLed.Green) {
                    LightToYellow();
                }
                if (isLetTouchOneTime() && currentLedColor() == ColorLed.Yellow && StoryManager.Instance.InputB.currentLedColor() == ColorLed.Green && StoryManager.Instance.InputC.currentLedColor() == ColorLed.Green) {
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
