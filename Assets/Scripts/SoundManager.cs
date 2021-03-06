﻿using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class SoundManager : MonoBehaviour
{
    public AudioSource efxSource;                   //Drag a reference to the audio source which will play the sound effects.
    public AudioSource musicSource;                 //Drag a reference to the audio source which will play the music.           
    public float lowPitchRange = .95f;              //The lowest a sound effect will be randomly pitched.
    public float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.

    public AudioClip MistakeSound;

    public AudioClip musicIntro;
    public AudioClip musicLoop;

    public TimeManager timeManager;

    public void Start()
    {
        musicSource.clip = musicIntro;
        musicSource.Play();

        Invoke("PlayLoopMusic", musicSource.clip.length);
    }

    public void PlayLoopMusic()
    {
        musicSource.Stop();
        musicSource.clip = musicLoop;
        musicSource.loop = true;
        musicSource.Play();
        timeManager.StartCounting();
    }

    public void OnMistake(object sender, ResolveCommandEventArgs e)
    {
        if (e.IsCorrect) return;
        PlaySingle(MistakeSound);
    }



    //Used to play single sound clips.
    public void PlaySingle(AudioClip clip)
    {
        //Set the clip of our efxSource audio source to the clip passed in as a parameter.
        efxSource.clip = clip;

        //Debug.Log("SOUND");

        //Play the clip.
        efxSource.Play();
    }


    //RandomizeSfx chooses randomly between various audio clips and slightly changes their pitch.
    public void RandomizeSfx(params AudioClip[] clips)
    {
        //Generate a random number between 0 and the length of our array of clips passed in.
        int randomIndex = Random.Range(0, clips.Length);

        //Choose a random pitch to play back our clip at between our high and low pitch ranges.
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        //Set the pitch of the audio source to the randomly chosen pitch.
        efxSource.pitch = randomPitch;

        //Set the clip to the clip at our randomly chosen index.
        efxSource.clip = clips[randomIndex];

        //Play the clip.
        efxSource.Play();
    }
}