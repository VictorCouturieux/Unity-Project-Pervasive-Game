using UnityEngine;
using System.Collections;
using System.Collections.Generic;


// Le pion se déplace à l'endroit où l'on clique sur la grille des mood.
// Plus tard, son déplacement se fera par les données des capteurs émotionnels. Intégration à faire.

public class ClickableTile : MonoBehaviour {

	public int tileX;
	public int tileY;
	public TileMap map;

	void OnMouseUp() {
		//Debug.Log ("Click!");

		map.MoveSelectedUnitTo(tileX, tileY);
	}

}
