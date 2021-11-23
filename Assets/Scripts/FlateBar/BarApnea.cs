using System;
using System.Collections;
using System.Collections.Generic;
using UnityCaptiv.Sensors;
using UnityEngine;
using UnityEngine.UI;

public class BarApnea : MonoBehaviour
{
    public float timeMaxReachInSec = 6;
    private float activeTime = 0f;
    
    private bool apneaIsValid = false;
    public Image mask;

    public SensorStatistics respirationStatistics;
    private double lastAverageBreathing;

    private double slope = 0;

    private void Start()
    {
        if (respirationStatistics != null)
        {
            lastAverageBreathing = respirationStatistics.Average;
            // StartCoroutine(RefreshData());
        }
    }

    private void Update()
    {
        if (respirationStatistics == null)
        {
            if (Input.GetKey("right"))
            {
                activeTime += Time.deltaTime;
            }
            else
            {
                activeTime = 0f;
                apneaIsValid = false;
            }
            
            GetCurrentFill();
        }
        else
        {
            if (respirationStatistics.Average != lastAverageBreathing)
            {
                slope = respirationStatistics.Average - lastAverageBreathing;
            }
            // Debug.Log("slope : " + slope + " // Average : " + respirationStatistics.Average);
            if (slope >= -1f && slope <= 1f && respirationStatistics.Average != 0 || Input.GetKey("right"))
            {
                activeTime += Time.deltaTime;
            }
            else if (slope < -1f || slope > 1f || respirationStatistics.Average == 0.0f || Input.GetKeyUp("right"))
            {
                activeTime = 0f;
                apneaIsValid = false;
            }
            GetCurrentFill();

            lastAverageBreathing = respirationStatistics.Average;
        }
    }

    private IEnumerator RefreshData()
    {
        while (true)
        {
            //Attente jusqu'au prochain tic de mise à jour
            yield return new WaitForSecondsRealtime(respirationStatistics.TimeWindowSize);

            slope = respirationStatistics.Average - lastAverageBreathing;
            // Debug.Log("slope : " + slope + " // Average : " + respirationStatistics.Average);
            if (slope >= -1f && slope <= 1f && respirationStatistics.Average != 0 || Input.GetKey("right"))
            {
                activeTime += respirationStatistics.TimeWindowSize; //Time.deltaTime;
            }
            else if (slope < -1f || slope > 1f || respirationStatistics.Average == 0.0f)
            {
                activeTime = 0f;
                apneaIsValid = false;
            }
            GetCurrentFill();

            lastAverageBreathing = respirationStatistics.Average;
        }
    }

    void GetCurrentFill()
    {
        mask.fillAmount = Mathf.Lerp(0, 1, activeTime / timeMaxReachInSec);
        if (mask.fillAmount >= 1 && !apneaIsValid) {
            StoryManager.Instance.InteractApnea();
            activeTime = 0f;
            apneaIsValid = true;
        }
    }
}
