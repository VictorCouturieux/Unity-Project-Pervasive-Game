using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    public Color zoneEnabledColor = new Color(0.0f, 1.0f, 0.0f, 1.0f);
    public Color zoneDisabledColor = new Color(1.0f, 0.0f, 0.0f, 1.0f);

    public SensorAnalysis sensorAnalysis = null;

    public Image stateNeutralZone = null;
    public Image stateZone1 = null;
    public Image stateZone2 = null;
    public Image stateZone3 = null;
    public Image stateZone4 = null;

    void Update()
    {
        //Met à jour la carte des zones
        if (sensorAnalysis != null)
        {
            //Réinitialisation de la carte
            stateNeutralZone.color = zoneDisabledColor;
            stateZone1.color = zoneDisabledColor;
            stateZone2.color = zoneDisabledColor;
            stateZone3.color = zoneDisabledColor;
            stateZone4.color = zoneDisabledColor;

            //Application du résultat de la dernière zone calculée.
            switch (sensorAnalysis.Zone)
            {
                case 0:
                    stateNeutralZone.color = zoneEnabledColor;
                    break;
                case 1:
                    stateZone1.color = zoneEnabledColor;
                    break;
                case 2:
                    stateZone2.color = zoneEnabledColor;
                    break;
                case 3:
                    stateZone3.color = zoneEnabledColor;
                    break;
                case 4:
                    stateZone4.color = zoneEnabledColor;
                    break;
            }
        }
    }
}
