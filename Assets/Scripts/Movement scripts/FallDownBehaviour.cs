using System.Collections;
using UnityEngine;

public class FallDownBehaviour : MonoBehaviour
{
    public float fallSpeed; // The speed at which the object will fall
    public float bottomYThreshold = -4.5f; // The y position at which the object will disappear
    public bool isFalling = true;


    // Update is called once per frame
    void Update()
    {
        // If the object is falling, move it down based on fallSpeed
        if (isFalling)
        {
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);
            // Check if the object has reached the bottom
            if (transform.position.y <= bottomYThreshold)
            {
                // Stop the falling
                isFalling = false;
                // Start the coroutine to wait for 3 seconds and then disappear
                StartCoroutine(DisappearAfterSeconds(3));
            }
        }

        
    }

    private IEnumerator DisappearAfterSeconds(float seconds)
    {
        // Wait for the specified amount of seconds
        yield return new WaitForSeconds(seconds);
        // Then deactivate the GameObject
        Destroy(gameObject);

        // If you want to completely destroy the object instead of deactivating it, use:
        // Destroy(gameObject);
    }

    public void setFallingTrue()
    {
        isFalling = true;
    }
}
