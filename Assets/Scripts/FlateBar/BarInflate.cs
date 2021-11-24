using System;
using System.Collections;
using System.Collections.Generic;
using UnityCaptiv.Sensors;
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
    public float timeMaxReachInSec = 3;
    private float activeTime = 0f;
    
    private bool inflateIsValid = false;
    public Image mask;

    public SensorStatistics respirationStatistics;
    private double lastAverageBreathing;
    
    private double slope = 0;

    private void Start()
    {
        if (respirationStatistics != null)
        {
            lastAverageBreathing = respirationStatistics.Average;
        }
    }

    private void Update()
    {
        if (respirationStatistics == null)
        {
            if (Input.GetKey("up"))
            {
                activeTime += Time.deltaTime;
            }
            else 
            {
                activeTime = 0f;
                inflateIsValid = false;
            }

            GetCurrentFill();
        } else {
            if (respirationStatistics.Average != lastAverageBreathing)
            {
                slope = respirationStatistics.Average - lastAverageBreathing;
            }
            //Debug.Log("slope : " + slope + " // Average : " + respirationStatistics.Average);
            if (slope > StoryManager.Instance._breathingSlopeTolerance && respirationStatistics.Average != 0 || Input.GetKey("up"))
            {
                activeTime += Time.deltaTime;
            }
            else //if (slope <= -1f || respirationStatistics.Average == 0.0f || Input.GetKeyUp("up"))
            {
                activeTime = 0f;
                inflateIsValid = false;
            }

            GetCurrentFill();
            lastAverageBreathing = respirationStatistics.Average;
        }
    }

    void GetCurrentFill()
    {
        float fillAmount = Mathf.Lerp(0, 1, activeTime / timeMaxReachInSec);
        mask.fillAmount = fillAmount;
        if (mask.fillAmount >= 1 && !inflateIsValid) {
            StoryManager.Instance.InteractNegativeAnswer();
            inflateIsValid = true;
        }
    }

}
