using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bounce : MonoBehaviour
{
    public float bounceForce = 10f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the colliding object has a Rigidbody
        Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Calculate the direction of the collision
            Vector2 direction = (collision.transform.position - transform.position).normalized;

            // Apply a force opposite to the collision direction to bounce the object back
            rb.AddForce(direction * bounceForce, ForceMode2D.Impulse);
        }
    }
}

