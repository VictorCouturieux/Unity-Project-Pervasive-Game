using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radio : Element
{
    public bool isTouching() {
        return Input.GetKey(KeyCode.R);
    }
    
    public bool isTouchingOneTime() {
        return Input.GetKeyDown(KeyCode.R);
    }
    
    public void TouchRadioEffect(int stageEnum) {
        if (currentLedColor() != ColorLed.Red && currentLedColor() != ColorLed.Green) {
            if (isTouching()) {
                LightToYellow();
            }
            else {
                LightToBlue();
            }
        }
    }
    
    public IEnumerator CoroutineRadioInteractPositiveAnswer(float ledFeedBackTimeInSec) {
        if (currentLedColor() == ColorLed.Yellow) {
            LightToGreen();
        }
        yield return new WaitForSeconds(ledFeedBackTimeInSec);
        if (isTouching()) {
            LightToYellow();
        }
        else {
            LightToBlue();
        }
    }
    
    public IEnumerator CoroutineRadioInteractNegativeAnswer(float ledFeedBackTimeInSec) {
        if (currentLedColor() == ColorLed.Yellow) {
            LightToRed();
        }
        yield return new WaitForSeconds(ledFeedBackTimeInSec);
        if (isTouching()) {
            LightToYellow();
        }
        else {
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
    
