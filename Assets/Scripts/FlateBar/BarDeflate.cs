using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// L'inspiration. La barre se remplit quand le joueur inspire. 
// pour le moment la respiration est simul�e par les fl�ches haut et bas du clavier.
// UI et fonction pour la commande Arduino.

// /!\ ATTENTION ne marche pas s'il n'y a pas 2 cartes Arduino branch�es et connect�es aux bons ports s�ries. Pas pratique, d�so je me suis pas pench�e sur le prb.


[ExecuteInEditMode()]
public class BarDeflate : MonoBehaviour
{
    public float timeMaxReachInSec = 6;
    private float activeTime = 0f;
    
    private bool deflateIsValid = false;
    public bool apneaIsValid = false;
    public Image mask;
//    public Input_Arduino inputArduinoInstance;

    void Update()
    {

        if (Input.GetKey("down"))
        {
            activeTime += Time.deltaTime;
//            if (inputArduinoInstance)
//            {
//                inputArduinoInstance.Valve2_On();
//            }
        }
        else {
            activeTime = 0f;
            deflateIsValid = false;
            apneaIsValid = false;
//            if (inputArduinoInstance)
//            {
//                inputArduinoInstance.Valve2_Off();
//            }
        }

        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        mask.fillAmount = Mathf.Lerp(0, 1, activeTime / timeMaxReachInSec);
        if (mask.fillAmount >= 0.5 && !deflateIsValid) {
            StoryManager.Instance.InteractPositiveAnswer();
            deflateIsValid = true;
        }
        if (mask.fillAmount >= 1 && !apneaIsValid) {
            StoryManager.Instance.InteractApnea();
            apneaIsValid = true;
        }
    }

}
