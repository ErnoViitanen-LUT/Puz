using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public static AudioManager Instance { get { return _instance; } }
    public AudioClip musicClip;
    public bool musicOn = true;
    public AudioClip effectClip;
    public AudioClip footStep;
    public AudioClip levelClear;
    public AudioClip jumpStart;
    public AudioClip jumpEnd;

    AudioSource musicSource;
    AudioSource footStepSource;
    AudioSource levelClearSource;
     private void Awake()
     {
         if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.clip = musicClip;
            musicSource.loop = true;

            levelClearSource = gameObject.AddComponent<AudioSource>();  
            levelClearSource.clip = levelClear;
            
            footStepSource = gameObject.AddComponent<AudioSource>();            
            footStepSource.clip = footStep;
            DontDestroyOnLoad(transform.gameObject);            
            PlayMusic();
        }
         
     }
 
     public void PlayMusic()
     {
        if (!musicSource.isPlaying && musicOn){
             musicSource.Play();
        }
       /* if (!effectSource.isPlaying){
             effectSource.Play();
        }*/
     }
 
     public void StopMusic()
     {
         musicSource.Stop();
         //effectSource.Stop();
     }
     public void PlayLevelClear(){
         levelClearSource.Play();
     }
     public void PlayFootStep(){
         footStepSource.Play();
     }
     public void PlayJumpStart(){         
            levelClearSource.PlayOneShot(jumpStart);
     }
     public void PlayJumpEnd(){      
            levelClearSource.PlayOneShot(jumpEnd);
     }
}
