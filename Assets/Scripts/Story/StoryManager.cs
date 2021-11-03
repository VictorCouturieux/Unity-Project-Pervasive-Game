using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour {
    
    public StartRadioNoise _startRadioNoise;
    public InteractiveStage1 _interactiveStage1;
    public InteractiveStage2 _interactiveStage2;
    public InteractiveStage3 _interactiveStage3;
    public InteractiveStage4 _interactiveStage4;
    public InteractiveStage5 _interactiveStage5;
    public InteractiveStage6 _interactiveStage6;
    public InteractiveStage7 _interactiveStage7;
    
    private InputA _inputA;
    private InputB _inputB;
    public InputB InputB{
        get { return _inputB; }
    }
    private InputC _inputC;
    public InputC InputC{
        get { return _inputC; }
    }

    private Door _door;
    private GrpA _grpA;
    private Radio _radio;
    public Radio Radio{
        get { return _radio; }
    }
    
    private BarDeflate _barDeflate;
    private BarInflate _barInflate;

    private IEnumerator currentCinematicCoroutine;
    
    private VoiceEvent _voiceEvent;
    public VoiceEvent VoiceEvent{
        get { return _voiceEvent; }
    }
    
    private int _stageEnum = -1;
    public int StageEnum {
        get { return _stageEnum; }
        set { _stageEnum = value; }
    }

    private static StoryManager m_instance;
    public static StoryManager Instance { 
        get {
            if (!m_instance)
            {
                m_instance = new StoryManager();
            }
            return m_instance; 
        } 
    }
    
    private void Awake() {
        if (m_instance != null && m_instance != this) {
            Destroy(gameObject);
        }
        else {
            m_instance = this;
        }
        
        _inputA = FindObjectOfType<InputA>();
        _inputB = FindObjectOfType<InputB>();
        _inputC = FindObjectOfType<InputC>();
        
        _door = FindObjectOfType<Door>();
        _grpA = FindObjectOfType<GrpA>();
        _radio = FindObjectOfType<Radio>();
        
        _barDeflate = FindObjectOfType<BarDeflate>();
        _barInflate = FindObjectOfType<BarInflate>();
        
        _voiceEvent = FindObjectOfType<VoiceEvent>();
    }

    private void Start() {
        resetLights();
        
        StartStageCoroutineTimeLine(_startRadioNoise.StartingGame(_grpA, _door, _radio));
    }

    private void Update() {
        switch (_stageEnum) {
            case 0 :
                if (_radio.isTouchingOneTime()) {
                    StartStageCoroutineTimeLine(_interactiveStage1.CinematicStage1(_grpA, _radio));
                }
                break;
        }
    }
    
    public void InteractPositiveAnswer() {
        if (_radio.isTouching()) {
            StartCoroutine(_radio.CoroutineRadioInteractPositiveAnswer());
            switch (_stageEnum) {
                case 1:
                    VoiceEvent.DialogueEvent(_interactiveStage1._positiveAnswer);
                    StartStageCoroutineTimeLine(_interactiveStage2.CinematicStage2());
                    break;
                case 2:
                    VoiceEvent.DialogueEvent(_interactiveStage2._posAnswer);
                    VoiceEvent.DialogueEvent(_interactiveStage2._endDialogue);
                    StartStageCoroutineTimeLine(_interactiveStage3.CinematicStage(_inputA));
                    break;
                case 4:
                    VoiceEvent.DialogueEvent(_interactiveStage4._answer);
                    StartStageCoroutineTimeLine(_interactiveStage5.CinematicStage());
                    break;
                case 5:
                    VoiceEvent.DialogueEvent(_interactiveStage5._positivAnswer);
                    StartStageCoroutineTimeLine(_interactiveStage6.CinematicStage(_inputA, _inputB, _inputC, _radio));
                    break;
            }
        }
    }
    
    public void InteractNegativeAnswer() {
        if (_radio.isTouching()) {
            StartCoroutine(_radio.CoroutineRadioInteractNegativeAnswer());
            switch (_stageEnum) {
                case 1:
                    VoiceEvent.DialogueEvent(_interactiveStage1._negativeAnswer);
                    break;
                case 2:
                    VoiceEvent.DialogueEvent(_interactiveStage2._negAnswer);
                    VoiceEvent.DialogueEvent(_interactiveStage2._endDialogue);
                    StartStageCoroutineTimeLine(_interactiveStage3.CinematicStage(_inputA));
                    break;
                case 4:
                    VoiceEvent.DialogueEvent(_interactiveStage4._answer);
                    StartStageCoroutineTimeLine(_interactiveStage5.CinematicStage());
                    break;
                case 5:
                    VoiceEvent.DialogueEvent(_interactiveStage5._negativAnswer);
                    StartStageCoroutineTimeLine(_interactiveStage6.CinematicStage(_inputA, _inputB, _inputC, _radio));
                    break;
            }
        }
    }

    public void InteractApnea() {
        if (_inputA.isTouching()) {
            switch (_stageEnum) {
                case 3:
                    StartStageCoroutineTimeLine(_interactiveStage4.CinematicStage(_inputA, _radio));
                    break;
                case 6:
                    if (_inputB.currentLedColor() == ColorLed.Green && 
                        _inputC.currentLedColor() == ColorLed.Green) {
                        _radio.StopCurrantHelpMode();
                        _inputA.LightToGreen();
                        VoiceEvent.DialogueEvent(_interactiveStage6._inputASuccess);
                        VoiceEvent.DialogueEvent(_interactiveStage6._lighthouseHum);
                        StartStageCoroutineTimeLine(_interactiveStage7.CinematicStage(_door));
                    }
                    break;
                case 7:
                    _inputA.LightToRed();
                    VoiceEvent.DialogueEvent(_interactiveStage7._badEndFirst);
                    VoiceEvent.DialogueEvent(_interactiveStage7._badEndSecond);
                    break;
            }
        }

        if (_inputB.isTouching()) {
            switch (_stageEnum) {
                case 6:
                    VoiceEvent.DialogueEvent(_interactiveStage6._timerSound);
                    _radio.StartHelpMode(_interactiveStage6._helpTimeLapsInSec, _interactiveStage6._helpInputC);
                    _inputB.LightToGreen();
                    break;
            }
        }

        if (_inputC.isTouching()) {
            switch (_stageEnum) {
                case 6:
                    if (_inputB.currentLedColor() == ColorLed.Green) {
                        VoiceEvent.DialogueEvent(_interactiveStage6._inputBCSuccess);
                        _radio.StartHelpMode(_interactiveStage6._helpTimeLapsInSec, _interactiveStage6._helpInputA);
                        _inputC.LightToGreen();
                    }
                    break;
            }
        }
    }

    private void StartStageCoroutineTimeLine(IEnumerator coroutineTimeLine) {
        if (currentCinematicCoroutine != null) {
            StopCoroutine(currentCinematicCoroutine);
        }
        currentCinematicCoroutine = coroutineTimeLine;
        StartCoroutine(currentCinematicCoroutine);
    }

    private void resetLights() {
        _inputA.LightOff();
        _inputB.LightOff();
        _inputC.LightOff();
        _door.LightOff();
        _grpA.LightOff();
        _radio.LightOff();
    }

}
