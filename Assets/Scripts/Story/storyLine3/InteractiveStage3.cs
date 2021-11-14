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

    public IEnumerator CinematicStageIn(InputA _inputA, Radio radio) {
        StageEnum = -1;
        _firstTouchingInputAStage3 = false;
        _inputA.LightToRed();
        yield return StartDialogueEvent(_reload);
        StageEnum = 3;
        radio.StartHelpMode(_timeLapsHelpContact, _helpContact);
    }

    public IEnumerator CinematicStageFirstTouch() {
        if (!_firstTouchingInputAStage3) {
            Debug.Log("_contactInputA");
            yield return StartDialogueEvent(_contactInputA);
            _firstTouchingInputAStage3 = true;
        }
        StoryManager.Instance.Radio.StartHelpMode(_timeLapsHelpToApnea, _helpToApnea);
        yield return null;
    }
}
