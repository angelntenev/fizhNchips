using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovementEnemy1 : MonoBehaviour
{
    private Vector2 destinationPosition;
    private bool facingRight = true;
    private float currentXCoordinate;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float movementSpeed = speed * Time.deltaTime;
        GameObject closestFood = FindClosestFood();
        destinationPosition = closestFood.transform.position;
        transform.position = Vector2.MoveTowards(transform.position, destinationPosition, movementSpeed);

    }

    GameObject FindClosestFood()
    {
        List<GameObject> foods = new List<GameObject>();
        foods.AddRange(AddObjectsWithTag("Clownfish"));
        foods.AddRange(AddObjectsWithTag("Carnivore"));
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

    List<GameObject> AddObjectsWithTag(string tag)
    {
        List<GameObject> taggedObjects = new List<GameObject>();
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
        foreach (var obj in objectsWithTag)
        {
            taggedObjects.Add(obj);
        }
        return taggedObjects;
    }

}
