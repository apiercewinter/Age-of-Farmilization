//Aaron Winter

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{

    [SerializeField] private string volumeParameter;
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider mySlider;

    [SerializeField] private float multiplier = 30f; 

    // Start is called before the first frame update
    void Awake()
    {
        updateSlider();
        mySlider.onValueChanged.AddListener(volumeChange);
    }

    private void volumeChange(float volume)
    {
        volume = Mathf.Log10(volume) * multiplier;
        myMixer.SetFloat(volumeParameter, volume); 
    }

    //Updates the slider to the current volume of the source
    private void updateSlider()
    {
        float curVol;
        myMixer.GetFloat(volumeParameter, out curVol);
        curVol = Mathf.Pow(10, curVol / multiplier);
        mySlider.value = Mathf.Max(.0001f, Mathf.Min(1, curVol));
    }
}
