using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Holds the amount of health that the object this is attached to

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;

    // check if the other object is a damage dealer, and if it is, this object takes damage
    void OnTriggerEnter2D(Collider2D other) 
    {
        // if other object has a DamageDealer component, then take damage
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if (damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
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
}
