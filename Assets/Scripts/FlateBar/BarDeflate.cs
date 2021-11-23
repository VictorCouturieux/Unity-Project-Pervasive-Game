using System;
using System.Collections;
using System.Collections.Generic;
using UnityCaptiv.Sensors;
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
    public Image mask;

    public SensorStatistics respirationStatistics;
    private double lastAmplBreathing;

    private void Start()
    {
        if (respirationStatistics != null)
        {
            lastAmplBreathing = respirationStatistics.Average;
            StartCoroutine(RefreshData());
        }
    }

    private IEnumerator RefreshData()
    {
        while (true)
        {
            //Attente jusqu'au prochain tic de mise à jour
            yield return new WaitForSecondsRealtime(respirationStatistics.TimeWindowSize);

            double slope = respirationStatistics.Average - lastAmplBreathing;
            //Debug.Log("slope deflate : " + slope);
            if (slope < -1f || Input.GetKey("down"))
            {
                activeTime += respirationStatistics.TimeWindowSize; //Time.deltaTime;
            }
            else if (slope >= 1f)
            {
                activeTime = 0f;
                deflateIsValid = false;
            }
            GetCurrentFill();

            lastAmplBreathing = respirationStatistics.Average;
        }
    }

    void GetCurrentFill()
    {
        mask.fillAmount = Mathf.Lerp(0, 1, activeTime / timeMaxReachInSec);
        if (mask.fillAmount >= 1 && !deflateIsValid) {
            StoryManager.Instance.InteractPositiveAnswer();
            activeTime = 0f;
            deflateIsValid = true;
        }
    }

}
