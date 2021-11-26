using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class InteractiveStage5 : InteractiveStage {
    public SoundEffect scrambled;
    public float volumeScrambled = 0.5f;
    public Dialogue _question;
    public float _answerTimeLapsInSec = 12f;
    public Dialogue _positivAnswer;
    public Dialogue _negativAnswer;
    
    private bool _isWaiting = false;
    public bool IsWaiting
    {
        get { return _isWaiting; }
    }
    
    public IEnumerator CinematicStageIn(GrpA grpA) {
        ShowStageNUM = 5;
        StageEnum = 5;
        //Event_Arduino.Instance.SendEventArduino();
        yield return new WaitForSeconds(1); 
        StoryManager.Instance.InputArduino.StartScene5();
        yield return new WaitForSeconds(1);
        StoryManager.Instance.GrpA.StopBlinking();
        grpA.StartBlueBlinking();
        yield return StartSoundEvent(scrambled, 1, false, volumeScrambled);
        grpA.StopBlinking();
        grpA.LightToRed();
        yield return StartDialogueEvent(_question);
    }
    
    public IEnumerator CinematicTimeLapsOut()
    {
        _isWaiting = true;
        yield return new WaitForSeconds(_answerTimeLapsInSec);
        _isWaiting = false;
        StoryManager.Instance.StopCoroutineRadio1VoiceLine();
        StoryManager.Instance.StartCoroutineRadio1VoiceLine(CinematicStageNegOut());
    }

    public IEnumerator CinematicStagePosOut() {
        ShowStageNUM = 6;
        
        yield return new WaitForSeconds(1); 
        StoryManager.Instance.InputArduino.StartScene6();
        yield return new WaitForSeconds(1);
        StoryManager.Instance.GrpA.StopBlinking();
        StoryManager.Instance.GrpA.LightToRed();
        
        yield return StartDialogueEvent(_positivAnswer);
    }
    
    public IEnumerator CinematicStageNegOut() {
        ShowStageNUM = 6;
        
        yield return new WaitForSeconds(1); 
        StoryManager.Instance.InputArduino.StartScene6();
        yield return new WaitForSeconds(1);
        StoryManager.Instance.GrpA.StopBlinking();
        StoryManager.Instance.GrpA.LightToRed();
        
        yield return StartDialogueEvent(_negativAnswer);
    }
    
    
}
