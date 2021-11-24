using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLineTool : MonoBehaviour
{
    private InputA _inputA;
    private InputB _inputB;
    private InputC _inputC;

    private Door _door;
    private GrpA _grpA;
    private Radio _radio;

    public int ShowStageNUM
    {
        get { return StoryManager.Instance.ShowStageNUM; }
        set { StoryManager.Instance.ShowStageNUM = value; }
    }
    
    private void Awake() {
        _inputA = FindObjectOfType<InputA>();
        _inputB = FindObjectOfType<InputB>();
        _inputC = FindObjectOfType<InputC>();
        
        _door = FindObjectOfType<Door>();
        _grpA = FindObjectOfType<GrpA>();
        _radio = FindObjectOfType<Radio>();
    }

    private void ResetStage()
    {
        StoryManager.Instance.StopForceCoroutineRadio1();
        _radio.StopCurrantHelpMode();
        StoryManager.Instance.resetLights();
        StoryManager.Instance.StageEnum = -1;
        StoryManager.Instance._interactiveStage6.CanControl = false;
        StoryManager.Instance.SoundEvent.StopForceSound();
        StoryManager.Instance.VoiceEvent.StopForceSound();
    }

    private void storyLineLoading_0()
    {
        ResetStage();
        StoryManager.Instance.StartCoroutineRadio1VoiceLine(
            StoryManager.Instance._startRadioNoise.StartingGame(_grpA, _door, _radio));
    }
    
    private void storyLineLoading_1()
    {
        ResetStage();
        _radio.LightToBlue();
        StoryManager.Instance.StartCoroutineRadio1VoiceLine(
            StoryManager.Instance._interactiveStage1.CinematicStageIn(_grpA), 
            StoryManager.Instance._interactiveStage1.CinematicLoop() );
    }
    
    private void storyLineLoading_2()
    {
        ResetStage();
        _grpA.LightToRed();
        _radio.LightToBlue();
        StoryManager.Instance.StartCoroutineRadio1VoiceLine(
            StoryManager.Instance._interactiveStage2.CinematicStageIn(), 
            StoryManager.Instance._interactiveStage2.CinematicLoop());
        
    }
    
    private void storyLineLoading_3()
    {
        ResetStage();
        _grpA.LightToRed();
        _radio.LightToBlue();
        StoryManager.Instance.StartCoroutineRadio1VoiceLine( 
            StoryManager.Instance._interactiveStage3.CinematicStageIn(_inputA, _radio));
    }
    
    private void storyLineLoading_4()
    {
        ResetStage();
        _grpA.LightToRed();
        _radio.LightToBlue();
        StoryManager.Instance.StartCoroutineRadio1VoiceLine(
            StoryManager.Instance._interactiveStage4.CinematicStageIn(_inputA, _radio), 
            StoryManager.Instance._interactiveStage4.CinematicTimeLapsOut(_grpA));
    }
    
    private void storyLineLoading_5()
    {
        ResetStage();
        _radio.LightToBlue();
        StoryManager.Instance.StartCoroutineRadio1VoiceLine(StoryManager.Instance._interactiveStage5
            .CinematicStageIn(_grpA));
    }
    
    private void storyLineLoading_6()
    {
        ResetStage();
        _grpA.LightToRed();
        _radio.LightToBlue();
        _inputA.LightToBlue();
        StoryManager.Instance._interactiveStage6.IsFirstLoopLevel6 = true;
        StoryManager.Instance.StartCoroutineRadio1VoiceLine(
            StoryManager.Instance._interactiveStage5.CinematicStagePosOut(), 
            StoryManager.Instance._interactiveStage6.CinematicStageIn(_inputA, _inputB, _inputC, _radio));    
    }
    
    private void storyLineLoading_7()
    {
        ResetStage();
        _radio.LightToBlue();
        _inputA.LightToGreen();
        _inputB.LightToGreen();
        _inputC.LightToGreen();
        StoryManager.Instance.StartCoroutineRadio1VoiceLine(
            StoryManager.Instance._interactiveStage6.CinematicStageOut(_inputA, _radio, _door, _grpA), 
            StoryManager.Instance._interactiveStage7.CinematicStageIn(_door, _radio));
    }
      
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0))
        {
            Debug.Log("0");
            ShowStageNUM = 0;
            storyLineLoading_0();
        } 
        else if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            Debug.Log("1");
            ShowStageNUM = 1;
            storyLineLoading_1();
        } 
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            Debug.Log("2");
            ShowStageNUM = 2;
            storyLineLoading_2();
        } 
        else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            Debug.Log("3");
            ShowStageNUM = 3;
            storyLineLoading_3();
        } 
        else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            Debug.Log("4");
            ShowStageNUM = 4;
            storyLineLoading_4();
        } 
        else if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
        {
            Debug.Log("5");
            ShowStageNUM = 5;
            storyLineLoading_5();
        } 
        else if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
        {
            Debug.Log("6");
            ShowStageNUM = 6;
            storyLineLoading_6();
        } 
        else if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))
        {
            Debug.Log("7");
            ShowStageNUM = 7;
            storyLineLoading_7();
        }
    }
    
}
