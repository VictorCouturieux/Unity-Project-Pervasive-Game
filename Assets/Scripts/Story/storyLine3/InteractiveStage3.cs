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

    public IEnumerator CinematicStageIn(InputA _inputA, Radio radio) {
        StageEnum = 3;
        _inputA.FirstTouchingInputAStage3 = false;
        _inputA.LightToRed();
        yield return StartDialogueEvent(_reload);
        radio.StartHelpMode(_timeLapsHelpContact, _helpContact);
        yield break;
    }

    public IEnumerator CinematicStageFirstTouch() {
        yield return StartDialogueEvent(_contactInputA);
    }
}
