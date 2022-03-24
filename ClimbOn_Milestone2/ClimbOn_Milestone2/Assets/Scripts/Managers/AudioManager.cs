using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum eSFX { footsteps, jumping, climbing, dash, mantle, gemGrab, collectibleGrab, grappleStart, grappleStop, grapplePull, umbrellaUp, umbrellaDown, staminaLow, staminaPickup, speedBoost, snakeTrigger, goatBuck, goatBaaaa, rockPickup, rockThrow, ropeSwing, fallingRockHit, looseRockTrigger, death, checkPoint, beeShot, shroom }

public enum eMusic { music1 }

public enum eUIaudio { click, select, slider, startGame, pauseOn, pauseOff, quit }


public class AudioManager : MonoBehaviour
{

    public static AudioManager am;

    [Space]
    [Header("Volume Settings")]
    public float masterVolume = 1;
    public float musicVolume = 1;
    public float sfxVolume = 1;

    [Space]
    [Header("Audio Mixers")]
    public AudioMixerGroup masterMixer;
    public AudioMixerGroup musicMixer;
    public AudioMixerGroup sfxMixer;

    [Space]
    [Header("Audio Clips")]
    [NamedArray(typeof(eMusic))] public AudioClip[] music;
    [NamedArray(typeof(eSFX))] public AudioClip[] sfx;
    [NamedArray(typeof(eUIaudio))] public AudioClip[] ui;

    private AudioClip currentMusic;
    eMusic eCurrentMusic;

    [Space]
    [Header("Audio Sources")]
    public AudioSource sfxSource;
    public AudioSource musicSource;

    public static int musicOrder = 0; 

    // Awake Refs
    private void Awake()
    {

        if (am == null)
        {

            DontDestroyOnLoad(this.gameObject);
            am = this;

            eCurrentMusic = (int)(eMusic)0;
            currentMusic = music[(int)eCurrentMusic];
            musicSource.clip = currentMusic;
        }

        else if (am != this)
        {

            Destroy(gameObject);

        }

        //am = GetComponentInChildren<AudioManager>();

        //currentMusic = musicClip[musicOrder];
        //PlayMusic(musicClip[musicOrder]);
        
    }

    public void PlayMusic(eMusic _music)
    {

        currentMusic = music[(int)_music];
        
        musicSource.Play();

    }

    public void PlaySFX(eSFX _sfx)
    {
       Debug.Log("Playing " + sfx[(int)_sfx]);
       sfxSource.PlayOneShot(sfx[(int)_sfx]);
    }

    public void PlayUIAudio(eUIaudio _uiAudio)
    {

        sfxSource.PlayOneShot(ui[(int)_uiAudio]);

    }

    // MIXER CONTROLS
    public void ChangeMusicVolume(float _newValue)// Changes fader value of music volume
    {

        musicMixer.audioMixer.SetFloat("MusicVolume", Mathf.Log10(_newValue) * 20);// Changes as a logarithmic fade
        musicVolume = _newValue;
    }

    public void ChangeSFXVolume(float _newValue)// Changes fader value of sfx volume
    {

        sfxMixer.audioMixer.SetFloat("SFXVolume", Mathf.Log10(_newValue) * 20);
        sfxVolume = _newValue;
    }

    public void ChangeMasterVolume(float _newValue)// Changes fader value of master volume
    {

        masterMixer.audioMixer.SetFloat("MasterVolume", Mathf.Log10(_newValue) * 20);
        masterVolume = _newValue;
    }

    // STOP SOUNDS
   
    /*
    public void StopMusic()
    {

        musicSource.clip = currentMusic;
        musicSource.Stop();
        //musicSource[(int)_soundIndex].Stop();
    }// Stops current music

    public void NextSong()
    {


        if (musicOrder >= GetMaxSongs())
        {
            Debug.Log("THIS IS THE LAST SONG!");
        }

        else
        {
            
            musicSource.Stop();
            musicOrder++;
            //PlayMusic(musicClip[musicOrder]);
           
        }

    }// Changes to next song in array

    public void PreviousSong()
    {
        if (musicOrder != 0)
        {
            musicOrder--;
            musicSource.Stop();
            //PlayMusic(musicClip[musicOrder]);
        }
        else
        {
            Debug.Log("THIS IS THE FIRST SONG!");
        }
        

    }// Changes to previous song in array


    */

}
