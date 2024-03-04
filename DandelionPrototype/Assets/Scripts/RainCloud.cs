using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raincloud : MonoBehaviour
{
    [SerializeField] private float repulsionForce = 10f;
    [SerializeField] private float repulsionRange = 5f;

    [SerializeField] private float rainRate;
    private GameObject flowerObject;
    private bool isWateringFlower;

    private void Update()
    {
        if (isWateringFlower == true)
            Water();
    }

    private void FixedUpdate()
    {
        // Get the position of the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return; // If player not found, exit

        // Calculate the distance between the player and the object
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // Check if the player is within the repulsion range
        if (distanceToPlayer <= repulsionRange)
        {
            // Calculate direction from the object to the player
            Vector3 direction = transform.position - player.transform.position;

            // Apply force in the direction opposite to the player
            GetComponent<Rigidbody>().AddForce(direction.normalized * repulsionForce, ForceMode.Force);
        }
    }

    private void Water()
    {
        flowerObject.gameObject.GetComponent<WaterableObject>().SetWater(rainRate);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, repulsionRange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<WaterableObject>() != null)
        {
            isWateringFlower = true;
            flowerObject = other.gameObject;
        }
    }
}
