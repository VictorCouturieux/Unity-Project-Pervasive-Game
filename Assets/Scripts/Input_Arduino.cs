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
    private SerialPort stream3;

    [Range(0, 9)]
    public int message;

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

        stream3 = new SerialPort(portsArduino.port3, portsArduino.baudRate);
        stream3.ReadTimeout = 100;
        stream3.Open();

        Input_Arduino.singletonInstance = this;
    }

    private void OnDisable()
    {
        SendMessageToServo("0");
        if (stream.IsOpen) stream.Close();
        if (stream3.IsOpen) stream3.Close();
    }


//******************* MOOD *****************//
// Communication Arduino du changement de couleur en fonction du mood
    public void GrpARed()
    {
        if (stream != null && stream.IsOpen)
        {
            stream.Write("1");
        }
    }
    public void GrpABlack()
    {
        if (stream != null && stream.IsOpen)
        {
            stream.Write("2");
        }
    }
    public void GrpACrossToBlue()
    {
        if (stream != null && stream.IsOpen)
        {
            stream.Write("3");
        }
    }
    public void GrpABlue()
    {
        if (stream != null && stream.IsOpen)
        {
            stream.Write("4");
        }
    }
    public void RadioBlue()
    {
        if (stream != null && stream.IsOpen)
        {
            stream.Write("5");
        }
    }
    public void RadioGreen()
    {
        if (stream != null && stream.IsOpen)
        {
            stream.Write("6");
        }
    }
    public void RadioRed()
    {
        if (stream != null && stream.IsOpen)
        {
            stream.Write("7");
        }
    }
    public void RadioYellow()
    {
        if (stream != null && stream.IsOpen)
        {
            stream.Write("8");
        }
    }
    public void RadioBlack()
    {
        if (stream != null && stream.IsOpen)
        {
            stream.Write("9");
        }
    }
    public void InputABlue()
    {
        if (stream != null && stream.IsOpen)
        {
            stream.Write("10");
        }
    }
    public void InputAYellow()
    {
        if (stream != null && stream.IsOpen)
        {
            stream.Write("11");
        }
    }
    public void InputAGreen()
    {
        if (stream != null && stream.IsOpen)
        {
            stream.Write("12");
        }
    }
    public void InputABlack()
    {
        if (stream != null && stream.IsOpen)
        {
            stream.Write("13");
        }
    }
    public void InputBRed()
    {
        if (stream != null && stream.IsOpen)
        {
            stream.Write("14");
        }
    }
    public void InputBYellow()
    {
        if (stream != null && stream.IsOpen)
        {
            stream.Write("15");
        }
    }
    public void InputBGreen()
    {
        if (stream != null && stream.IsOpen)
        {
            stream.Write("16");
        }
    }
    public void InputBBlack()
    {
        if (stream != null && stream.IsOpen)
        {
            stream.Write("17");
        }
    }
    public void InputCRed()
    {
        if (stream != null && stream.IsOpen)
        {
            stream.Write("18");
        }
    }
    public void InputCYellow()
    {
        if (stream != null && stream.IsOpen)
        {
            stream.Write("19");
        }
    }
    public void InputCGreen()
    {
        if (stream != null && stream.IsOpen)
        {
            stream.Write("20");
        }
    }
    public void InputCBlack()
    {
        if (stream != null && stream.IsOpen)
        {
            stream.Write("21");
        }
    }
    public void DoorRed()
    {
        if (stream != null && stream.IsOpen)
        {
            stream.Write("22");
        }
    }
    public void DoorGreen()
    {
        if (stream != null && stream.IsOpen)
        {
            stream.Write("23");
        }
    }
    public void DoorBlack()
    {
        if (stream != null && stream.IsOpen)
        {
            stream.Write("24");
        }
    }

    // Changement de mood dans le jeu
    /*public void UpdateState(TileType currentTile)
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
    }*/

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
