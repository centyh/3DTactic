using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider = null;


    private void Start()
    {
        LoadValue();
    }

    
    //Asetetaan Sliderin OnValueChanged-toimintoon = Tallentaa PlayerPrefsin avulla sen arvon mihin sen Slideriss‰ j‰tt‰‰
    public void SaveVolume()
    {
        float volumeValue = volumeSlider.value;
        PlayerPrefs.SetFloat("VolumeValue", volumeValue);
        LoadValue();
    }

    //Kun Scene avautuu, lataa viimeisimm‰n Sliderin arvon
    void LoadValue()
    {
        float volumeValue = PlayerPrefs.GetFloat("VolumeValue");
        volumeSlider.value = volumeValue;
        AudioListener.volume = volumeValue;
    }
}
