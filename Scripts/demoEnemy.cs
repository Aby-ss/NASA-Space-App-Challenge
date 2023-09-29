using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demoEnemy : MonoBehaviour
{
    public float health = 100f; // Initialize enemy health.

    // Function to deduct health.
    public void DeductHealth(float amount)
    {
        health -= amount;

        // Check if the health has reached zero or below.
        if (health <= 0f)
        {
            // Enemy defeated logic (e.g., destroy the enemy).
            Destroy(gameObject);
        }
    }
}
