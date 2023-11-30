using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerActivityCarnivore : MonoBehaviour
{
    public float hunger = 100f; // Initial hunger level
    public float hungerDecayRate = 1f; // The rate at which hunger decays per second
    private SpriteRenderer spriteRenderer;
    private bool isHungry = false;
    private bool isDying = false;
    private MovementCarnivore movementCarnivore;
    private FallDownBehaviour fallDownBehaviour;
    private bool shouldLoseHunger = true;

    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        fallDownBehaviour = GetComponent<FallDownBehaviour>();
        movementCarnivore = GetComponent<MovementCarnivore>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldLoseHunger)
        {
            hunger -= hungerDecayRate * Time.deltaTime;
        }

        hunger = Mathf.Clamp(hunger, -100f, 300f);

        // Check if the fish is hungry
        if (hunger >= 0f)
        {
            if (hunger < 40f & isHungry == false)
            {
                BecomeHungry();
            }
        }
        else if (isDying == false)
        {
            fallDownBehaviour.setFallingTrue();
            isHungry = false;
            Vector2 currentScale = transform.localScale;
            currentScale.y *= (-1);
            transform.localScale = currentScale;
            isDying = true;
            movementCarnivore.FreezeMovement();
            movementCarnivore.StopCoinDropping();
        }
    }

    void BecomeHungry()
    {
        isHungry = true;
        spriteRenderer.color = Color.green;
    }

    public bool getIsHungry()
    {
        return isHungry;
    }

    public bool getIsDying()
    {
        return isDying;
    }

    public void addToHunger(float value)
    {
        hunger += value;
        if (hunger > 40)
        {
            isHungry = false;
            spriteRenderer.color = Color.white;
        }
    }

    public void pauseHunger(bool pause)
    {
        if (pause)
        {
            shouldLoseHunger = false;
        }
        else
        {
            shouldLoseHunger = true;
        }
    }

    public bool getShouldBeHungry()
    {
        return shouldLoseHunger;
    }
}
