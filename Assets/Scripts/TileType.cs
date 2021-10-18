using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class TileType {

	public string name;
	public GameObject tileVisualPrefab;


	public bool moodEvent = false;
	public int movementCost = 1;
}
