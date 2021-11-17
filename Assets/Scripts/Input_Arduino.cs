using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.IO.Ports;
using System.Runtime.InteropServices.WindowsRuntime;


// Gestion de la communication avec les cartes Arduino.
// L'ordre des streams correspond à l'ordre des ports sur le scriptable object : steam = port; stream2 = port2.


public class Input_Arduino : MonoBehaviour
{
    private SerialPort stream;
    private SerialPort stream2;
    private SerialPort stream3;

    [Range(0, 9)]
    public int message;
    private float lastSend = 0;

    public PortsArduino portsArduino;


    public static Input_Arduino singletonInstance = null;

    public static Input_Arduino GetInstance()
    {
        if (singletonInstance)
        {
            return singletonInstance;
        }
        else
        {
            return null;
        }
    }


    //************** Ouverture et fermeture des ports série ****************//

    void OnEnable()
    {
        try
        {
            stream = new SerialPort(portsArduino.port, portsArduino.baudRate);
            stream.ReadTimeout = 100;
            stream.Open();
        }
        catch (IOException e)
        {
            Console.WriteLine(e + portsArduino.port);
            throw;
        }
        
        try
        {
            stream2 = new SerialPort(portsArduino.port2, portsArduino.baudRate);
            stream2.ReadTimeout = 100;
            stream2.Open();
        }
        catch (IOException e)
        {
            Console.WriteLine(e + portsArduino.port2);
            throw;
        }

        try
        {
            stream3 = new SerialPort(portsArduino.port3, portsArduino.baudRate);
            stream3.ReadTimeout = 100;
            stream3.Open();
        }
        catch (IOException e)
        {
            Console.WriteLine(e + portsArduino.port3);
            throw;
        }
            
        Valve2_Off();
        Valve1_Off();

        Input_Arduino.singletonInstance = this;
    }

    private void OnDisable()
    {
        SendMessageToServo("0");
        if (stream2 != null && stream.IsOpen) stream.Close();
        if (stream2 != null && stream2.IsOpen) stream2.Close();
        if (stream3 != null && stream3.IsOpen) stream3.Close();
    }


//***************** BREATHE ****************//

    public void Valve1_On()
    {
        if (stream2 != null && stream2.IsOpen)
        {
            stream2.Write("0");
        }
    }
    public void Valve1_Off()
    {
        if (stream2 != null && stream2.IsOpen)
        {
            stream2.Write("1");
        }
    }
    public void Valve2_On()
    {
        if (stream2 != null && stream2.IsOpen)
        {
            stream2.Write("2");
        }
    }
    public void Valve2_Off()
    {
        if (stream2 != null && stream2.IsOpen)
        {
            stream2.Write("3");
        }
    }


//******************* MOOD *****************//
// Communication Arduino du changement de couleur en fonction du mood
    public void MoodNeutral()
    {
        if (stream != null && stream.IsOpen)
        {
            stream.Write("4");
        }
    }
    public void MoodRed()
    {
        if (stream != null && stream.IsOpen)
        {
            stream.Write("5");
        }
    }
    public void MoodBlue()
    {
        if (stream != null && stream.IsOpen)
        {
            stream.Write("6");
        }
    }
    public void MoodGreen()
    {
        if (stream != null && stream.IsOpen)
        {
            stream.Write("7");
        }
    }
    public void MoodBrown()
    {
        if (stream != null && stream.IsOpen)
        {
            stream.Write("8");
        }
    }

// Changement de mood dans le jeu
    public void UpdateState(TileType currentTile)
    {
        if (currentTile == null)
        {
            Debug.Log("currentTile is null!!!");
            return;
        }

        switch(currentTile.movementCost)
        {
            case 0:
                MoodNeutral();
                Debug.Log("MoodNeutral");
                break;
            case 1:
                MoodRed();              
                Debug.Log("MoodRed");
                break;
            case 2:
                MoodBlue();
                Debug.Log("MoodBlue");
                break;
            case 3:
                MoodGreen();
                Debug.Log("MoodGreen");
                break;
            case 4:
                MoodBrown();
                Debug.Log("MoodBrown");
                break;
            default:
                MoodNeutral();
                break;
        }
    }

    public void SendMessageToServo(string message)
    {

        if (stream3 != null && stream3.IsOpen)
        {
            stream3.WriteLine(message);
            stream3.BaseStream.Flush();
           // Debug.Log(message);

        }
    }

    private void Update()
    {
        //SendMessageToServo(message);
        int msg = StoryManager.Instance.StageEnum + 1;
        if (Input.GetKeyDown(KeyCode.P))
        {
            SendMessageToServo((message * 10).ToString());
            Debug.Log(message);
        }
    }
}
