using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace UnityCaptiv
{
    namespace Core
    {
        public class UI_RecordingManager : MonoBehaviour
        {
            [Header("Recording")]
            [SerializeField] private Button btnStartRecording;
            [SerializeField] private Button btnStopRecording;
            [SerializeField] private InputField inputRecordingName;

            [Header("Top synchro")]
            [SerializeField] private Button btnTopSynchro;
            [SerializeField] private Color[] btnTopSynchroColors = { new Color(1.0f, 0.5f, 0.0f, 1.0f), new Color(0.0f, 1.0f, 0.2f, 1.0f) };
            private int topSynchroColorIndex = 0;


            private void Start()
            {
                if (UnityCaptiv.Core.ControlServer.Instance != null)
                {
                    this.btnStartRecording.onClick.AddListener(
                        () => StartCoroutine(StartRecording())
                    );
                    this.btnStopRecording.onClick.AddListener(
                        StopRecording
                    );

                    this.btnTopSynchro.onClick.AddListener(
                        GenerateTopSynchro
                    );

                    this.btnStartRecording.gameObject.SetActive(true);
                    this.btnStopRecording.gameObject.SetActive(false);
                }
                else
                {
                    Debug.LogError("Unable to find a ControlServer instance, please add one to the scene.");
                }

                this.btnTopSynchro.GetComponent<Image>().color = this.btnTopSynchroColors[this.topSynchroColorIndex];
            }

            /// <summary>
            /// Lance un enregistrement avec le nom donné par <paramref name="inputRecordingName"/> dans CAPTIV.
            /// </summary>
            private IEnumerator StartRecording()
            {
                //Definition du nom de l'enregistrement dans CAPTIV.
                if (UnityCaptiv.Core.ControlServer.Instance != null)
                {
                    UnityCaptiv.Core.ControlServer.Instance.InitializeRecording(this.inputRecordingName.text);
                }


                yield return new WaitForSecondsRealtime(0.5f);

                //Lancement de l'enregistrement
                if (UnityCaptiv.Core.ControlServer.Instance != null)
                {
                    UnityCaptiv.Core.ControlServer.Instance.StartRecording();
                    this.btnStartRecording.gameObject.SetActive(false);
                    this.btnStopRecording.gameObject.SetActive(true);
                }

                yield return null;
            }

            /// <summary>
            /// Stoppe un enregistrement dans CAPTIV.
            /// </summary>
            public void StopRecording()
            {
                if (UnityCaptiv.Core.ControlServer.Instance != null)
                {
                    UnityCaptiv.Core.ControlServer.Instance.StopRecording();
                    this.btnStartRecording.gameObject.SetActive(true);
                    this.btnStopRecording.gameObject.SetActive(false);
                }
            }

            /// <summary>
            /// Génère un top synchro dans CAPTIV.
            /// </summary>
            public void GenerateTopSynchro()
            {
                if (UnityCaptiv.Core.ControlServer.Instance != null)
                {
                    UnityCaptiv.Core.ControlServer.Instance.SendTopSynchro();

                    //Changement de la couleur du bouton top synchro pour feedback visuel.

                    this.topSynchroColorIndex++;
                    if (this.topSynchroColorIndex >= this.btnTopSynchroColors.Length)
                    {
                        this.topSynchroColorIndex = 0;
                    }
                    this.btnTopSynchro.GetComponent<Image>().color = this.btnTopSynchroColors[this.topSynchroColorIndex];
                }
            }
        }
    }
}
