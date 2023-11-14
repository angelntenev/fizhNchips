using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerActivity : MonoBehaviour
{
    public float hunger = 100f; // Initial hunger level
    public float hungerDecayRate = 1f; // The rate at which hunger decays per second
    private SpriteRenderer spriteRenderer;
    private bool isHungry = false;
    private bool isDying = false;
    private ClownfishMovement clownfishMovement;
    private FallDownBehaviour fallDownBehaviour;




    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        clownfishMovement = GetComponent<ClownfishMovement>();
        fallDownBehaviour = GetComponent<FallDownBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        // Reduce hunger over time
        hunger -= hungerDecayRate * Time.deltaTime;

        // Clamp the hunger value to ensure it doesn't go below 0
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
            clownfishMovement.FreezeMovement();
            clownfishMovement.StopCoinDropping();
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
    
}
