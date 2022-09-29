using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Holds the amount of attack damage that the object this is attached to

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 10;

    public int GetDamage()
    {
        return damage;
    }

    public void Hit() // destroys THIS object of it is hit once
    {
        Destroy(gameObject);
    }
}
