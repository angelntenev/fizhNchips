using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private Camera _mainCamera;
    public MoneyManager MoneyManager;
    public ObjectAssets CoinValues;
    public GameObject Food;
    public GameObject buyOption1;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }

        // Create a ray from the camera to our click position
        Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        // Check if the ray hit a collider
        if (hit.collider != null)
        {
            // If it's a collectable, then add money and destroy it
            if (hit.collider.gameObject.CompareTag("Collectable"))
            {
                MoneyManager.AddMoney(CoinValues.value);
                Destroy(hit.collider.gameObject);
            }
            // If it's not a collectable, don't do anything
        }
        else // If the ray didn't hit anything, we're clicking on empty space
        {
            // Convert the mouse position to a point in world space
            Vector2 spawnPosition = _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            // Instantiate the object at that position
            Instantiate(Food, spawnPosition, Quaternion.identity);
        }
    }
}
