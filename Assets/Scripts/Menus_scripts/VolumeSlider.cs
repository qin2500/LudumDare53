using UnityEngine;
using TMPro;

public class VolumeSlider:MonoBehaviour
{

    public static float musicVolume {get; private set;}
    public static float SFXVolume {get; private set;}

    //[SerializeField] private TextMeshProUGUI musicSliderText;
    //[SerializeField] private TextMeshProUGUI SFXSliderText;

    
    public void OnMusicSliderValueChange(float value){
        musicVolume = value;
        //musicSliderText.text = ((int) (value * 100)).ToString();
        SoundManager.Instance.UpdateMixerVolume();
    }
    public void OnSFXSliderValueChange(float value){
        SFXVolume = value;
       //SFXSliderText.text = ((int) (value * 100)).ToString();
        SoundManager.Instance.UpdateMixerVolume();
    }

}
