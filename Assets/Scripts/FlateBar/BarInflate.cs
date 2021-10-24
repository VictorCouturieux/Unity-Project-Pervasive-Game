using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// L'expiration. La barre se remplit quand le joueur expire. 
// pour le moment la respiration est simulée par les flèches haut et bas du clavier.
// UI et fonction pour la commande Arduino.

// /!\ ATTENTION ne marche pas s'il n'y a pas 2 cartes Arduino branchées et connectées aux bons ports séries. Pas pratique, déso je me suis pas penchée sur le prb.

[ExecuteInEditMode()]
public class BarInflate : MonoBehaviour
{
    public int maximum;
    public int currentInflate;
    public bool inflateIsValid = false;
    public Image mask;
//    public Input_Arduino inputArduinoInstance;

    // Start is called before the first frame update
    void Start()
    {
        currentInflate = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("up"))
        {
            currentInflate++;
//            if (inputArduinoInstance != null)
//            {
//                inputArduinoInstance.Valve1_On();
//            }
        }
        else 
        { 
            currentInflate = 0;
            inflateIsValid = false;
//            if (inputArduinoInstance != null)
//            {
//                inputArduinoInstance.Valve1_Off();
//            }
        }

        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        float fillAmount = (float)currentInflate / (float)maximum;
        mask.fillAmount = fillAmount;
        if (mask.fillAmount >= 1 && !inflateIsValid) {
            inflateIsValid = true;
            StoryManager.Instance.RadioInteractNegativeAnswer();
        }
    }

}
