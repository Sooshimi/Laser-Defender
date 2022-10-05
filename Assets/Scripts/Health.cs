using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Holds the amount of health that the object this is attached to
// Only takes damage if the other object is a DamageDealer
// Instantiates particle effects when hit
// Camerashake when hit
// Adds score for every enemy destroyed

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer; // for scoring system. Set as true for player in Unity Inspector
    [SerializeField] int score = 50;
    [SerializeField] int health = 50;
    [SerializeField] ParticleSystem hitEffect;

    [SerializeField] bool applyCameraShake; // camerashake only for player
    CameraShake cameraShake;

    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;

    void Awake() 
    {
        // Camera.main already has a FindObjectOfType built into it
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    // check if the other object is a damage dealer, and if it is, this object takes damage
    void OnTriggerEnter2D(Collider2D other) 
    {
        // if other object has a DamageDealer component, then take damage
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        // take damage
        if (damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            audioPlayer.PlayDamageClip();
            ShakeCamera();
            damageDealer.Hit();
        }
    }

    public int GetHealth()
    {
        return health;
    }

    void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
            {
                Die();
            }
    }

    void Die()
    {
        if (!isPlayer)
        {
            scoreKeeper.ModifyScore(score);
        }
        Destroy(gameObject);
    }

    void PlayHitEffect()
    {
        if(hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);

            // destroy particle with a delay of the lifetime of the particle effect
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    void ShakeCamera()
    {
        if (cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }
}
