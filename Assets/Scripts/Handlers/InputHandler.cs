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
    public float currentDamage;

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
        GameObject enemyObject = GameObject.FindGameObjectWithTag("Enemy");
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
            if (hit.collider.gameObject.CompareTag("Clownfish") || hit.collider.gameObject.CompareTag("Carnivore"))
            {
                if (GameObject.FindGameObjectsWithTag("Food").Length < 10)
                {
                    Vector2 spawnPosition = _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                    Instantiate(Food, spawnPosition, Quaternion.identity);
                }
            }

            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                ShootEnemy shot = enemyObject.GetComponent<ShootEnemy>();
                shot.Shoot(GetCursorWorldPosition(), currentDamage);
            }
            // If it's not a collectable, don't do anything
        }
        else // If the ray didn't hit anything, we're clicking on empty space
        {
            if (GameObject.FindGameObjectsWithTag("Food").Length < 10)
            {
                Vector2 spawnPosition = _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                Instantiate(Food, spawnPosition, Quaternion.identity);
            }
        }
    }

    private Vector2 GetCursorWorldPosition()
    {
        Vector2 screenPosition = Mouse.current.position.ReadValue();
        return _mainCamera.ScreenToWorldPoint(screenPosition);
    }
}
