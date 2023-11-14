using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCarnivore : MonoBehaviour
{
    public GameObject collectablePrefab;
    public Vector2 spawnOffset = new Vector2(0, -0.5f);

    private bool arrivedAtDestination;
    [HideInInspector]
    public bool waiting;

    public Vector2 destinationPosition;
    private Vector2 currentPosition;
    private float currentXCoordinate;
    private bool facingRight = true;
    float arrivalThreshold = 0.1f;
    public float minWaitTime;
    public float maxWaitTime;

    public float speed;
    private float tempSpeed;

    private HungerActivityCarnivore hungerActivityCarnivore;

    // Start is called before the first frame update
    void Start()
    {
        SetRandomStartPosition();
        transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        currentXCoordinate = transform.position.x;
        destinationPosition = GetRandomScreenPosition();
        if (currentXCoordinate > destinationPosition.x)
        {
            FlipToLeft();
        }
        if (currentXCoordinate < destinationPosition.x)
        {
            FlipToRight();
        }
        arrivedAtDestination = false;
        waiting = false;
        tempSpeed = speed;
        hungerActivityCarnivore = GetComponent<HungerActivityCarnivore>();
        StartCoinDropping();
    }

    // Update is called once per frame
    void Update()
    {
        float movementSpeed = speed * Time.deltaTime;
        if (!hungerActivityCarnivore.getIsDying())
        {
            if (!hungerActivityCarnivore.getIsHungry())
            {
                if (!arrivedAtDestination)
                {
                    transform.position = Vector2.MoveTowards(transform.position, destinationPosition, movementSpeed);
                    if (Vector2.Distance(transform.position, destinationPosition) < arrivalThreshold)
                    {
                        arrivedAtDestination = true; // Set the flag to true when destination is reached
                        speed = 0; // Stop the movement
                        StartCoroutine(WaitAtDestination(Random.Range(minWaitTime, maxWaitTime)));
                    }
                }
            }
            else
            {
                GameObject closestFood = FindClosestFood();
                if (closestFood != null)
                {
                    speed = tempSpeed;
                    // Move towards the closest food object
                    destinationPosition = closestFood.transform.position;
                    transform.position = Vector2.MoveTowards(transform.position, destinationPosition, movementSpeed);
                    if (Vector2.Distance(transform.position, destinationPosition) < arrivalThreshold)
                    {
                        FoodDisplay foodDisplay = closestFood.GetComponent<FoodDisplay>();
                        hungerActivityCarnivore.addToHunger(150);
                        Destroy(closestFood);
                        arrivedAtDestination = true; // Set the flag to true when destination is reached
                        speed = 0; // Stop the movement
                        StartCoroutine(WaitAtDestination(1));
                    }
                }
                else
                {
                    if (!arrivedAtDestination)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, destinationPosition, movementSpeed);
                        if (Vector2.Distance(transform.position, destinationPosition) < arrivalThreshold)
                        {
                            arrivedAtDestination = true; // Set the flag to true when destination is reached
                            speed = 0; // Stop the movement
                            StartCoroutine(WaitAtDestination(Random.Range(minWaitTime, maxWaitTime)));
                        }
                    }
                }
            }
        }
    }

    Vector2 GetRandomScreenPosition()
    {
        // Get the corners of the screen in world space
        Vector2 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector2 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.nearClipPlane));

        // Generate a random position within the screen bounds
        float randomX = Random.Range(bottomLeft.x, topRight.x);
        float randomY = Random.Range(bottomLeft.y, topRight.y);



        return new Vector2(randomX, randomY);
    }


    IEnumerator WaitAtDestination(float seconds)
    {
        currentXCoordinate = transform.position.x;
        yield return new WaitForSeconds(seconds);
        arrivedAtDestination = false; // Reset the flag after waiting
        destinationPosition = GetRandomScreenPosition(); // Pick a new destination
        if (currentXCoordinate > destinationPosition.x)
        {
            FlipToLeft();
        }
        if (currentXCoordinate < destinationPosition.x)
        {
            FlipToRight();
        }
        speed = tempSpeed; // Reset the speed
    }



    GameObject FindClosestFood()
    {
        GameObject[] foods = GameObject.FindGameObjectsWithTag("Clownfish");
        GameObject closest = null;
        float closestDistance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (GameObject food in foods)
        {
            ClownfishMovement clownfishMovement = food.GetComponent<ClownfishMovement>();
            if (clownfishMovement != null && !clownfishMovement.getFishMaturity())
            {
                Vector3 directionToFood = food.transform.position - position;
                float distanceToFood = directionToFood.sqrMagnitude;
                if (distanceToFood < closestDistance)
                {
                    closestDistance = distanceToFood;
                    closest = food;
                }
            }
        }
        if (closest != null)
        {
            currentXCoordinate = transform.position.x;
            if (currentXCoordinate > closest.transform.position.x)
            {
                FlipToLeft();
            }
            if (currentXCoordinate < closest.transform.position.x)
            {
                FlipToRight();
            }
        }
        return closest;
    }

    void FlipToRight()
    {
        Vector2 currentScale = transform.localScale;
        if (currentScale.x > 0)
        {
            currentScale.x *= -1;
            transform.localScale = currentScale;
        }
        facingRight = true;
    }

    void FlipToLeft()
    {
        Vector2 currentScale = transform.localScale;
        currentScale.x = Mathf.Abs(currentScale.x);
        transform.localScale = currentScale;
        facingRight = false;
    }

    private void SetRandomStartPosition()
    {
        float minX = 0f;
        float maxX = 10f;
        // Choose a random x position between minX and maxX.
        float randomX = Random.Range(minX, maxX);

        // Set the GameObject's position to the random x and 0 on the y-axis.
        transform.position = new Vector2(randomX, 0f);
    }

    public void FreezeMovement()
    {
        speed = 0f;
    }

    public GameObject getCollectableType()
    {
        return collectablePrefab;
    }

    public void SpawnCollectable()
    {
        Vector2 spawnPosition = new Vector2(transform.position.x + spawnOffset.x, transform.position.y + spawnOffset.y);
        Instantiate(collectablePrefab, spawnPosition, Quaternion.identity);
    }

    public void StartCoinDropping()
    {
        InvokeRepeating("SpawnCollectable", 3f, 10f);
    }
    public void StopCoinDropping()
    {
        CancelInvoke("SpawnCollectable");
    }
}
