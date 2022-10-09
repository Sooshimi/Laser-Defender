using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField] [Range(0f, 1f)] float shootingVolume = 1f; // constrained between 0 - 100%

    [Header("Damage")]
    [SerializeField] AudioClip damageClip;
    [SerializeField] [Range(0f, 1f)] float damageVolume = 1f; // constrained between 0 - 100%

    // static variables persists through all instances of a class
    // e.g. all AudioPlayer's in the scene will share this exact static instance
    static AudioPlayer instance;
    
    void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton() // allows audio player to persist to next scene
    {
        if (instance != null) // if this is NOT the first AudioPlayer to be instantiated, then destroy the audio
        {
            // disables it before destroying it on awake, in case other objects use the audio player
            gameObject.SetActive(false); 
            Destroy(gameObject);
        }
        else // if this is the first AudioPlayer to be instantiated, then persist it
        {
            instance = this; // then set the instance as this AudioPlayer (to persist it)
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayShootingClip()
    {
        PlayClip(shootingClip, shootingVolume);
    }

    public void PlayDamageClip()
    {
        PlayClip(damageClip, damageVolume);
    }

    void PlayClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;

            // instantiate the audio clip into the world at the camera position
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }
}
