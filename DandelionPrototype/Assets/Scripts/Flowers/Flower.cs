using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Flower", menuName = "Waterable Object/Flower", order = 1)]
public class Flower : ScriptableObject
{
    public GameObject[] growthStates;
    public GameObject wiltState;
    public float[] waterStates;
    public float[] lightStates;
    public AudioClip blossomSound;

    public bool needsLight;
    public bool needsWater;

    public float wiltTimer;     //put 0 for flowers that can't wilt
}
