using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour {
    
    public StartRadioNoise _startRadioNoise;
    public InteractiveStage1 _interactiveStage1;
    public InteractiveStage2 _interactiveStage2;
    public InteractiveStage3 _interactiveStage3;
    public InteractiveStage3 InteractiveStage3{
        get { return _interactiveStage3; }
    }
    public InteractiveStage4 _interactiveStage4;
    public InteractiveStage5 _interactiveStage5;
    public InteractiveStage6 _interactiveStage6;
    public InteractiveStage7 _interactiveStage7;
    public InteractiveStage7 InteractiveStage7{
        get { return _interactiveStage7; }
    }
    
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
    public IEnumerator CurrentCinematicCoroutine{
        get { return currentCinematicCoroutine; }
    }
    
    private VoiceEvent _voiceEvent;
    public VoiceEvent VoiceEvent{
        get { return _voiceEvent; }
    }
    
    private SoundEvent _soundEvent;
    public SoundEvent SoundEvent{
        get { return _soundEvent; }
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
        _soundEvent = FindObjectOfType<SoundEvent>();
    }

    private void Start() {
        resetLights();
        
        StartStageCoroutineTimeLine(_startRadioNoise.StartingGame(_grpA, _door, _radio));
    }

    private void Update() {
        if (_stageEnum == 0 && _radio.isTouchingOneTime()) {
            StartStageCoroutineTimeLine(_interactiveStage1.CinematicStageIn(_grpA));
        }
    }
    
    public void InteractPositiveAnswer() {
        if (_radio.isTouching()) {
            StartCoroutine(_radio.CoroutineRadioInteractPositiveAnswer());
            switch (_stageEnum) {
                case 1:
                    StartStageCoroutineTimeLine(_interactiveStage1.CinematicStageOut(), _interactiveStage2.CinematicStageIn());
                    break;
                case 2:
                    StartStageCoroutineTimeLine(_interactiveStage2.CinematicStageOut(), _interactiveStage3.CinematicStageIn(_inputA, _radio));
                    break;
                case 4:
                    StartStageCoroutineTimeLine(_interactiveStage4.CinematicStageOut(), _interactiveStage5.CinematicStageIn());
                    break;
                case 5:
                    StartStageCoroutineTimeLine(_interactiveStage5.CinematicStagePosOut(), _interactiveStage6.CinematicStageIn(_inputA, _inputB, _inputC, _radio));
                    break;
            }
        }
    }
    
    public void InteractNegativeAnswer() {
        if (_radio.isTouching()) {
            StartCoroutine(_radio.CoroutineRadioInteractNegativeAnswer());
            switch (_stageEnum) {
                case 1:
                    StartStageCoroutineTimeLine(_interactiveStage1.CinematicStageNegAnswer());
                    break;
                case 2:
                    StartStageCoroutineTimeLine(_interactiveStage2.CinematicStageNegAnswer(), _interactiveStage3.CinematicStageIn(_inputA, _radio));
                    break;
                case 4:
                    StartStageCoroutineTimeLine(_interactiveStage4.CinematicStageOut(), _interactiveStage5.CinematicStageIn());
                    break;
                case 5:
                    StartStageCoroutineTimeLine(_interactiveStage5.CinematicStageNegOut(), _interactiveStage6.CinematicStageIn(_inputA, _inputB, _inputC, _radio));
                    break;
            }
        }
    }

    public void InteractApnea() {
        if (_inputA.isTouching()) {
            switch (_stageEnum) {
                case 3:
                    StartStageCoroutineTimeLine(_interactiveStage4.CinematicStageIn(_inputA, _radio));
                    break;
                case 6:
                    if (_inputB.currentLedColor() == ColorLed.Green && _inputC.currentLedColor() == ColorLed.Green) {
                        StartStageCoroutineTimeLine(_interactiveStage6.CinematicStageOut(_inputA, _radio), _interactiveStage7.CinematicStageIn(_door, _radio));
                    }
                    break;
                case 7:
                    StartStageCoroutineTimeLine(_interactiveStage7.CinematicSecondEnd(_inputA));
                    break;
            }
        }

        if (_inputB.isTouching()) {
            switch (_stageEnum) {
                case 6:
                    if (_inputC.currentLedColor() != ColorLed.Green) {
                        StartStageCoroutineTimeLine(_interactiveStage6.CinematicTouchingFirstB(_inputB, _radio));
                    }
                    else if (_inputC.currentLedColor() == ColorLed.Green) {
                        StartStageCoroutineTimeLine(_interactiveStage6.CinematicTouchingSecondB(_inputB, _radio));
                    }
                    break;
            }
        }

        if (_inputC.isTouching()) {
            switch (_stageEnum) {
                case 6:
                    if (_inputB.currentLedColor() != ColorLed.Green) {
                        StartStageCoroutineTimeLine(_interactiveStage6.CinematicTouchingFirstC(_inputC, _radio));
                    }
                    else if (_inputB.currentLedColor() == ColorLed.Green) {
                        StartStageCoroutineTimeLine(_interactiveStage6.CinematicTouchingSecondC(_inputC, _radio));
                    }
                    break;
            }
        }
    }

    public void StartStageCoroutineTimeLine(params IEnumerator[] coroutineTimeLine) {
        if (currentCinematicCoroutine != null) {
            StopCoroutine(currentCinematicCoroutine);
        }
        currentCinematicCoroutine = MultiCoroutine(coroutineTimeLine);
        StartCoroutine(currentCinematicCoroutine);
    }

    private IEnumerator MultiCoroutine(IEnumerator[] coroutineTimeLine) {
        foreach (IEnumerator routine in coroutineTimeLine) {
            yield return routine;
        }
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
