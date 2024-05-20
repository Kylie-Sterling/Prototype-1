using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Audio : MonoBehaviour // modified script from breakout game
{
    public AudioSource[] audioSource;
    public AudioSource[] songLayers;
    public bool mutedSFX = false;
    public bool mutedMusic = false;
    public TextMeshProUGUI sFXTextToChange;
    public TextMeshProUGUI musicTextToChange;
    //public SettingsManager settingsManager;
    private void Awake()
    {
        PlaySong(); // stars playing the song
        //AudioVolumeCheck(); // checks what sounds should be muted
    }
    private void LateUpdate()
    {
       //AudioVolumeCheck(); // ensures that audio levels are correct
    }
    /*public void AudioVolumeCheck()
    {
        settingsManager = FindAnyObjectByType<SettingsManager>(); // finds the audio manager which knows if the music and SFX should be muted

        if (settingsManager.mutedSFX) // if SFX is not muted
        {
            mutedSFX = ChangeVolume(0f, audioSource, sFXTextToChange, "SFX Enabled", true); // mute SFX
        }
        else if (!settingsManager.mutedSFX) // if SFX is not muted
        {
            mutedSFX = ChangeVolume(1f, audioSource, sFXTextToChange, "SFX  Disabled", false); // unmutes SFX
        }
        if (settingsManager.mutedMusic) // if music is muted
        {
            mutedMusic = ChangeVolume(0f, songLayers, musicTextToChange, "Music Enabled", true); // mute music
        }
        else if (!settingsManager.mutedMusic) // if music is not muted
        {
            mutedMusic = ChangeVolume(0.35f, songLayers, musicTextToChange, "Music Disabled", false); // unmutes music (i found .35f to be a good volume)
        }
    }*/
    public void PlaySong() // plays song
    {
        if (!songLayers[0].isPlaying) // if the standard piano track is not playing
        {
            songLayers[0].loop = true; // sets the track to loop
            songLayers[0].Play(); // sets the track to begin playing
        }
    }
    public void AddLayer(int i) // adds layers to the song
    {
        songLayers[i].loop = true; // sets the layer to loop
        songLayers[i].time = songLayers[0].time; // sets the time to be the same as the main track so they are always in time
        songLayers[i].Play(); // plays the layer
    }
    public void RemoveLayer(int i) // removes a layer of the song
    {
        songLayers[i].Stop(); // stops playback of that layer
    }
    // Update is called once per frame
    void Update()
    {
        PlaySong(); // emsures song is always playing
    }
    public void PlaySound(int i) // plays a SFX from the array
    {
        audioSource[i].Play(); // plays SFX
    }
    public bool ChangeVolume(float volume, AudioSource[] audioToChange, TextMeshProUGUI textToChange, string changeText, bool tOF) // mutes/ unmutes sfx or music arrays
    {
        for(int i = 0; i < audioToChange.Length; i++) // goes through entire array of sounds that was passed in 
        {
            audioToChange[i].volume = volume; // sets to passed in volume 
        }
        //textToChange.text = changeText; // sets button text to be correct

        return tOF; // returns true or false to ensure the boolean used is correct
    } 
}
