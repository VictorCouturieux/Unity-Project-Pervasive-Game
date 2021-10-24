using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour {

    public float ledFeedBackTimeInSec = 3;
    
    public StartRadioNoise _startRadioNoise;
    public InteractivePhase1 _interactivePhase1;
    
    private InputA _inputA;
    private InputB _inputB;
    private InputC _inputC;

    private Door _door;
    private GrpA _grpA;
    private Radio _radio;
    
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
                    StartStageCoroutineTimeLine(_interactivePhase1.InteractiveStage1(_grpA, _radio));
                }
                break;
        }

        //effect sur multi stage
        if (_stageEnum > 0) {
            _radio.TouchRadioEffect(_stageEnum);
        }
    }
    
    //feedback

    public void RadioInteractPositiveAnswer() {
        StartCoroutine(_radio.CoroutineRadioInteractPositiveAnswer(ledFeedBackTimeInSec));
        switch (_stageEnum) {
            case 1:
                VoiceEvent.DialogueEvent(_interactivePhase1._positiveAnswer.text);
//                StartStageCoroutineTimeLine() //2
                break;
        }
    }
    
    public void RadioInteractNegativeAnswer() {
        StartCoroutine(_radio.CoroutineRadioInteractNegativeAnswer(ledFeedBackTimeInSec));
        switch (_stageEnum) {
            case 1:
                VoiceEvent.DialogueEvent(_interactivePhase1._negativeAnswer.text);
                break;
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
