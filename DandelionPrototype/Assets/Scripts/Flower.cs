using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Flower", menuName = "Waterable Object/Flower", order = 1)]
public class Flower : ScriptableObject
{
    public GameObject[] growthStates;
    public float[] waterStates;
    public AudioClip blossomSound;

    public float requiredWater;
}
