using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
   private static AudioManager _instance;

   public static AudioManager Instance { get { return _instance; } }
   public AudioMixer audioMixer;
   public AudioClip musicClip;
   public bool musicOn = true;
   public AudioClip footStep;
   public AudioClip levelClear;
   public AudioClip flashlight;
   public AudioClip[] jumpStart;
   public AudioClip[] jumpEnd;
   public AudioClip[] fall;
   public AudioClip[] boulders;
   public AudioClip[] effect;

   public AudioClip[] menu;

   public float musicVolume = 0.8f;
   public float menuVolume = 0.1f;
   public float effectsVolume = 0.8f;

   AudioSource musicSource;
   AudioSource footStepSource;
   AudioSource effectSource;
   AudioSource menuSource;
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
         musicSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Music")[0];

         effectSource = gameObject.AddComponent<AudioSource>();
         effectSource.clip = levelClear;
         effectSource.volume = effectsVolume;
         effectSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Effects")[0];;

         footStepSource = gameObject.AddComponent<AudioSource>();
         footStepSource.clip = footStep;
         footStepSource.volume = effectsVolume;
         footStepSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Effects")[0];;

         menuSource = gameObject.AddComponent<AudioSource>();
         menuSource.volume = menuVolume;
         menuSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Effects")[0];;

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
   public void PlayMenuSelected()
   {
      menuSource.PlayOneShot(menu[0]);
   }
   public void PlayMenuPressed()
   {
      menuSource.PlayOneShot(menu[1]);
   }
   public void PlayReset()
   {
      effectSource.PlayOneShot(effect[4]);
   }
   public void PlayEffect1()
   {
      menuSource.PlayOneShot(effect[1]);
   }
   public void PlayDeath()
   {
      menuSource.PlayOneShot(effect[3]);
   }
   public void PlayFall()
   {
      effectSource.PlayOneShot(fall[Random.Range(0, fall.Length)]);
   }
   public void PlayFlashlight()
   {
      effectSource.PlayOneShot(flashlight);
   }
}
