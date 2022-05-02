using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
//Resetable
{
    #region Inspector
    
    // oneshots
    [SerializeField] private AudioClip shotMusic;
    [SerializeField] private AudioClip pointMusic;
    [SerializeField] private AudioClip objectBreaksMusic;
    [SerializeField] private AudioClip monsterTrysToAttackMusic;
    [SerializeField] private AudioClip monsterIdle;
    [SerializeField] private AudioClip[] monsterDeathMusic;
    [SerializeField] private AudioClip monsterAttackMusic;
    [SerializeField] private AudioClip meetTheTreeMusic;
    [SerializeField] private AudioClip playerDeathMusic;
    [SerializeField] private AudioClip explosionMusic;
    [SerializeField] private AudioClip doorLockOpenMusic;
    [SerializeField] private AudioClip ammoPickupMusic;
    
    
    // backgrounds
    [SerializeField] private AudioClip bossBattleMusic;
    [SerializeField] private AudioClip endingAndCreditsMusic;
    [SerializeField] private AudioClip gameLoopMusic;
    [SerializeField] private AudioClip pressStartMusic;
    [SerializeField] private AudioClip startSceneMusic;

    
    #endregion

    #region Fields

    private AudioSource _audio;
    private static AudioManager _shared;

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        if (_shared == null)
        {
            _shared = this;
            _audio = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        /*PlayTitleMusic();*/
    }

    #endregion

    #region Methods

        #region BackgroundMusic
        
        public static void PlayBossBattleMusic()
        {
            _shared.SetMusic(_shared.bossBattleMusic);
        }
        
        public static void PlayEndingAndCreditsMusic()
        {
            _shared.SetMusic(_shared.endingAndCreditsMusic);
        }
        
        public static void PlayGameLoopMusic()
        {
            _shared.SetMusic(_shared.gameLoopMusic);
        }
        
        public static void PlayPressStartMusic()
        {
            _shared.SetMusic(_shared.pressStartMusic);
        }
        
        public static void PlayStartSceneMusic()
        {
            _shared.SetMusic(_shared.startSceneMusic);
        }
        
        #endregion

        #region OneShotMusic
        

        public static void PlayShotMusic()
        {
            _shared._audio.PlayOneShot(_shared.shotMusic);
        }
        
        public static void PlayPointMusic()
        {
            _shared._audio.PlayOneShot(_shared.pointMusic);
        }
        
        public static void PlayObjectBreaksMusic()
        {
            _shared._audio.PlayOneShot(_shared.objectBreaksMusic);
        }
        
        public static void PlayMonsterTrysToAttackMusic()
        {
            _shared._audio.PlayOneShot(_shared.monsterTrysToAttackMusic);
        }
        
        public static void PlayMonsterIdle()
        {
            _shared._audio.PlayOneShot(_shared.monsterIdle);
        }
        
        public static void PlayMonsterDeath()
        {
            _shared._audio.PlayOneShot(_shared.monsterDeathMusic[Random.Range(0,4)]);
        }
        
        public static void PlayMonsterAttackMusic()
        {
            _shared._audio.PlayOneShot(_shared.monsterAttackMusic);
        }

        public static void PlayMeetTheTreeMusic()
        {
            _shared._audio.PlayOneShot(_shared.meetTheTreeMusic);
        }
        
        
        public static void PlayPlayerDeathMusic()
        {
            _shared._audio.PlayOneShot(_shared.playerDeathMusic);
        }
        
        
        public static void PlayExplosionMusic()
        {
            _shared._audio.PlayOneShot(_shared.explosionMusic);
        }
        
        public static void PlayDoorLockOpenMusic()
        {
            _shared._audio.PlayOneShot(_shared.doorLockOpenMusic);
        }
        
        public static void PlayAmmoPickupMusic()
        {
            _shared._audio.PlayOneShot(_shared.ammoPickupMusic);
        }
        
        
        

        #endregion

    private void SetMusic(AudioClip music)
    {
        _audio.Stop();
        _audio.clip = music;
        _audio.Play();   
    }

    public void Reset()
    {
        /*PlayTitleMusic();*/
    }

    #endregion
}
