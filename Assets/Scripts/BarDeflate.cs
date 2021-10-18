using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// L'inspiration. La barre se remplit quand le joueur inspire. 
// pour le moment la respiration est simulée par les flèches haut et bas du clavier.
// UI et fonction pour la commande Arduino.

// /!\ ATTENTION ne marche pas s'il n'y a pas 2 cartes Arduino branchées et connectées aux bons ports séries. Pas pratique, déso je me suis pas penchée sur le prb.


[ExecuteInEditMode()]
public class BarDeflate : MonoBehaviour
{

    public int maximum;
    public int currentDeflate;
    public Image mask;
    public Input_Arduino inputArduinoInstance;

    void Start()
    {
        currentDeflate = 0;

    }

    void Update()
    {

        if (Input.GetKey("down"))
        {
            currentDeflate++; 
            if (inputArduinoInstance)
            {
                inputArduinoInstance.Valve2_On();
            }
        }
        else
        {
            currentDeflate = 0;
            if (inputArduinoInstance)
            {
                inputArduinoInstance.Valve2_Off();
            }
        }

        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        float fillAmount = (float)currentDeflate / (float)maximum;
        mask.fillAmount = fillAmount;
    }

}
