using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Pour créer les ports de communication Arduino. Création d'un scriptable object sur lequel on renseigne les ports

[CreateAssetMenu(fileName = "PortsArduino", menuName = "Tile Movement Tutorial ep4/PortsArduino", order = 1)]
public class PortsArduino : ScriptableObject
{
	public string port = "COM3";
	public string port3 = "COM9";

	public int baudRate = 9600;
}

