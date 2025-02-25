using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDmg : MonoBehaviour
{
    public int damage = 10; // Default damage value
    public PlayerHealth playerHealth;
    private bool canAttack = true; // Prevents constant damage
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get enemy's Rigidbody2D
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && canAttack)
        {
            // Stop the enemy from pushing the player by freezing its position
            rb.velocity = Vector2.zero;  // Stop movement
            rb.constraints = RigidbodyConstraints2D.FreezePosition;  // Freeze position

            StartCoroutine(AttackCooldown()); // Start the attack cooldown
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Unfreeze the enemy position when the player moves away
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation; // Keep rotation frozen
        }
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        playerHealth.TakeDamage(damage); // Deal damage to the player
        yield return new WaitForSeconds(3f); // Wait 3 seconds before the next attack
        canAttack = true; // Allow the next attack
    }
}