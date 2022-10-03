using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Holds the amount of health that the object this is attached to
// Only takes damage if the other object is a DamageDealer
// Instantiates particle effects when hit

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;
    [SerializeField] ParticleSystem hitEffect;

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
            damageDealer.Hit();
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
            {
                Destroy(gameObject);
            }
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
}
