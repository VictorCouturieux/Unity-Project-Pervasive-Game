using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace UnityCaptiv
{
    namespace Sensors
    {
        public class SensorServer : MonoBehaviour
        {
            /// <summary>
            /// Modèle du capteur correspondant aux données à recevoir
            /// </summary>
            public enum SensorType {
                TSens_Accelerometer,
                TSens_ECG,
                TSens_EMG,
                TSens_FSR,
                TSens_GSR,
                TSens_GSR_Temperature,
                TSens_LoadCell,
                TSens_Motion,
                TSens_Respiration,
                TSens_Temperature
            }

            private TcpClient socket;
            private NetworkStream network;
            [Header("Sensor information")]
            [Tooltip("The model of the sensor to get data from")]
            public SensorType sensorType = SensorType.TSens_GSR;

            [Tooltip("The number of received data packet that will be saved in the SensorServer. Set this to -1 if you don't need to save them")]
            public int maximumDataCached = -1;


            [Header("Server connection")]
            [Tooltip("Ip address of the computer hosting the CAPTIV instance, default is localhost 127.0.0.1")]
            public string host = "127.0.0.1";
            [Tooltip("Port on which CAPTIV streams the sensor data, default is port 7660")]
            public int port = 7660;


            private bool serverConnected = false;

            private float timestamp;

            private float serverAutoConnectTimer = 2.0f;

            private bool serverConnectionThreadActive = false;
            private Thread serverConnectionThread = null;

            private float[] realTimeSensorData = null;  //Valeurs en temps réel des données du capteur
            public float[] RealtimeSensorData {
                get
                {
                    return realTimeSensorData;
                }
            }

            private Queue<List<double>> sensorData = new Queue<List<double>>();
            public List<double>[] SensorData    //Cache des données du capteur envoyés par CAPTIV.
            {
                get {
                    return sensorData.ToArray();
                }                
            }       

            /// <summary>
            /// Nombre de canaux de données des capteurs TEA.
            /// </summary>
            public int SensorChannelCount
            {
                get
                {
                    switch (sensorType)
                    {
                        case SensorType.TSens_Accelerometer:
                            return 3;
                        case SensorType.TSens_ECG:
                            return 1;
                        case SensorType.TSens_EMG:
                            return 1;
                        case SensorType.TSens_FSR:
                            return 3;
                        case SensorType.TSens_GSR:
                            return 1;
                        case SensorType.TSens_GSR_Temperature:
                            return 2;
                        case SensorType.TSens_LoadCell:
                            return 1;
                        case SensorType.TSens_Motion:
                            return 4;
                        case SensorType.TSens_Respiration:
                            return 1;
                        case SensorType.TSens_Temperature:
                            return 1;
                    }

                    return -1;
                }
            }

            /// <summary>
            /// Vitesse d'acquisition des capteurs TEA.
            /// </summary>
            public int SensorSamplingRate
            {
                get
                {
                    switch (sensorType)
                    {
                        case SensorType.TSens_Accelerometer:
                            return 128;
                        case SensorType.TSens_ECG:
                            return 256;
                        case SensorType.TSens_EMG:
                            return 2048;
                        case SensorType.TSens_FSR:
                            return 32;
                        case SensorType.TSens_GSR:
                            return 32;
                        case SensorType.TSens_GSR_Temperature:
                            return 32;
                        case SensorType.TSens_LoadCell:
                            return 32;
                        case SensorType.TSens_Motion:
                            return 64;
                        case SensorType.TSens_Respiration:
                            return 32;
                        case SensorType.TSens_Temperature:
                            return 32;
                    }

                    return -1;
                }
            }

            private void Awake()
            {
                //Lancement du thread de connexion au T-Server de CAPTIV.
                serverConnectionThreadActive = true;
                serverConnectionThread = new Thread(connectToServerThread);
                serverConnectionThread.Start();

            }

            void OnApplicationQuit()
            {
                //Arrêt forcé du thread de connexion au T-Server pour s'assurer qu'il ne devienne pas un thread zombie.
                try
                {
                    serverConnected = false;
                    serverConnectionThreadActive = false;
                    serverConnectionThread.Abort();
                }
                catch (ThreadAbortException ex)
                {
                    if (ex != null)
                    {
                        Debug.Log("Sensor server connection thread stopped");
                    }
                }

                //Fermeture des connexions réseau. 
                network.Close();
                socket.Close();
            }

            private void Update()
            {
                if (serverConnected)
                {
                    if (socket.Connected == false)
                    {
                        Debug.Log("Server disconnected");
                        serverConnected = false;
                        return;
                    }

                    List<List<double>> decodedData = null;
                    Byte[] data = new byte[10000];

                    //Si CAPTIV a envoyé des données.
                    if (network.DataAvailable)
                    {
                        int dataLength = network.Read(data, 0, data.Length);

                        decodedData = decodeByte(data, dataLength);

                        realTimeSensorData = new float[SensorChannelCount];

                        //Pour la valeur "temps réel", on utilise la 1ère données contenue dans la trame envoyée par CAPTIV.
                        //La totalité des données envoyées est stockée dans le cache <paramref name="SensorData"/> si nécessaire.
                        timestamp = (float)decodedData[0][0];
                        for (int i = 1; i <= SensorChannelCount; i++)
                        {
                            realTimeSensorData[i - 1] = (float)decodedData[0][i];
                        }

                        //Si le cache de données est actif.
                        if (maximumDataCached >= 0)
                        {
                            //Stockage des données reçues dans le cache.
                            foreach (List<double> packet in decodedData)
                            {
                                sensorData.Enqueue(packet);
                            }

                            //Si la capacité du cache est dépassées, les données les plus vielles sont effacées jusqu'à ce que le cache revienne à sa taille maximale autorisée.
                            while (sensorData.Count > maximumDataCached)
                            {
                                sensorData.Dequeue();
                            }
                        }

                    }
                }
            }

            /// <summary>
            /// Connecte le serveur de capteur au T-Server de CAPTIV
            /// Si la connexion échoue, le serveur de capteur essaiera à nouveau de se connecter après <paramref name="serverAutoConnectTimer"/> secondes.
            /// </summary>
            private void connectToServerThread()
            {
                while (serverConnectionThreadActive == true)
                {
                    if (serverConnected == false)
                    {
                        try
                        {
                            //Lancement du thread de connexion au T-Server de CAPTIV.
                            socket = new TcpClient(host, port);
                            network = socket.GetStream();
                            serverConnected = true;
                        }
                        catch (Exception e)
                        {
                            serverConnected = false;
                            Debug.LogError(port + " - " + e);
                        }

                    }

                    Thread.Sleep((int)(serverAutoConnectTimer * 1000.0));
                }
            }

            /// <summary>
            /// Décode et extrait les données envoyées par CAPTIV.
            /// </summary>
            /// <param name="dataByte">La trame de données envoyée par CAPTIV.</param>
            /// <param name="length">La taille de la trame de données envoyée par CAPTIV.</param>
            /// <returns></returns>
            private List<List<double>> decodeByte(Byte[] dataByte, int length)
            {
                int dataCount = 1 + SensorChannelCount;

                List<List<double>> data = new List<List<double>>(); //Chaque entrée de la liste contient un paquet de données du capteur 
                //Chaque paquet suit la forme suivante:
                //packet[0] == timestamp du packet
                //packet[1] == valeur du canal de donnée 1 du capteur
                // .
                // .
                // .
                //packet[n] == valeur du canal de donnée n du capteur

                for (int i = 0; i < length;)
                {
                    List<double> dataPacket = new List<double>();

                    for (int j = 0; j <= SensorChannelCount; j++)
                    {
                        dataPacket.Add(System.BitConverter.ToDouble(dataByte, i));
                        i += 8;
                    }

                    data.Add(dataPacket);
                }

                return data;
            }
        }
    }
}

