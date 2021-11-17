using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Arduino : MonoBehaviour
{
    private Input_Arduino _input_Arduino;

    private static Event_Arduino m_instance;
    public static Event_Arduino Instance
    {
        get
        {
            if (!m_instance)
            {
                m_instance = new Event_Arduino();
            }
            return m_instance;
        }
    }

    private void Awake()
    {
        if (m_instance != null && m_instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            m_instance = this;
        }

        _input_Arduino = FindObjectOfType<Input_Arduino>();
    }

    public void SendEventArduino()
    {
        int msg = StoryManager.Instance.StageEnum + 1;
        if (_input_Arduino != null)
        {
            _input_Arduino.SendMessageToServo((msg * 10).ToString());
            //Debug.Log(msg);
        }
    }
}
