using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;

namespace UnityCaptiv
{
    namespace Core
    {
        public class ControlServer : MonoBehaviour
        {
            private static ControlServer instance;
            public static ControlServer Instance
            {
                get
                {
                    return ControlServer.instance;
                }
            }
            
            private TcpClient socket;
            private NetworkStream network;
            private StreamWriter networkWriter;

            [Header("Server connection")]
            [Tooltip("Ip address of the computer hosting the CAPTIV instance, default is localhost 127.0.0.1")]
            public string host = "127.0.0.1";
            [Tooltip("Port on which CAPTIV listens to commands, default is port 7652")]
            public int port = 7652;

            private bool serverConnected = false;
            public bool ServerConnected
            {
                get { return serverConnected; }
            }

            private float serverAutoConnectTimer = 2.0f;

            private bool serverConnectionThreadActive = false;
            private Thread serverConnectionThread = null;

            public bool isRecording = false;



            private void Awake()
            {
                if (ControlServer.instance == null)
                {
                    ControlServer.instance = this;
                }
                else
                {
                    DestroyImmediate(this);
                }

                //Lancement du thread de connexion au T-Server de CAPTIV.
                serverConnectionThreadActive = true;
                serverConnectionThread = new Thread(ConnectToServerThread);
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
                        Debug.Log("Control server connection thread stopped");
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
                    string decodedData = "";
                    Byte[] data = new byte[256];

                    //Réception des données.
                    if (network.DataAvailable)
                    {
                        //Si CAPTIV a envoyé des données.

                        //Lecture des données reçues.
                        network.Read(data, 0, data.Length);
                        decodedData = System.Text.Encoding.UTF8.GetString(data);

                        //Gestion des codes d'erreurs au cas où CAPTIV en ait envoyé un.
                        if (Regex.Match(decodedData, "^TEAError").Success)
                        {
                            string errorMessage = decodedData.Split('\t')[2];
                            string errorCode = decodedData.Split('\t')[1];

                            string error = ManageError(errorCode);

                            Debug.LogError(error);
                        }
                    }
                }
            }

            #region Gestion Serveur

            /// <summary>
            /// Connecte le serveur de contrôle au T-Server de CAPTIV
            /// Si la connexion échoue, le serveur de contrôle essaiera à nouveau de se connecter après <paramref name="serverAutoConnectTimer"/> secondes.
            /// </summary>
            private void ConnectToServerThread()
            {
                while (serverConnectionThreadActive == true)
                {
                    if (serverConnected == false)
                    {
                        try
                        {
                            //Lance la connextion avec CAPTIV.
                            socket = new TcpClient(host, port);
                            network = socket.GetStream();
                            serverConnected = true;
                        }
                        catch (Exception e)
                        {
                            serverConnected = false;
                            Debug.LogError(e);
                        }
                    }

                    Thread.Sleep((int)(serverAutoConnectTimer * 1000.0));
                }
            }

            /// <summary>
            /// Retourne la description du code d'erreur correspondant.
            /// </summary>
            /// <param name="errorCode">Le code d'erreur.</param>
            /// <returns>La description du code d'erreur donné.</returns>
            private string ManageError (string errorCode)
            {
                switch (errorCode)
                {
                    case "1001":
                        return "Error" + errorCode + " : No class created";
                    case "1002":
                        return "Error" + errorCode + " : Cannot be done while recording";
                    case "1003":
                        return "Error" + errorCode + " : Available only while recording";
                    case "1004":
                        isRecording = false;
                        return "Error" + errorCode + " : Not ready to initialize a recording";
                    case "1005":
                        isRecording = false;
                        return "Error" + errorCode + " : Not ready to start a recording";
                    default:
                        return "Invalid error code : " + errorCode;
                }
            }

            /// <summary>
            /// Envoie une commande au T-Server de CAPTIV.
            /// </summary>
            /// <param name="command">Commande à envoyer.</param>
            public void SendCommand(string command)
            {
                try
                {
                    byte[] data = System.Text.Encoding.UTF8.GetBytes(command + "\n");
                    if (network != null)
                    {
                        network.Write(data, 0, data.Length);
                        network.Flush();
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError(this.gameObject.transform.name + " : " + e.ToString());
                }
            }

            #endregion

            #region Gestion Enrgistrement

            /// <summary>
            /// Initialise un nouvel enregistrement dans CAPTIV avec le nom <paramref name="name"/>.
            /// </summary>
            /// <param name="name">Le nom du nouvel enregistrement.</param>
            public void InitializeRecording(string name = "")
            {
                SendCommand("TEAInitRec\t" + name);
            }

            /// <summary>
            /// Lance un enregistrement dans CAPTIV.
            /// </summary>
            public void StartRecording()
            {
                if (isRecording == false)
                {
                    SendCommand("TEAStartRec");
                    isRecording = true;
                }
            }

            /// <summary>
            /// Stoppe un enregistrement dans CAPTIV.
            /// </summary>
            public void StopRecording()
            {
                if (isRecording == true)
                {
                    SendCommand("TEAStopRec");
                    isRecording = false;
                }
            }

            /// <summary>
            /// Génère un nouveau top synchro dans CAPTIV.
            /// </summary>
            public void SendTopSynchro()
            {
                SendCommand("TEAAskForTOP");
            }

            #endregion

        }
    }
}