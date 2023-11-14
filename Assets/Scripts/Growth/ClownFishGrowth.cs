using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class ClownFishGrowth : MonoBehaviour
{
    private ClownfishMovement clownfishMovement;

    public float growth = 0f;
    public float growthRate = 5f;
    int stageOfGrowth = 0;
    private bool isMatureForCoins = false;

    private Vector3 fishSizeToIncrease = new Vector3(0.3f, 0.3f, 0.3f);


    public void addToGrowth(float value)
    {
        growth += value;
        doGrow();
    }

    public void doGrow()
    {
        if (stageOfGrowth <= 2)
        {
            if (growth >= 20)
            {
                stageOfGrowth++;
                growth = 0 - (1*stageOfGrowth);
                Vector3 newScale = fishSizeToIncrease;
                newScale.x += fishSizeToIncrease.x;
                newScale.y += fishSizeToIncrease.y;
                newScale.z += fishSizeToIncrease.z;
                Vector3 checkForTurn = transform.localScale;
                if (checkForTurn.x < 0)
                {
                    newScale.x *= (-1);
                    transform.localScale = newScale;
                }
                else transform.localScale = newScale;
                fishSizeToIncrease = new Vector3(0.1f, 0.1f, 0.1f);
            }
            if (isMatureForCoins == false && stageOfGrowth > 0)
            {
                clownfishMovement = GetComponent<ClownfishMovement>();
                clownfishMovement.StartCoinDropping();
                isMatureForCoins = true;
            }
        }

    }

    public bool GetMaturity()
    {
        return isMatureForCoins;
    }


}
