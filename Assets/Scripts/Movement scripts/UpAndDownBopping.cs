using UnityEngine;

public class UpAndDownBopping : MonoBehaviour
{
    public float amplitude = 0.5f;
    public float frequency = 1f;

    private float tempVal;
    private Vector3 tempPos;

    void Start()
    {
        // Store the starting position & y-offset of the object
        tempVal = transform.position.y;
    }

    void Update()
    {
        // Calculate the temporary Y value using a sine wave
        tempPos.y = tempVal + amplitude * Mathf.Sin(frequency * Time.time);

        // Apply the Y offset to the object's current position, leaving X & Z the same
        transform.position = new Vector3(transform.position.x, tempPos.y, transform.position.z);
    }
}
