using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public List<GameObject> bossEnemy;
    int bossID = 0;
    private bool activeBoss = false;
    private GameObject[] clownfish;
    private GameObject[] carnivores;


    // Start is called before the first frame update
    void Start()
    {
        spawnBossAfterSeconds(150);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnBoss()
    {
        clownfish = GameObject.FindGameObjectsWithTag("Clownfish");
        carnivores = GameObject.FindGameObjectsWithTag("Carnivore");
        for (int i = 0; i < clownfish.Length; i++)
        {
            clownfish[i].GetComponent<HungerActivityClownFish>().pauseHunger(true);
            clownfish[i].GetComponent<ClownfishMovement>().setBossActive(true);
        }
        for (int i = 0; i < carnivores.Length; i++)
        {
            carnivores[i].GetComponent<HungerActivityClownFish>().pauseHunger(true);
            carnivores[i].GetComponent<MovementCarnivore>().setBossActive(true);
        }
        switch (bossID)
        {
            case 0:
                float minX = -10f;
                float maxX = 10f;
                float minY = -5f;
                float maxY = 5f;
                // Choose a random x position between minX and maxX.
                float randomX = Random.Range(minX, maxX);
                float randomY = Random.Range(minY, maxY);
                Instantiate(bossEnemy[bossID], new Vector2(randomX,randomY), Quaternion.identity);
                break;
                
        }
    }

    public void spawnBossAfterSeconds(float seconds)
    {
        Invoke("spawnBoss", seconds);
    }

    public void resetFish()
    {
        for (int i = 0; i < clownfish.Length; i++)
        {
            clownfish[i].GetComponent<HungerActivityClownFish>().pauseHunger(false);
            clownfish[i].GetComponent<ClownfishMovement>().setBossActive(false);
        }
        for (int i = 0; i < carnivores.Length; i++)
        {
            carnivores[i].GetComponent<HungerActivityClownFish>().pauseHunger(false);
            carnivores[i].GetComponent<MovementCarnivore>().setBossActive(false);
        }
    }
}
