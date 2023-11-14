using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu]
public class ObjectAssets : ScriptableObject
{
    public string nameOfCollectable;
    public TagAttribute tagman;
    public Sprite artwork;
    public int value;
    public int growthValue;
}
