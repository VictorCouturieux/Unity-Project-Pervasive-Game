using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


// L'expiration. La barre se remplit quand le joueur expire. 
// pour le moment la respiration est simulée par les flèches haut et bas du clavier.
// UI et fonction pour la commande Arduino.

// /!\ ATTENTION ne marche pas s'il n'y a pas 2 cartes Arduino branchées et connectées aux bons ports séries. Pas pratique, déso je me suis pas penchée sur le prb.

[ExecuteInEditMode()]
public class BarInflate : MonoBehaviour
{
    public float timeMaxReachInSec = 6;
    private float activeTime = 0f;
    
    public bool inflateIsValid = false;
    public bool apneaIsValid = false;
    public Image mask;
//    public Input_Arduino inputArduinoInstance;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("up"))
        {
            activeTime += Time.deltaTime;
//            if (inputArduinoInstance != null)
//            {
//                inputArduinoInstance.Valve1_On();
//            }
        }
        else 
        { 
            activeTime = 0f;
            inflateIsValid = false;
            apneaIsValid = false;
//            if (inputArduinoInstance != null)
//            {
//                inputArduinoInstance.Valve1_Off();
//            }
        }

        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        float fillAmount = Mathf.Lerp(0, 1, activeTime / timeMaxReachInSec);
        mask.fillAmount = fillAmount;
        if (mask.fillAmount >= 0.5 && !inflateIsValid) {
            StoryManager.Instance.InteractNegativeAnswer();
            inflateIsValid = true;
        }
        if (mask.fillAmount >= 1 && !apneaIsValid) {
            StoryManager.Instance.InteractApnea();
            apneaIsValid = true;
        }
    }

}
