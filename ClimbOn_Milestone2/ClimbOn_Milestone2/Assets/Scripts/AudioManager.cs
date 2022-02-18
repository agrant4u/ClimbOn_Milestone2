using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum eSFX {click, noGo, buy, jail, sweep, chance, commChest, rent, railroad, electric, waterworks, dice, luxury, income, cha_Ching, free, GO, saveLoad, sell, construct, carDrive, thimbleFly, whistle, diceLoop, tada, sad_ahh, property }
public enum eMusic {Flux, Collage, CandyShop, Subversie }


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

    public AudioClip[] sfxClip;

    public AudioClip[] musicClip;

    private AudioClip currentMusic;

    [Space]
    [Header("Audio Sources")]
    public AudioSource sfxSource;
    public AudioSource musicSource;

    public static int musicOrder = 0;

    //[NamedArray(typeof(eMusic))] public AudioClip[] music;

    //[NamedArray(typeof(eSFX))] public AudioClip[] sfx;
    //[NamedArray(typeof(eSFX))] public AudioClip[] sfx;


    // Awake Refs
    private void Awake()
    {
       

        if (am == null)
        {

            DontDestroyOnLoad(this.gameObject);
            am = this;


        }

        else if (am != this)
        {

            Destroy(gameObject);

        }

        //am = GetComponentInChildren<AudioManager>();

        currentMusic = musicClip[musicOrder];
        PlayMusic(musicClip[musicOrder]);
        
    }

    public void EPlayMusic(eMusic _music)
    {
        //musicSource.Play(music[(int)_music]);
    }

    public void EPlaySFX(eSFX _sfx)
    {
        //sfxSource.PlayOneShot(sfx[(int)_sfx]);
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


    // PLAY CONTROLS
    public void PlaySFX(AudioClip sfxClip)
    {

        sfxSource.PlayOneShot(sfxClip);// Plays sound fx clip

        //sfxSource[(int)_soundIndex].Play();

    }

    public void PlayMusic(AudioClip musicClip)
    {
        musicSource.clip = musicClip;
        musicSource.Play();

        //musicSource[(int)_musicIndex].Play();

    }// Plays music

    // STOP SOUNDS
   
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
            PlayMusic(musicClip[musicOrder]);
           
        }

    }// Changes to next song in array

    public void PreviousSong()
    {
        if (musicOrder != 0)
        {
            musicOrder--;
            musicSource.Stop();
            PlayMusic(musicClip[musicOrder]);
        }
        else
        {
            Debug.Log("THIS IS THE FIRST SONG!");
        }
        

    }// Changes to previous song in array

    public int GetMaxSongs()
    {
       
     int maxMusic = musicClip.Length;
     return maxMusic;
}// Returns music array length

}
