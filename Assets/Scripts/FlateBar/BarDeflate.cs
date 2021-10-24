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

    public int maximum;
    public int currentDeflate;
    public bool deflateIsValid = false;
    public Image mask;
//    public Input_Arduino inputArduinoInstance;

    void Start()
    {
        currentDeflate = 0;

    }

    void Update()
    {

        if (Input.GetKey("down"))
        {
            currentDeflate++; 
//            if (inputArduinoInstance)
//            {
//                inputArduinoInstance.Valve2_On();
//            }
        }
        else
        {
            currentDeflate = 0;
            deflateIsValid = false;
//            if (inputArduinoInstance)
//            {
//                inputArduinoInstance.Valve2_Off();
//            }
        }

        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        float fillAmount = (float)currentDeflate / (float)maximum;
        mask.fillAmount = fillAmount;
        if (mask.fillAmount >= 1 && !deflateIsValid) {
            deflateIsValid = true;
            StoryManager.Instance.RadioInteractPositiveAnswer();
        }
    }

}
