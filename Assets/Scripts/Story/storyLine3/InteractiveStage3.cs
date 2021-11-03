using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class InteractiveStage3 {
    public Dialogue _reload;
    public float _timeLapsHelpContact = 3;
    public Dialogue _helpContact;
    public Dialogue _contactInputA;
    public float _timeLapsHelpToApnea = 3;
    public Dialogue _helpToApnea;

    public IEnumerator CinematicStage(InputA _inputA) {
        _inputA.LightToRed();
        StoryManager.Instance.VoiceEvent.DialogueEvent(_reload);
        StoryManager.Instance.StageEnum = 3;
        StoryManager.Instance.Radio.StartHelpMode(_timeLapsHelpContact, _helpContact);
        yield break;
    }
    
    
}
