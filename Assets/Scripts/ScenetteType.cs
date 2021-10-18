using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Pour faire des types de scénettes. Pour plus tard gérer les conditions de victoire de chacune peut-être.

[System.Serializable]
public class ScenetteType
{

	public string name;
	public Image image;
	public int colorState = 0;
	public bool completed = false;
}
