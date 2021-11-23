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
    public float timeMaxReachInSec = 6;
    private float activeTime = 0f;
    
    public bool inflateIsValid = false;
    public bool apneaIsValid = false;
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
    
    void Update()
    {
        // if (Input.GetKey("up"))
        // {
        //     activeTime += Time.deltaTime;
        // }
        // else 
        // { 
        //     activeTime = 0f;
        //     inflateIsValid = false;
        //     apneaIsValid = false;
        // }
        //
        // GetCurrentFill();
    }
    
    
    private IEnumerator RefreshData()
    {
        while (true)
        {
            //Attente jusqu'au prochain tic de mise à jour
            double slope = respirationStatistics.Average - lastAmplBreathing;
            //Debug.Log("slope : " + slope + " // Average : " + respirationStatistics.Average);
            if (slope > 0.5f || Input.GetKey("up"))
            {
                activeTime += Time.deltaTime; //respirationStatistics.TimeWindowSize;//Time.deltaTime;
            }
            else if (slope <= -0.5f)
            {
                activeTime = 0f;
                inflateIsValid = false;
                apneaIsValid = false;
            }

            GetCurrentFill();
            lastAmplBreathing = respirationStatistics.Average;
        }
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
            activeTime = 0f;
            apneaIsValid = true;
        }
    }

}
