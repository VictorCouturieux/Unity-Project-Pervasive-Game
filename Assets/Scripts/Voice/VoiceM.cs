using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class VoiceM : MonoBehaviour
{
    public static AudioMixer _audioMixer;

    private Mood currantMood = Mood.Neutral;
    private static float moveToMood = 2;

    private IEnumerator currantRoutine;

    private SensorAnalysis _sensorAnalysis;

    private VoiceEffect[] exposedParamList = 
    {
        new VoiceEffect ("EchoDelay_Dia", 1, 1255),
        new VoiceEffect ("EchoDrymix_Dia", 0, 1),
        
        new VoiceEffect ("FlangeDrymix_Dia", 0, 0.45f),
        new VoiceEffect ("FlangeDepth_Dia", 0.01f, 0.40f),
        new VoiceEffect ("FlangeRate_Dia", 0, 8),
        
        new VoiceEffect ("PitchShifterPitch_Dia", 1, 0.92f),
        new VoiceEffect ("PitchShifterFFTSize_Dia", 256, 1024),
        new VoiceEffect ("PitchShifterOverlap_Dia", 1, 4),
        
        new VoiceEffect ("SFXReverbRoom_Dia", -10000, 0),
        new VoiceEffect ("SFXReverbDecayTime_Dia", 1, 7),
    };

    private void Awake()
    {
        _sensorAnalysis = FindObjectOfType<SensorAnalysis>();
    }

    private void Start()
    {
        _sensorAnalysis.OnZoneChange += ChangeZone;
    }
    
    private void ChangeZone(int newZone)
    {
        // Debug.Log("Zone : " + newZone);
        // if (newZone == 0 && currantMood != Mood.Neutral)
        // {
        //     ToMood(false);
        //     currantMood = Mood.Neutral;
        // } else if (newZone == 1 && currantMood != Mood.Stat1)
        // {
        //     ToMood(false);
        //     currantMood = Mood.Stat1;
        //     ToMood(true);
        // } else if (newZone == 2 && currantMood != Mood.Stat2)
        // {
        //     ToMood(false);
        //     currantMood = Mood.Stat2;
        //     ToMood(true);
        // } else if (newZone == 3 && currantMood != Mood.Stat3)
        // {
        //     ToMood(false);
        //     currantMood = Mood.Stat3;
        //     ToMood(true);
        // } else if (newZone == 4 && currantMood != Mood.Stat4)
        // {
        //     ToMood(false);
        //     currantMood = Mood.Stat4;
        //     ToMood(true);
        // }
    }

    public void ToMood(bool isStarting)
    {
        switch (currantMood)
        {
            case Mood.Stat1 :
                StartRoutine(exposedParamList[8], isStarting);
                StartRoutine(exposedParamList[9], isStarting);
                break;
            case Mood.Stat2 :
                StartRoutine(exposedParamList[5], isStarting);
                StartRoutine(exposedParamList[6], isStarting);
                StartRoutine(exposedParamList[7], isStarting);
                break;
            case Mood.Stat3 :
                StartRoutine(exposedParamList[2], isStarting);
                StartRoutine(exposedParamList[3], isStarting);
                StartRoutine(exposedParamList[4], isStarting);
                break;
            case Mood.Stat4 :
                StartRoutine(exposedParamList[0], isStarting);
                StartRoutine(exposedParamList[1], isStarting);
                break;
        }
    }
    
    public void StartRoutine(VoiceEffect voiceEffect, bool isValOn)
    {
        StopRoutine(voiceEffect);
        if (isValOn)
        {
            voiceEffect.Routine = voiceEffect.RoutineVef(voiceEffect.ValOn);
        }
        else
        {
            voiceEffect.Routine = voiceEffect.RoutineVef(voiceEffect.ValOff);
        }
        voiceEffect.IsValOn = isValOn;
        StartCoroutine(voiceEffect.Routine);
    }
        
    public void StopRoutine(VoiceEffect voiceEffect)
    {
        if (voiceEffect.Routine != null)
        {
            StartCoroutine(voiceEffect.Routine);
            voiceEffect.Routine = null;
        }
    }

    public struct VoiceEffect
    {
        public String Name { get; }
        public float ValOff { get; }
        public float ValOn { get; }
        public bool IsValOn { get; set; }
        public IEnumerator Routine { get; set; }

        public VoiceEffect(String name, float valOff, float valOn)
        {
            Name = name;
            ValOff = valOff;
            ValOn = valOn;
            IsValOn = false;
            Routine = null;
        }

        public IEnumerator RoutineVef(float targetVal)
        {
            yield return FadeMixerGroup.StartParamsFade(_audioMixer, Name, moveToMood, targetVal);
            Routine = null;
        }
    }
    
    public enum Mood
    {
        Neutral,
        Stat1,
        Stat2,
        Stat3,
        Stat4
    }
    
    //simple Test ZONE
        
    public bool isNeutralStat() {
        return Input.GetKey(KeyCode.P);
    }
    
    public bool isStat1() {
        return Input.GetKeyDown(KeyCode.Y);
    }
    
    public bool isStat2() {
        return Input.GetKey(KeyCode.U);
    }
    
    public bool isStat3() {
        return Input.GetKey(KeyCode.I);
    }
    
    public bool isStat4() {
        return Input.GetKey(KeyCode.O);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (isNeutralStat() && currantMood != Mood.Neutral)
        {
            ToMood(false);
            currantMood = Mood.Neutral;
        } else if (isStat1() && currantMood != Mood.Stat1)
        {
            ToMood(false);
            currantMood = Mood.Stat1;
            ToMood(true);
        } else if (isStat2() && currantMood != Mood.Stat2)
        {
            ToMood(false);
            currantMood = Mood.Stat2;
            ToMood(true);
        } else if (isStat3() && currantMood != Mood.Stat3)
        {
            ToMood(false);
            currantMood = Mood.Stat3;
            ToMood(true);
        } else if (isStat4() && currantMood != Mood.Stat4)
        {
            ToMood(false);
            currantMood = Mood.Stat4;
            ToMood(true);
        }
    }

}
