using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class InteractiveStage
{
    private VoiceEvent _voiceEvent;
    public VoiceEvent VoiceEvent{
        get { return StoryManager.Instance.VoiceEvent; }
    }
    
    private SoundEvent _soundEvent;
    public SoundEvent SoundEvent{
        get { return StoryManager.Instance.SoundEvent; }
    }
    
    private int _stageEnum;
    public int StageEnum {
        get { return StoryManager.Instance.StageEnum; }
        set { StoryManager.Instance.StageEnum = value; }
    }

    public IEnumerator StartDialogueEvent(Dialogue dialogue) {
        VoiceEvent.DialogueEvent(dialogue);
        yield return VoiceEvent.StartPhareDialogueNow(dialogue);
    }
    
    public IEnumerator StartLeataDialogueEvent(Dialogue dialogue) {
        VoiceEvent.DialogueEvent(dialogue);
        yield return VoiceEvent.StartLeataDialogueNow(dialogue);
    }

    public IEnumerator StartSoundEvent(SoundEffect soundEffect, float fadeInDuration, bool isLevel6Looped) {
        SoundEvent.SoundEffectEvent(soundEffect);
        yield return SoundEvent.StartPhareAmbientNow(soundEffect, fadeInDuration, isLevel6Looped);
        StoryManager.Instance.AdditionalCoroutine = null;
    }
}
