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
            Debug.Log("<color=green>GrpA Red</color>");
        }
        else
        {
            Debug.Log("<color=green>GrpA Red</color>");
        }
    }
    public void GrpABlack()
    {
        if (stream != null && stream.IsOpen)
        {
            stream.Write("2");
            Debug.Log("<color=green>GrpA Black</color>");
        }
        else
        {
            Debug.Log("<color=green>GrpA Black</color>");
        }
    }
    public void GrpACrossToBlue()
    {
        if (stream != null && stream.IsOpen)
        {
            stream.Write("3");
            Debug.Log("<color=green>GrpA Blue</color>");
        }
        else
        {
            Debug.Log("<color=green>GrpA Blue</color>");
        }
    }
    public void GrpABlue()
    {
        if (stream != null && stream.IsOpen)
        {
            stream.Write("4");
            Debug.Log("<color=green>GrpA Blue</color>");
        }
        else
        {
            Debug.Log("<color=green>GrpA Blue</color>");
        }
    }
    public void RadioBlue()
    {
        if (stream3 != null && stream3.IsOpen)
        {
            stream3.Write("5");
            Debug.Log("<color=green>Radio Blue</color>");
        }
        else
        {
            //Debug.Log("<color=green>Radio Blue</color>");
        }
    }
    public void RadioGreen()
    {
        if (stream3 != null && stream3.IsOpen)
        {
            stream3.Write("6");
            Debug.Log("<color=green>Radio Green</color>");
        }
        else
        {
            //Debug.Log("<color=green>Radio Green</color>");
        }
    }
    public void RadioRed()
    {
        if (stream3 != null && stream3.IsOpen)
        {
            stream3.Write("7");
            Debug.Log("<color=green>Radio Red</color>");
        }
        else
        {
            //Debug.Log("<color=green>Radio Red</color>");
        }
    }
    public void RadioYellow()
    {
        if (stream3 != null && stream3.IsOpen)
        {
            stream3.Write("8");
            Debug.Log("<color=green>Radio Yellow</color>");
        }
        else
        {
            //Debug.Log("<color=green>Radio Yellow</color>");
        }
    }
    public void RadioBlack()
    {
        if (stream3 != null && stream3.IsOpen)
        {
            stream3.Write("9");
            Debug.Log("<color=green>Radio OFF</color>");
        }
        else
        {
            //Debug.Log("<color=green>Radio OFF</color>");
        }
    }
    public void InputABlue()
    {
        if (stream3 != null && stream3.IsOpen)
        {
            stream3.Write("10");
            Debug.Log("<color=green>InputA Blue</color>");
        }
        else
        {
            Debug.Log("<color=green>InputA Blue</color>");
        }
    }
    public void InputARed()
    {
        if (stream3 != null && stream3.IsOpen)
        {
            stream3.Write("22");
            Debug.Log("<color=green>InputA Red</color>");
        }
        else
        {
            Debug.Log("<color=green>InputA Red</color>");
        }
    }
    public void InputAYellow()
    {
        if (stream3 != null && stream3.IsOpen)
        {
            stream3.Write("11");
            Debug.Log("<color=green>InputA Yellow</color>");
        }
        else
        {
            Debug.Log("<color=green>InputA Yellow</color>");
        }
    }
    public void InputAGreen()
    {
        if (stream3 != null && stream3.IsOpen)
        {
            stream3.Write("12");
            Debug.Log("<color=green>InputA Green</color>");
        }
        else
        {
            Debug.Log("<color=green>InputA Green</color>");
        }
    }
    public void InputABlack()
    {
        if (stream3 != null && stream3.IsOpen)
        {
            stream3.Write("13");
            Debug.Log("<color=green>InputA Black</color>");
        }
        else
        {
            Debug.Log("<color=green>InputA Black</color>");
        }
    }
    public void InputBRed()
    {
        if (stream3 != null && stream3.IsOpen)
        {
            stream3.Write("14");
            Debug.Log("<color=green>InputB Red</color>");
        }
        else
        {
            Debug.Log("<color=green>InputB Red</color>");
        }
    }
    public void InputBYellow()
    {
        if (stream3 != null && stream3.IsOpen)
        {
            stream3.Write("15");
            Debug.Log("<color=green>InputB Yellow</color>");
        }
        else
        {
            Debug.Log("<color=green>InputB Yellow</color>");
        }
    }
    public void InputBGreen()
    {
        if (stream3 != null && stream3.IsOpen)
        {
            stream3.Write("16");
            Debug.Log("<color=green>InputB Green</color>");
        }
        else
        {
            Debug.Log("<color=green>InputB Green</color>");
        }
    }
    public void InputBBlack()
    {
        if (stream3 != null && stream3.IsOpen)
        {
            stream3.Write("17");
            Debug.Log("<color=green>InputB Black</color>");
        }
        else
        {
            Debug.Log("<color=green>InputB Black</color>");
        }
    }
    public void InputCRed()
    {
        if (stream3 != null && stream3.IsOpen)
        {
            stream3.Write("18");
            Debug.Log("<color=green>InputC Red</color>");
        }
        else
        {
            Debug.Log("<color=green>InputC Red</color>");
        }
    }
    public void InputCYellow()
    {
        if (stream3 != null && stream3.IsOpen)
        {
            stream3.Write("19");
            Debug.Log("<color=green>InputC Yellow</color>");
        }
        else
        {
            Debug.Log("<color=green>InputC Yellow</color>");
        }
    }
    public void InputCGreen()
    {
        if (stream3 != null && stream3.IsOpen)
        {
            stream3.Write("20");
            Debug.Log("<color=green>InputC Green</color>");
        }
        else
        {
            Debug.Log("<color=green>InputC Green</color>");
        }
    }
    public void InputCBlack()
    {
        if (stream3 != null && stream3.IsOpen)
        {
            stream3.Write("21");
            Debug.Log("<color=green>InputC Black</color>");
        }
        else
        {
            Debug.Log("<color=green>InputC Black</color>");
        }
        
    }
    public void DoorRed()
    {
        /*if (stream != null && stream.IsOpen)
        {
            stream.Write("22");
        }*/
    }
    public void DoorGreen()
    {
        /*if (stream != null && stream.IsOpen)
        {
            stream.Write("23");
        }*/
    }
    public void DoorBlack()
    {
        /*if (stream != null && stream.IsOpen)
        {
            stream.Write("24");
        }*/
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
        /*if (stream3 != null && stream3.IsOpen)
        {
            stream3.WriteLine(message);
            stream3.BaseStream.Flush();
           // Debug.Log(message);
        }*/
    }

    private void Update()
    {
        //SendMessageToServo(message);
        /*int msg = StoryManager.Instance.StageEnum + 1;
        if (Input.GetKeyDown(KeyCode.P))
        {
            SendMessageToServo((message * 10).ToString());
            Debug.Log(message);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            InputCRed();
        }*/
    }
}
