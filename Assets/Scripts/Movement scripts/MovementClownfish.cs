using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownfishMovement : MonoBehaviour
{
    public GameObject collectablePrefab;
    public Vector2 spawnOffset = new Vector2(0, -0.5f);

    private bool arrivedAtDestination;
    [HideInInspector]
    public bool waiting;

    private Vector2 destinationPosition;
    private Vector2 currentPosition;
    private float currentXCoordinate;
    private bool facingRight = true;
    float arrivalThreshold = 0.1f;
    public float minWaitTime;
    public float maxWaitTime;

    public float speed;
    private float tempSpeed;

    private HungerActivity hungerActivity;
    private ClownFishGrowth clownFishGrowth;

     private float saveInterval = 10f;  //30 SEC       // 600f; // 10 minutes
    private float nextSaveTime;

    // Unique identifier for each clownfish instance
    private static int nextId = 0;
    private int id;



    void Awake()
    {
        id = nextId++;
    }


    // Start is called before the first frame update
    void Start()
    {

        
       //LoadPosition();
       // nextSaveTime = Time.time + saveInterval;
       // SetRandomStartPosition();
       // transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
       // currentXCoordinate = transform.position.x;
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
        hungerActivity = GetComponent<HungerActivity>();
        clownFishGrowth = GetComponent<ClownFishGrowth>();
    }

    // Update is called once per frame
    void Update()
    {
        //Updating the next save time
        // if (Time.time >= nextSaveTime)
        // {
        //     SavePosition();
        //     nextSaveTime = Time.time + saveInterval;
        // }
        SavePosition();

       

            float movementSpeed = speed * Time.deltaTime;
            if (!hungerActivity.getIsDying())
            {
                if (!hungerActivity.getIsHungry())
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
                            hungerActivity.addToHunger(foodDisplay.GetValue());
                            clownFishGrowth.addToGrowth(foodDisplay.GetGrowthValue());
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

    

    //Storing ClownFishPosition
    private void SavePosition()
    {
        PlayerPrefs.SetString("ClownfishPosition_" + id, SerializeVector(transform.position));
    }
    //Loading it
    private void LoadPosition()
    {
        string savedPosition = PlayerPrefs.GetString("ClownfishPosition_" + id, string.Empty);
        if (!string.IsNullOrEmpty(savedPosition))
        {
            Vector3 position = DeserializeVector(savedPosition);
            transform.position = position;
        }
    }

    //Data Serialization and Deserialization.
    private string SerializeVector(Vector3 vector)
    {
        return vector.x + "," + vector.y + "," + vector.z;
    }
    private Vector3 DeserializeVector(string data)
    {
        string[] values = data.Split(',');
        if (values.Length == 3)
        {
            return new Vector3(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]));
        }
        return Vector3.zero;
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
        GameObject[] foods = GameObject.FindGameObjectsWithTag("Food");
        GameObject closest = null;
        float closestDistance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (GameObject food in foods)
        {
            Vector3 directionToFood = food.transform.position - position;
            float distanceToFood = directionToFood.sqrMagnitude;
            if (distanceToFood < closestDistance)
            {
                closestDistance = distanceToFood;
                closest = food;
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
        currentScale.x = Mathf.Abs(currentScale.x);
        transform.localScale = currentScale;
        facingRight = false;
    }

    void FlipToLeft()
    {
        Vector2 currentScale = transform.localScale;
        if (currentScale.x > 0) 
        {
            currentScale.x *= -1;
            transform.localScale = currentScale;
        }
        facingRight = true;
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

    public bool getFishMaturity()
    {
        clownFishGrowth = GetComponent<ClownFishGrowth>();
        return clownFishGrowth.GetMaturity();
    }
}
