using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GrpA : Element {
    
    public float _blinkingLapsInSec = 1;
    private IEnumerator _blinkingCoroutine;
    
    public void LightOff() {
        if (_ledVisual != null) {
            _ledVisual.sprite = Resources.Load <Sprite>("Sprites/NoneRound");
        }
    }
    
    public void LightToWight() {
        if (_ledVisual != null) {
            _ledVisual.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Knob.psd");
        }
    }

    public void LightToRed() {
        if (_ledVisual != null) {
            _ledVisual.sprite = Resources.Load <Sprite>("Sprites/RedRound");
        }
    }
    
    public void LightToBlue() {
        if (_ledVisual != null) {
            _ledVisual.sprite = Resources.Load <Sprite>("Sprites/BlueRound");
        }
    }

    public void FadeToBlue() {
        if (_ledVisual != null) {
            _ledVisual.sprite = Resources.Load <Sprite>("Sprites/BlueRound");
        }
    }

    public void StartBlinking() {
        _blinkingCoroutine = BlinkingRed();
        StartCoroutine(_blinkingCoroutine);
    }

    public void StopBlinking() {
        if (_blinkingCoroutine != null) {
            StopCoroutine(_blinkingCoroutine);
            LightOff();
            _blinkingCoroutine = null;
        }
    }
    
    private IEnumerator BlinkingRed() {
        while (true) {
            LightToRed();
            yield return new WaitForSeconds(_blinkingLapsInSec);
            LightOff();
            yield return new WaitForSeconds(_blinkingLapsInSec);
        }
    }
    
}
