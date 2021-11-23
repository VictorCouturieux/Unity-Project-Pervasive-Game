using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityCaptiv
{
    namespace Sensors
    {
        public class SensorStatistics : MonoBehaviour
        {
            public UnityCaptiv.Sensors.SensorServer sensorSource = null;

            [SerializeField]
            [Range(1.0f, 10.0f)]
            [Tooltip("Duration on which the statistics on the sensor will be made, default = 5 seconds")]
            private float timeWindowSize = 5.0f;
            public float TimeWindowSize {
                get
                {
                    return timeWindowSize;
                }
                set
                {
                    timeWindowSize = value;
                    //Met à jour la taille du cache du SensorServer pour qu'il correspondent au montant de données envoyées par le capteur pendant <paramref name="TimeWindowSize"/> secondes.
                    int samplingRate = sensorSource.SensorSamplingRate;
                    sensorSource.maximumDataCached = (int)(samplingRate * TimeWindowSize);
                }
            }

	    public int sensorChannel = 1;  			//

            private double dataMinValue = 0.0f;                     //Valeur minimale des données du capteur sur les <paramref name="TimeWindowSize"/> dernières secondess d'enregistrement.
            private double dataMaxValue = 0.0f;                     //Valeur maximale des données du capteur sur les <paramref name="TimeWindowSize"/> dernières secondess d'enregistrement.

            private double dataNeutralValue = 0.0f;
            public double NeutralValue { get => dataNeutralValue; } //Valeur de référence des données du capteur.


            private double dataAverage = 0.0f;
            public double Average { get => dataAverage; }           //Valeur moyenne des données du capteur sur les <paramref name="TimeWindowSize"/> dernières secondess d'enregistrement.

            private double dataAverageRef = 0.0f;
            public double AverageRef { get => dataAverageRef; }     //Valeur moyenne de référence des données du capteur.


            private double dataAmplitude = 0.0f;
            public double Amplitude { get => dataAmplitude; }       //Amplitude des données du capteur sur les <paramref name="TimeWindowSize"/> dernières secondess d'enregistrement.

            private double dataAmplitudeRef = 0.0f;
            public double AmplitudeRef { get => dataAmplitudeRef; } //Amplitude de référence des données du capteur sur les <paramref name="TimeWindowSize"/> dernières secondess d'enregistrement.

            private bool calibrated = false;
            public bool SensorCalibrated { get => calibrated; }     //Les données du capteur ont-elles été calibrées? Pour effectuer la calibration, appeler la fonction Calibrate().

            void Start()
            {
                if (sensorSource != null)
                {
                    int samplingRate = sensorSource.SensorSamplingRate;

                    //Met à jour la taille du cache du SensorServer pour qu'il correspondent au montant de données envoyées par le capteur pendant <paramref name="TimeWindowSize"/> secondes.
                    sensorSource.maximumDataCached = (int)(samplingRate * TimeWindowSize);
                }

                StartCoroutine(RefreshData());
            }

            private IEnumerator RefreshData()
            {
                while (true)
                {          
                    //Attente jusqu'au prochain tic de mise à jour
                    yield return new WaitForSecondsRealtime(timeWindowSize);

                    if (sensorSource != null)
                    {
                        //Récupération des données du capteur pour les <paramref name="TimeWindowSize"/> dernières secondes d'enregistement.
                        List<double>[] sensorData = sensorSource.SensorData;

                        double mean = 0.0f;

                        //Mise à jour des valeurs minimales et maximales.
                        if (sensorData.Length > 0)
                        {
                            dataMinValue = sensorData[0][sensorChannel];
                            dataMaxValue = sensorData[0][sensorChannel];
                        }

                        for (int i = 0; i < sensorData.Length; i++)
                        {
                            if (sensorData[i][sensorChannel] < dataMinValue)
                            {
                                dataMinValue = sensorData[i][sensorChannel];
                            }
                            if (sensorData[i][sensorChannel] > dataMaxValue)
                            {
                                dataMaxValue = sensorData[i][sensorChannel];
                            }

                            mean += sensorData[i][sensorChannel];
                        }

                        //Mise à jour de l'amplitude et de la moyenne des données.
                        dataAmplitude = dataMaxValue - dataMinValue;

                        if (sensorData.Length > 0)
                        {
                            dataAverage = mean / (double)sensorData.Length;
                        }
                    }
                }
            }

            /// <summary>
            /// Initialise les valeurs de référence des capteurs.
            /// </summary>
            public void Calibrate()
            {
                if (sensorSource != null)
                {
                    if (sensorSource.RealtimeSensorData != null)
                    {
                        if (sensorSource.RealtimeSensorData.Length > 0)
                        {
                            dataNeutralValue = sensorSource.RealtimeSensorData[0];
                            dataAverageRef = dataAverage;
                            dataAmplitudeRef = dataAmplitude;
                            calibrated = true;
                        }
                    }
                }
            }
        }
    }
}
