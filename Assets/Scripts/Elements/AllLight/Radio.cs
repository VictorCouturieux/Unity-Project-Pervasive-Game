using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radio : Element {
    
    public float ledLetContactTimeInSec = 1;

    private IEnumerator _routineLetContact = null;
    private IEnumerator _routineHelpMode = null;
    private Dialogue _currantHelpDialogue;
    
    public bool isTouching() {
        return Input.GetKey(KeyCode.R);
    }
    
    public bool isTouchingOneTime() {
        return Input.GetKeyDown(KeyCode.R);
    }

    public bool isLetTouchOneTime() {
        return Input.GetKeyUp(KeyCode.R);
    }
    
    private void Update() {
        if (StoryManager.Instance.StageEnum >= 0) {
            if (currentLedColor() != ColorLed.Red && currentLedColor() != ColorLed.Green) {
                if (isTouchingOneTime()) {
                    if (_routineLetContact != null) {
                        StopCoroutine(_routineLetContact);
                        _routineLetContact = null;
                    }
                    LightToYellow();
                }
                else if (isLetTouchOneTime()) {
                    _routineLetContact = CoroutineRadioLetContact();
                    StartCoroutine(_routineLetContact);
                }
            }
        }
        
        if (_routineHelpMode != null && isTouchingOneTime()) {
            StopCoroutine(_routineHelpMode);
            StartCoroutine(_routineHelpMode);
        }
    }

    public void StartHelpMode(float lapsTimeLoop, Dialogue dialogue) {
        StopCurrantHelpMode();
        _currantHelpDialogue = dialogue;
        _routineHelpMode = CoroutineHelpMode(lapsTimeLoop, dialogue);
        StartCoroutine(_routineHelpMode);
    }

    public void StopCurrantHelpMode() {
        if (_routineHelpMode != null) {
            StopCoroutine(_routineHelpMode);
            _routineHelpMode = null;
        }
    }
    
    private IEnumerator CoroutineHelpMode(float lapsTimeLoop, Dialogue helpDialogue) {
        while (true) {
            yield return new WaitForSeconds(lapsTimeLoop);
            StoryManager.Instance.VoiceEvent.DialogueEvent(helpDialogue);
        }
    }
    
    public IEnumerator CoroutineRadioLetContact() {
        yield return new WaitForSeconds(ledLetContactTimeInSec);
        if (isTouching()) {
            LightToYellow();
        }
        else {
            LightToBlue();
        }
        _routineLetContact = null;
    }
    
    public IEnumerator CoroutineRadioInteractPositiveAnswer() {
        if (currentLedColor() == ColorLed.Yellow) {
            while (isTouching()) {
                LightToGreen();
                yield return new WaitForSeconds(0.1f);
            }
            LightToBlue();
        }
    }
    
    public IEnumerator CoroutineRadioInteractNegativeAnswer() {
        if (currentLedColor() == ColorLed.Yellow) {
            while (isTouching()) {
                LightToRed();
                yield return new WaitForSeconds(0.1f);
            }
            LightToBlue();
        }
    }
    
    public void LightOff() {
        if (_ledVisual != null) {
            _ledVisual.sprite = Resources.Load <Sprite>("Sprites/NoneRound");
        }
    }
    
    public void LightToBlue() {
        if (_ledVisual != null) {
            _ledVisual.sprite = Resources.Load <Sprite>("Sprites/BlueRound");
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
    
