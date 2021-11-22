using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SensorAnalysis : MonoBehaviour
{
    [Range(0.5f, 10.0f)]
    [Tooltip("The time (in seconds) between updates.")]
    public float refreshFrequency = 1.0f;

    public UnityCaptiv.Sensors.SensorStatistics gsrStatistics = null;
    public UnityCaptiv.Sensors.SensorStatistics respirationStatistics = null;
    public UnityCaptiv.Sensors.SensorStatistics temperatureStatistics = null;
    public UnityCaptiv.Sensors.SensorStatistics cfmStatistics = null;

    //Données du capteur GSR
    private double gsrAmpRef = 0.0;
    public double GSR_Amp_ref { get => gsrAmpRef; }     //Amplitude de référence des valeurs du capteur GSR.

    private double gsrAmp = 0.0;
    public double GSR_amp { get => gsrAmp; }            //Amplitude des valeurs du capteur GSR sur les T1 dernières secondes.

    private double gsrAmpPrev = 0.0;
    public double GSR_amp_prev { get => gsrAmpPrev; }   //Amplitude des valeurs du capteur GSR sur les -T2 à -(T1+T2) dernières secondes.

    private double gsrAvgRef = 0.0;
    public double GSR_avg_ref { get => gsrAvgRef; }     //Valeur moyenne de référence du capteur GSR.

    private double gsrAvg = 0.0;
    public double GSR_avg { get => gsrAvg; }            //Valeur moyenne des valeurs du capteur GSR sur les T1 dernières secondes.

    private double gsrAvgPrev = 0.0;
    public double GSR_avg_prev { get => gsrAvgPrev; }   //Valeur moyenne des valeurs du capteur GSR sur les -T2 à -(T1+T2) dernières secondes.


    //Données du capteur Respiration
    private double respAmpRef = 0.0;
    public double RESP_amp_ref { get => respAmpRef; }   //Amplitude de référence des valeurs du capteur Respiration.

    private double respAmp = 0.0;
    public double RESP_amp { get => respAmp; }          //Amplitude des valeurs du capteur Respiration sur les T1 dernières secondes.

    private double respAmpPrev = 0.0;
    public double RESP_amp_prev { get => respAmpPrev; } //Amplitude des valeurs du capteur Respiration sur les -T2 à -(T1+T2) dernières secondes.

    //Données du capteur CFM
    private double cfmAvgRef = 0.0;
    public double CFM_avg_ref { get => cfmAvgRef; }     //Valeur moyenne de référence du capteur CFM.

    private double cfmAvg = 0.0;
    public double CFM_avg { get => cfmAvg; }            //Valeur moyenne des valeurs du capteur CFM sur les T1 dernières secondes.

    private double cfmAvgPrev = 0.0;
    public double CFM_avg_prev { get => cfmAvgPrev; }   //Valeur moyenne des valeurs du capteur CFM sur les -T2 à -(T1+T2) dernières secondes.

    //Données du capteur Température
    private double tempAvgRef = 0.0;
    public double TEMP_avg_ref { get => tempAvgRef; }   //Valeur moyenne de référence du capteur Température.

    private double tempAvg = 0.0;
    public double TEMP_avg { get => tempAvg; }          //Valeur moyenne des valeurs du capteur Température sur les T1 dernières secondes.

    private double tempAvgPrev = 0.0;
    public double TEMP_avg_prev { get => tempAvgPrev; } //Valeur moyenne des valeurs du capteur Température sur les -T2 à -(T1+T2) dernières secondes.

    //Zone actuelle définie par les données des capteurs
    // 0 : Zone Neutre
    // 1 : Zone 1
    // 2 : Zone 2
    // 3 : Zone 3
    // 4 : Zone 4
    //La zone neutre est la zone par défaut.
    private int m_zone = 0;
    private int zone = 0;
    public double Zone { get => zone; }
    public delegate void OnVariableChangeDelegate(int zone);
    public event OnVariableChangeDelegate OnZoneChange;

    private void Start()
    {
        StartCoroutine(RefreshData());
    }

    private void Update()
    {
        if (zone != m_zone && OnZoneChange != null)
        {
            m_zone = zone;
            OnZoneChange(zone);
        }
    }

    private IEnumerator RefreshData()
    {
        while (true)
        {
            //Attente jusqu'au prochain tic de mise à jour
            yield return new WaitForSecondsRealtime(refreshFrequency);
            
            //Récupération des dernières données des capteurs
            gsrAmpRef = gsrStatistics.AmplitudeRef;
            gsrAmpPrev = gsrAmp;
            gsrAmp = gsrStatistics.Amplitude;

            gsrAvgRef = gsrStatistics.AverageRef;
            gsrAvgPrev = gsrAvg;
            gsrAvg = gsrStatistics.Average;

            respAmpRef = respirationStatistics.AmplitudeRef;
            respAmpPrev = respAmp;
            respAmp = respirationStatistics.Amplitude;

            cfmAvgRef = cfmStatistics.AverageRef;
            cfmAvgPrev = cfmAvg;
            cfmAvg = cfmStatistics.Average;

            tempAvgRef = temperatureStatistics.AverageRef;
            tempAvgPrev = tempAvg;
            tempAvg = temperatureStatistics.Average;

            //Vérification de la calibration des données des capteurs, la zone ne sera pas mise à jour si elles ne sont pas calibrées au moins une fois.
            if (gsrStatistics.SensorCalibrated == true || respirationStatistics.SensorCalibrated == true || cfmStatistics.SensorCalibrated == true || temperatureStatistics.SensorCalibrated == true)
            {
                //Calcul de la nouvelle zone correspondant aux dernières données des capteurs.

                //L'algorithme d'analyse des données doit mettre à jour la variable <paramref name="zone"/> avec la nouvelle zonne correspondant aux données.
                //Les valeurs de zone possibles pour la variable <paramref name="zone"/> sont les suivantes :
                // 0 : Zone Neutre
                // 1 : Zone 1
                // 2 : Zone 2
                // 3 : Zone 3
                // 4 : Zone 4

                //<-------------------------------->
                //Pour changer d'algorithme de traitement, modifier cette section du script

                //Example d'algorithme de traitement :
                //  Si la valeur d'amplitude du capteur GSR est supérieure à la valeur de référence du capteur GSR, les données correspondent à la zone 1.
                //  Sinon, les données correspondent à la zone 2.
                //
                //Se traduira par le code suivant :
                //  if(GSR_amp > GSR_Amp_ref)
                //  {
                //      zone = 1;
                //  }
                //  else
                //  {
                //      zone = 2;
                //  }

                if (GSR_amp > GSR_Amp_ref)
                {
                    zone = 1;
                }
                else {
                    zone = 2;
                }

                //<-------------------------------->
            }
        }
    }
}
