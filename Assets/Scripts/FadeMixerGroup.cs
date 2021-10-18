using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;


// WIP tentative de faire un fade entre deux musiques. Le truc est bon je crois mais je sais pas comment l'appeler dans un autre script.
// lien du tuto : https://gamedevbeginner.com/how-to-fade-audio-in-unity-i-tested-every-method-this-ones-the-best/ méthode 2.


public class FadeMixerGroup : MonoBehaviour
{
    public static IEnumerator StartFade (AudioMixer audioMixer, string exposedParam, float duration, float targetVolume)
    {
        float currentTime = 0;
        float currentVol;
        audioMixer.GetFloat(exposedParam, out currentVol);
        currentVol = Mathf.Pow(10, currentVol / 20);
        float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);


        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);
            audioMixer.SetFloat(exposedParam, Mathf.Log10(newVol) * 20);
            yield return null;
        }

        yield break;
    }




}
