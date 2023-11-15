using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemy : MonoBehaviour
{
    public float moveSpeed = 5f; // Impulse force applied to the object
    private Rigidbody2D rb;        // Reference to the Rigidbody2D component

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Call this method to shoot the object in the opposite direction of the click
    public void Shoot(Vector2 clickWorldPosition)
    {
        Vector2 objectPosition = transform.position;
        Vector2 directionToClick = (clickWorldPosition - objectPosition);
        // Consider the object's mass so the force is consistent regardless of mass
        Vector2 force = directionToClick.normalized * (moveSpeed / rb.mass);

        // Apply the force inversely to the click direction
        rb.AddForce(-force, ForceMode2D.Impulse);

    }

}