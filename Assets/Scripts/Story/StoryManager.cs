using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{

    public Text _stageEnumShowText;

    public float _breathingSlopeTolerance = 5f;
    
    public StartRadioNoise _startRadioNoise;
    public InteractiveStage1 _interactiveStage1;
    public InteractiveStage2 _interactiveStage2;
    public InteractiveStage3 _interactiveStage3;
    public InteractiveStage4 _interactiveStage4;
    public InteractiveStage5 _interactiveStage5;
    public InteractiveStage6 _interactiveStage6;
    public InteractiveStage7 _interactiveStage7;

    private InputA _inputA;
    public InputA InputA{
        get { return _inputA; }
    }
    private InputB _inputB;
    public InputB InputB{
        get { return _inputB; }
    }
    private InputC _inputC;
    public InputC InputC{
        get { return _inputC; }
    }

    private Door _door;
    public Door Door{
        get { return _door; }
    }
    private GrpA _grpA;
    public GrpA GrpA{
        get { return _grpA; }
    }
    private Radio _radio;
    public Radio Radio{
        get { return _radio; }
    }

    private Queue<IEnumerator> routineList;

    private IEnumerator currentCinematicCoroutine;
    public IEnumerator CurrentCinematicCoroutine
    {
        get { return currentCinematicCoroutine; }
        set { currentCinematicCoroutine = value; }
    }
    private IEnumerator _additionalCoroutine;
    public IEnumerator AdditionalCoroutine
    {
        get { return _additionalCoroutine; }
        set { _additionalCoroutine = value; }
    }
    
    private bool _waitingLoop = false;
    public bool WaitingLoop
    {
        get { return _waitingLoop; }
        set => _waitingLoop = value;
    }

    private VoiceEvent _voiceEvent;
    public VoiceEvent VoiceEvent{
        get { return _voiceEvent; }
    }
    
    private SoundEvent _soundEvent;
    public SoundEvent SoundEvent{
        get { return _soundEvent; }
    }
    
    private int toggleShowStageNUM = -1;
    private int _showStageNUM = -1;
    public int ShowStageNUM {
        get { return _showStageNUM; }
        set { _showStageNUM = value; }
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
        
        routineList = new Queue<IEnumerator>();
        
        _voiceEvent = FindObjectOfType<VoiceEvent>();
        _soundEvent = FindObjectOfType<SoundEvent>();
    }

    private void Start() {
        //resetLights();
        
        StartCoroutineRadio1VoiceLine(_startRadioNoise.StartingGame(_grpA, _door, _radio));
    }

    private void Update() {
        if (_stageEnum == 0 && _radio.IsTouchingOneTime()) {
            StopForceCoroutineRadio1();
            StartCoroutineRadio1VoiceLine(_interactiveStage1.CinematicStageIn(_grpA), _interactiveStage1.CinematicLoop() );
        }
        
        if (_stageEnumShowText != null && toggleShowStageNUM != ShowStageNUM)
        {
            _stageEnumShowText.text = ShowStageNUM.ToString();
        }

        toggleShowStageNUM = ShowStageNUM;
    }
    
    public void InteractPositiveAnswer() {
        if (Radio.IsTouching()) {
            StartCoroutine(_radio.CoroutineRadioInteractPositiveAnswer());
            switch (_stageEnum) {
                case 1:
                    StopCoroutineRadio1VoiceLine();
                    StartCoroutineRadio1VoiceLine(_interactiveStage1.CinematicStageOut(), _interactiveStage2.CinematicStageIn(), _interactiveStage2.CinematicLoop());
                    break;
                case 2:
                    StopCoroutineRadio1VoiceLine();
                    StartCoroutineRadio1VoiceLine(_interactiveStage2.CinematicStageOut(), _interactiveStage3.CinematicStageIn(_inputA, _radio));
                    break;
                case 4:
                    _interactiveStage4.StopTimeLapsOut(_grpA);
                    break;
                case 5:
                    StopCoroutineRadio1VoiceLine();
                    StartCoroutineRadio1VoiceLine(_interactiveStage5.CinematicStagePosOut(), _interactiveStage6.CinematicStageIn(_inputA, _inputB, _inputC, _radio));
                    break;
            }
        }
    }
    
    public void InteractNegativeAnswer() {
        if (Radio.IsTouching()) {
            StartCoroutine(_radio.CoroutineRadioInteractNegativeAnswer());
            switch (_stageEnum) {
                case 1:
                    StopCoroutineRadio1VoiceLine();
                    StartCoroutineRadio1VoiceLine(_interactiveStage1.CinematicStageNegAnswer(), _interactiveStage1.CinematicLoop());
                    break;
                case 2:
                    StopCoroutineRadio1VoiceLine();
                    StartCoroutineRadio1VoiceLine(_interactiveStage2.CinematicStageNegAnswer(), _interactiveStage3.CinematicStageIn(_inputA, _radio));
                    break;
                case 4:
                    _interactiveStage4.StopTimeLapsOut(_grpA);
                    break;
                case 5:
                    StopCoroutineRadio1VoiceLine();
                    StartCoroutineRadio1VoiceLine(_interactiveStage5.CinematicStageNegOut(), _interactiveStage6.CinematicStageIn(_inputA, _inputB, _inputC, _radio));
                    break;
            }
        }
    }

    public void InteractApnea() {
        if (_inputA.isTouching()) {
            switch (_stageEnum) {
                case 3:
                    StopCoroutineRadio1VoiceLine();
                    _inputA.LightToGreen();
                    StartCoroutineRadio1VoiceLine(_interactiveStage4.CinematicStageIn(_inputA, _radio), _interactiveStage4.CinematicTimeLapsOut(_grpA));
                    break;
                case 6:
                    StopCoroutineRadio1VoiceLine();
                    if (_inputB.currentLedColor() == ColorLed.Green && _inputC.currentLedColor() == ColorLed.Green && _interactiveStage6.CanControl) {
                        _inputA.LightToGreen();
                        StartCoroutineRadio1VoiceLine(_interactiveStage6.CinematicStageOut(_inputA, _radio, _door, _grpA), _interactiveStage7.CinematicStageIn(_door, _radio));
                    }
                    break;
                case 7:
                    StopForceCoroutineRadio1();
                    _inputA.LightToBlue();
                    StartCoroutineRadio1VoiceLine(_interactiveStage7.CinematicSecondEnd(_inputA));
                    break;
            }
        }

        if (_inputB.isTouching()) {
            switch (_stageEnum) {
                case 6:
                    if (_inputC.currentLedColor() != ColorLed.Green) {
                        StopCoroutineRadio1VoiceLine();
                        _inputB.LightToGreen();
                        StartCoroutineRadio1VoiceLine(_interactiveStage6.CinematicTouchingFirstB(_inputB, _radio));
                    }
                    else if (_inputC.currentLedColor() == ColorLed.Green && _interactiveStage6.CanControl) {
                        StopCoroutineRadio1VoiceLine();
                        _inputB.LightToGreen();
                        StopAdditionalCoroutineTimeLine6();
                    }
                    break;
            }
        }

        if (_inputC.isTouching()) {
            switch (_stageEnum) {
                case 6:
                    if (_inputB.currentLedColor() != ColorLed.Green) {
                        StopCoroutineRadio1VoiceLine();
                        _inputC.LightToGreen();
                        StartCoroutineRadio1VoiceLine(_interactiveStage6.CinematicTouchingFirstC(_inputC, _radio));
                    }
                    else if (_inputB.currentLedColor() == ColorLed.Green && _interactiveStage6.CanControl) {
                        StopCoroutineRadio1VoiceLine();
                        _inputC.LightToGreen();
                        StopAdditionalCoroutineTimeLine6();
                    }
                    break;
            }
        }
    }

    public void EventRestartLevel6ToRestSong()
    {
        StopCoroutineRadio1VoiceLine();
        StageEnum = -1;
        _radio.StopCurrantHelpMode();
        _interactiveStage6.CanControl = false;
        StartCoroutineRadio1VoiceLine(_interactiveStage6.CinematicStageIn(_inputA, _inputB, _inputC, _radio));
    }

    public void StartAdditionalCoroutinePlayedWithMain(IEnumerator coroutineTimeLine)
    {
        if (_additionalCoroutine == null)
        {
            _additionalCoroutine = coroutineTimeLine;
            StartCoroutine(_additionalCoroutine);
        }
    }
    
    public void StopAdditionalCoroutineTimeLine6()
    {
        if (_additionalCoroutine != null)
        {
            StopCoroutine(_additionalCoroutine);
            _additionalCoroutine = null;
            StopForceCoroutineRadio1();
            SoundEvent.StopForceSound();
            StartCoroutineRadio1VoiceLine(_interactiveStage6.CinematicTouchingSecond(_inputB, _inputC, _radio));
        }
    }

    public void StartCoroutineRadio1VoiceLine(params IEnumerator[] coroutineTimeLine)
    {
        if (currentCinematicCoroutine == null)
        {
            currentCinematicCoroutine = MultiCoroutine(coroutineTimeLine);
            StartCoroutine(currentCinematicCoroutine);
        } else
        {
            routineList.Enqueue(MultiCoroutine(coroutineTimeLine));
        }
    }

    public void StopCoroutineRadio1VoiceLine()
    {
        if (_radio.IsRunningHelpMode())
        {
            StopForceCoroutineRadio1();
            _radio.RoutineHelpMode = null;
        }
        _waitingLoop = false;
        routineList.Clear();
    }

    public void StopForceCoroutineRadio1()
    {
        _waitingLoop = false;
        routineList.Clear();
        if (currentCinematicCoroutine != null)
        {
            StopCoroutine(currentCinematicCoroutine);
        }
        SoundEvent.StopForceSound();
        VoiceEvent.StopForceSound();
        currentCinematicCoroutine = null;
    }

    public bool CurrentCinematicIsRunning()
    {
        return currentCinematicCoroutine != null;
    }
    
    private IEnumerator MultiCoroutine(IEnumerator[] coroutineTimeLine)
    {
        while (_waitingLoop)
        {
            yield return null;
        }

        foreach (IEnumerator routine in coroutineTimeLine) {
            yield return routine;
        }

        if (routineList.Count > 0) {
            yield return routineList.Dequeue();
        }
        currentCinematicCoroutine = null;
    }

    public IEnumerator resetLights() {
        _inputA.LightOff();
        yield return new WaitForSeconds(1);
        _inputB.LightOff();
        yield return new WaitForSeconds(1);
        _inputC.LightOff();
        yield return new WaitForSeconds(1);
        _grpA.LightOff();
        yield return new WaitForSeconds(1);
        _grpA.StopBlinking();
        yield return new WaitForSeconds(1);
        _radio.LightOff();
        yield return new WaitForSeconds(1);
    }

}
