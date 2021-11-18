using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radio : Element {
    
    public float ledLetContactTimeInSec = 1;

    private IEnumerator _routineLetContact = null;
    private IEnumerator _routineHelpMode = null;
    public IEnumerator RoutineHelpMode {
        get { return _routineHelpMode; }
        set { _routineHelpMode = value; }
    }
    
    private bool _isHelpWaiting = false;
    private float lapsTimeLoop;
    private Dialogue dialogue;
    
    public bool IsTouching() {
        return Input.GetKey(KeyCode.R);
    }
    
    public bool IsTouchingOneTime() {
        return Input.GetKeyDown(KeyCode.R);
    }

    public bool IsLetTouchOneTime() {
        return Input.GetKeyUp(KeyCode.R);
    }
    
    private void Update() {
        if (StoryManager.Instance.StageEnum >= 0 && currentLedColor() != ColorLed.Red && currentLedColor() != ColorLed.Green) {
            if (IsTouchingOneTime()) {
                if (_routineLetContact != null) {
                    StopCoroutine(_routineLetContact);
                    _routineLetContact = null;
                }
                LightToYellow();
            }
            else if (IsLetTouchOneTime()) {
                _routineLetContact = CoroutineRadioLetContact();
                StartCoroutine(_routineLetContact);
            }
        }
        if (IsTouchingOneTime() && StoryManager.Instance.StageEnum == 7) {
            LightToGreen();
            StoryManager.Instance.StopForceCoroutineRadio1();
            StoryManager.Instance.StartCoroutineRadio1VoiceLine(
                StoryManager.Instance._interactiveStage7.CinematicFirstEnd());
        }
        
        
        if (IsTouchingOneTime() && _isHelpWaiting) {
            StoryManager.Instance.StopForceCoroutineRadio1();
            _isHelpWaiting = false;
            _routineHelpMode = RestartCoroutineHelpLoop(lapsTimeLoop, dialogue);
            StoryManager.Instance.StartCoroutineRadio1VoiceLine(_routineHelpMode);
        }
    }

    public void StartHelpMode(float lapsTimeLoop, Dialogue dialogue)
    {
        StoryManager.Instance.StopCoroutineRadio1VoiceLine();
        this.dialogue = dialogue;
        this.lapsTimeLoop = lapsTimeLoop;
        _routineHelpMode = CoroutineHelpMode(lapsTimeLoop, dialogue);
        StoryManager.Instance.StartCoroutineRadio1VoiceLine(_routineHelpMode);
    }

    public void StopCurrantHelpMode()
    {
        StoryManager.Instance.WaitingLoop = false;
    }

    public bool IsRunningHelpMode()
    {
        return _routineHelpMode != null;
    }

    private IEnumerator CoroutineHelpMode(float lapsTimeLoop, Dialogue helpDialogue)
    {
        StoryManager.Instance.WaitingLoop = true;
        yield return new WaitForSeconds(lapsTimeLoop);
        if (!StoryManager.Instance.WaitingLoop){
            yield break;
        }
        yield return RestartCoroutineHelpLoop(lapsTimeLoop, helpDialogue);
    }

    private IEnumerator RestartCoroutineHelpLoop(float lapsTimeLoop, Dialogue helpDialogue) {
        StoryManager.Instance.WaitingLoop = true;
        while (StoryManager.Instance.WaitingLoop) {
            yield return CinematicHelpCLip(helpDialogue);
            if (!StoryManager.Instance.WaitingLoop){
                break;
            }
            _isHelpWaiting = true;
            yield return new WaitForSeconds(lapsTimeLoop);
            _isHelpWaiting = false;
        }
        dialogue = null;
        this.lapsTimeLoop = 0;
        StoryManager.Instance.WaitingLoop = false;
        _routineHelpMode = null;
    }

    public IEnumerator CinematicHelpCLip(Dialogue helpDialogue) {
        StoryManager.Instance.VoiceEvent.DialogueEvent(helpDialogue);
        yield return StoryManager.Instance.VoiceEvent.StartPhareDialogueNow(helpDialogue);
    }

    public IEnumerator CoroutineRadioLetContact() {
        yield return new WaitForSeconds(ledLetContactTimeInSec);
        if (IsTouching()) {
            LightToYellow();
        }
        else {
            LightToBlue();
        }
        _routineLetContact = null;
    }
    
    public IEnumerator CoroutineRadioInteractPositiveAnswer() {
        if (currentLedColor() == ColorLed.Yellow) {
            while (IsTouching()) {
                LightToGreen();
                yield return new WaitForSeconds(0.1f);
            }
            LightToBlue();
        }
    }
    
    public IEnumerator CoroutineRadioInteractNegativeAnswer() {
        if (currentLedColor() == ColorLed.Yellow) {
            while (IsTouching()) {
                LightToRed();
                yield return new WaitForSeconds(0.1f);
            }
            LightToBlue();
        }
    }
    
    public void LightOff() {
        if (_ledVisual != null) {
            _ledVisual.sprite = Resources.Load <Sprite>("Sprites/NoneRound");
        }
        input_Arduino.RadioBlack();
    }
    
    public void LightToBlue() {
        if (_ledVisual != null) {
            _ledVisual.sprite = Resources.Load <Sprite>("Sprites/BlueRound");
        }
        input_Arduino.RadioBlue();
    } 
    
    public void LightToYellow() {
        if (_ledVisual != null) {
            _ledVisual.sprite = Resources.Load <Sprite>("Sprites/YellowRound");
        }
        input_Arduino.RadioYellow();
    }
    
    public void LightToGreen() {
        if (_ledVisual != null) {
            _ledVisual.sprite = Resources.Load <Sprite>("Sprites/GreenRound");
        }
        input_Arduino.RadioGreen();
    }
    
    public void LightToRed() {
        if (_ledVisual != null) {
            _ledVisual.sprite = Resources.Load <Sprite>("Sprites/RedRound");
        }
        input_Arduino.RadioRed();
    }
    
    

}
    
