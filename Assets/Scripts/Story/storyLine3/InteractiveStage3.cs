using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class InteractiveStage3 : InteractiveStage {
    public Dialogue _reload;
    public float _timeLapsHelpContact = 3;
    public Dialogue _helpContact;
    public Dialogue _contactInputA;
    public float _timeLapsHelpToApnea = 3;
    public Dialogue _helpToApnea;
    
    private bool _firstTouchingInputAStage3 = false;
    public bool FirstTouchingInputAStage3
    {
        get { return _firstTouchingInputAStage3; }
        set { _firstTouchingInputAStage3 = value; }
    }

    public IEnumerator CinematicStageIn(InputA _inputA, Radio radio) {
        ShowStageNUM = 3;
        StageEnum = -1;
        _firstTouchingInputAStage3 = false;
        _inputA.LightToRed();
        yield return StartDialogueEvent(_reload);
        StageEnum = 3;
        Event_Arduino.Instance.SendEventArduino();
        radio.StartHelpMode(_timeLapsHelpContact, _helpContact);
    }

    public IEnumerator CinematicStageFirstTouch(InputA _inputA) {
        if (!_firstTouchingInputAStage3)
        {
            _firstTouchingInputAStage3 = true;
            yield return StartDialogueEvent(_contactInputA);
        }
        if (_inputA.currentLedColor() == ColorLed.Green)
        {
            yield break;
        }
        StoryManager.Instance.Radio.StartHelpMode(_timeLapsHelpToApnea, _helpToApnea);
        yield return null;
    }

    public IEnumerator StartHelpContact()
    {
        StoryManager.Instance.Radio.StartHelpMode(
            _timeLapsHelpContact, 
            _helpContact);
        yield return null;
    }
}
