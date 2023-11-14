using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResizer : MonoBehaviour
{
    public float targetWidth = 640f; // the width of the game area you want to be visible
    public float pixelsToUnits = 100f; // your camera's pixels to units setting

    void Start()
    {
        float desiredHalfHeight = 0.5f * targetWidth / (float)Screen.width * (float)Screen.height;
        Camera.main.orthographicSize = desiredHalfHeight / pixelsToUnits;
    }
}
