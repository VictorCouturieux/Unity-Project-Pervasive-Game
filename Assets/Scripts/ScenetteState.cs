using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// Visualisation de l'état des scenettes, les éléments à activer par le toucher et la repsiration.
// UI.

public class ScenetteState : MonoBehaviour
{

    public ScenetteType[] scenetteTypes;
    public Sprite[] sprite;


    void Update()
    {
        ScenetteType sce0 = scenetteTypes[0];
        ScenetteType sce1 = scenetteTypes[1];



        if (Input.GetKey(KeyCode.Space))
        {
            sce0.colorState = 1;
            if(Input.GetKey(KeyCode.Space) && sce0.completed == true)
            {
                sce0.colorState = 2;
            }
        }else if (sce0.completed == false)
        {
            sce0.colorState = 0;
        }
        else if (sce0.completed == true)
        {
            sce0.colorState = 2;
        }



        if (Input.GetKey(KeyCode.Return))
        {
            sce1.colorState = 1;
            if (Input.GetKey(KeyCode.Return) && sce1.completed == true)
            {
                sce1.colorState = 2;
            }
        }
        else if (sce1.completed == false)
        {
            sce1.colorState = 0;
        }
        else if (sce1.completed == true)
        {
            sce1.colorState = 2;
        }


        sce0.image.sprite = sprite[sce0.colorState];
        sce1.image.sprite = sprite[sce1.colorState];
        //sce2.image.sprite = sprite[colorState];
        //sce3.image.sprite = sprite[colorState];

    }

}
