using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDisplay : MonoBehaviour
{
    public ObjectAssets collectable;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }

    public int GetValue()
    {
        return collectable.value;
    }

    public int GetGrowthValue()
    {
        return collectable.growthValue;
    }

}
