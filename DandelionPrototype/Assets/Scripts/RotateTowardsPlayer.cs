using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsPlayer : MonoBehaviour
{
    public Transform player; // Reference to the player object

    void Update()
    {
        // Rotate the object to look at the player
        transform.LookAt(player);
    }
}    