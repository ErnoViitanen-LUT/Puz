﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
   private static AudioManager _instance;

   public static AudioManager Instance { get { return _instance; } }
   public AudioClip musicClip;
   public bool musicOn = true;
   public AudioClip footStep;
   public AudioClip levelClear;
   public AudioClip[] jumpStart;
   public AudioClip[] jumpEnd;
   public AudioClip[] boulders;

   public float musicVolume = 0.8f;
   public float effectsVolume = 0.8f;

   AudioSource musicSource;
   AudioSource footStepSource;
   AudioSource effectSource;
   private void Awake()
   {
      if (_instance != null && _instance != this)
      {
         Destroy(this.gameObject);
      }
      else
      {
         _instance = this;
         musicSource = gameObject.AddComponent<AudioSource>();
         musicSource.clip = musicClip;
         musicSource.loop = true;
         musicSource.volume = musicVolume;

         effectSource = gameObject.AddComponent<AudioSource>();
         effectSource.clip = levelClear;
         effectSource.volume = effectsVolume;

         footStepSource = gameObject.AddComponent<AudioSource>();
         footStepSource.clip = footStep;
         footStepSource.volume = effectsVolume;
         DontDestroyOnLoad(transform.gameObject);
         PlayMusic();
      }

   }

   public void PlayMusic()
   {
      if (!musicSource.isPlaying && musicOn)
      {
         musicSource.Play();
      }
   }

   public void StopMusic()
   {
      musicSource.Stop();
   }
   public void PlayLevelClear()
   {
      effectSource.Play();
   }
   public void PlayFootStep()
   {
      footStepSource.Play();
   }
   public void PlayJumpStart()
   {
      effectSource.PlayOneShot(jumpStart[Random.Range(0, jumpStart.Length)]);
   }
   public void PlayJumpEnd()
   {
      effectSource.PlayOneShot(jumpEnd[Random.Range(0, jumpEnd.Length)]);
   }
   public void PlayBoulder()
   {
      effectSource.PlayOneShot(boulders[Random.Range(0, boulders.Length)]);
   }
}
