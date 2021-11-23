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

    private void Start()
    {
        if (respirationStatistics != null)
        {
            lastAverageBreathing = respirationStatistics.Average;
            StartCoroutine(RefreshData());
        }
    }

    private IEnumerator RefreshData()
    {
        while (true)
        {
            //Attente jusqu'au prochain tic de mise à jour
            yield return new WaitForSecondsRealtime(respirationStatistics.TimeWindowSize);

            double slope = respirationStatistics.Average - lastAverageBreathing;
            //Debug.Log("slope deflate : " + slope);
            if (slope >= -1f && slope <= 1f  || Input.GetKey("right"))
            {
                activeTime += respirationStatistics.TimeWindowSize; //Time.deltaTime;
            }
            else if (slope < -1f || slope > 1f)
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
