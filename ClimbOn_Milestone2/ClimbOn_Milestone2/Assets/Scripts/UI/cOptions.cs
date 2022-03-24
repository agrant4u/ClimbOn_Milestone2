using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cOptions : MonoBehaviour
{

    //public Transform panelPlayerOptions;

    [Space]
    [Header("Audio Sliders")]
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    AudioManager am;

    public Dropdown gameResolution;

    public static bool hudOn;  // Change this probably

    cHUD hud;

    private void Start()
    {

        am = AudioManager.am;

    }

    public void InitUI()
    {
        
        // SET SLIDER VALUES
        musicSlider.value = AudioManager.am.musicVolume;
        sfxSlider.value = AudioManager.am.sfxVolume;
        masterSlider.value = AudioManager.am.masterVolume;
        //animationsToggle.isOn = GameManager.UIanimations;

        //AudioManager.am.musicSource.GetComponent<AudioLowPassFilter>().cutoffFrequency = 2000;

    }

    

    public void OnBackButton()
    {
        //AudioManager.am.PlaySFX(AudioManager.am.sfxClip[0]);

        Debug.Log("Back Clicked");
 
        //AudioManager.am.musicSource.GetComponent<AudioLowPassFilter>().cutoffFrequency = 20000;

        Destroy(this.gameObject);

    }
    

    public void OnMusicVolChanged()
    {
        
        am.ChangeMusicVolume(musicSlider.value);
    }


    public void OnMasterVolChanged()
    {

        am.ChangeMasterVolume(masterSlider.value);

    }

    public void OnSFXVolChanged()
    {

        am.ChangeSFXVolume(sfxSlider.value);

    }


}
