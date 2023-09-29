using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demoController : MonoBehaviour
{
    Vector2 movement;
    public float moveSpeed = 5f;
    public float damageAmount = 5f; // Amount of damage to deduct.

    public Rigidbody2D rb;
    public Animator animator;

    public KeyCode attackKey = KeyCode.Space; // Define the key to trigger the attack.
    public GameObject attackableObject; // The object to attack.
    public string targetTag = "Enemy"; // Define the tag to check for.
    public demoEnemy enemy; // Reference to the demoEnemy script.


    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        // Check if the attack key is pressed and the attackable object is in contact with the "enemy" tag.
    if (Input.GetKeyDown(attackKey) && IsAttackableInContact())
    {
        Debug.Log("Attack");
        DeductEnemyHealth();
    }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private bool IsAttackableInContact()
    {
        // Check if the attackable object is in contact with something.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackableObject.transform.position, 0.5f); // Adjust the radius as needed.

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy")) // Check if it's an enemy.
            {
                return true;
            }
        }

        return false;
    }

    private void DeductEnemyHealth()
        {
            // Find all colliders in the attackableObject's position.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(attackableObject.transform.position, 0.5f); // Adjust the radius as needed.

            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Enemy")) // Check if it's an enemy.
                {
                    enemy.DeductHealth(5f); // Deduct 5 from the enemy's health using the reference.
                }
            }
        }
}