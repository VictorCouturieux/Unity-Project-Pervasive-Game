using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Element : MonoBehaviour
{
    protected Image _ledVisual;

    private void Awake() {
        _ledVisual = GetComponent<Image>();
    }
    
    public ColorLed currentLedColor() {
        if (_ledVisual.sprite == Resources.Load <Sprite>("Sprites/NoneRound")) {
            return ColorLed.None;
        }
        if (_ledVisual.sprite == AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Knob.psd")) {
            return ColorLed.White;
        }
        if (_ledVisual.sprite == Resources.Load <Sprite>("Sprites/BlueRound")) {
            return ColorLed.Blue;
        }
        if (_ledVisual.sprite == Resources.Load <Sprite>("Sprites/YellowRound")) {
            return ColorLed.Yellow;
        }
        if (_ledVisual.sprite == Resources.Load <Sprite>("Sprites/RedRound")) {
            return ColorLed.Red;
        }
        if (_ledVisual.sprite == Resources.Load <Sprite>("Sprites/GreenRound")) {
            return ColorLed.Green;
        }
        return ColorLed.None;
    }
}

public enum ColorLed {
    None,
    White,
    Blue,
    Yellow,
    Red,
    Green
}

public interface IInteractiveLed {
    
}
