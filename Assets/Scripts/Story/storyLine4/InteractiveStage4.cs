using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class InteractiveStage4 : InteractiveStage
{
    public Dialogue _question;
    public float _answerTimeLapsInSec = 12f;

    private bool _isWaiting = false;

    public IEnumerator CinematicStageIn(InputA _inputA, Radio _radio)
    {
        ShowStageNUM = 4;
        StageEnum = 4;
        //Event_Arduino.Instance.SendEventArduino();
        _radio.StopCurrantHelpMode();
        yield return new WaitForSeconds(1); 
        StoryManager.Instance.InputArduino.StartScene4();
        yield return new WaitForSeconds(1);
        StoryManager.Instance.GrpA.StopBlinking();
        StoryManager.Instance.GrpA.LightToRed();
        yield return GreenLedSomeSec(_inputA);
        yield return StartDialogueEvent(_question);
    }

    public IEnumerator GreenLedSomeSec(InputA _inputA)
    {
        _inputA.LightToGreen();
        yield return new WaitForSeconds(2);
        _inputA.LightToBlue();
    }

    public IEnumerator CinematicTimeLapsOut(GrpA grpA)
    {
        _isWaiting = true;
        yield return new WaitForSeconds(_answerTimeLapsInSec);
        _isWaiting = false;
        StoryManager.Instance.StopCoroutineRadio1VoiceLine();
        StoryManager.Instance.StartCoroutineRadio1VoiceLine(StoryManager.Instance._interactiveStage5
            .CinematicStageIn(grpA), StoryManager.Instance._interactiveStage5.CinematicTimeLapsOut());
    }

    public void StopTimeLapsOut(GrpA grpA)
    {
        if (_isWaiting)
        {
            StoryManager.Instance.StopForceCoroutineRadio1();
            StoryManager.Instance.StartCoroutineRadio1VoiceLine(StoryManager.Instance._interactiveStage5
                .CinematicStageIn(grpA), StoryManager.Instance._interactiveStage5.CinematicTimeLapsOut());
        }
    }
}