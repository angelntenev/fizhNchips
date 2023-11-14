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
        Vector2 directionToClick = (clickWorldPosition - objectPosition).normalized;
        Vector2 moveDirection = -directionToClick; // Opposite direction

        // Apply an impulse force to move the object
        rb.AddForce(moveDirection * moveSpeed, ForceMode2D.Impulse);
    }
}