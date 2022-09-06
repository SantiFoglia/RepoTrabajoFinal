using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MenuOpciones : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetVolumen (float volumen)
    {
        audioMixer.SetFloat("volumen", volumen);
    }
    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
