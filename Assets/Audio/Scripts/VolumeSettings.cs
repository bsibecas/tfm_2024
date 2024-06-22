using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;


    public void SetMusicVolume()
    {
        float SFXVolume = musicSlider.value;
        float musicVolume = SFXSlider.value;

        GameManager.Instance.SaveVolume(SFXSlider.value, musicSlider.value);
    }
}
