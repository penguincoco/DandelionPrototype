using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject saplingPrefab;
    //[SerializeField] private Vector2 xBounds;
    //[SerializeField] private Vector2 yBounds;
    [SerializeField] private float neighborhoodBounds;
    [SerializeField] private float personalSpace; //flowers shouldn't spawn too close to each other. they each have a personal bubble :)
    private int spawnAttempts;
    [SerializeField] private int maxSpawnAttempts;

    private void Update()
    {
    }

    public void SpawnSapling(Vector3 neighborhood)
    {
        GameObject newSapling = Instantiate(saplingPrefab);
        //float randomX = Random.Range(xBounds.x, xBounds.y);
        //float randomY = Random.Range(yBounds.x, yBounds.y);

        //float randomX = Random.Range(neighborhood.x - neighborhoodBounds, neighborhood.x + neighborhoodBounds);
        //float randomZ = Random.Range(neighborhood.z - neighborhoodBounds, neighborhood.z + neighborhoodBounds);
        bool positionOutcome = SetRandomPosition(neighborhood, newSapling);
        //newSapling.transform.position = randomSpawnPos;

        if (positionOutcome == false)
            return;
        else
        {
            //play the sound for spawning 
            newSapling.GetComponent<FlowerObject>().SetAudio(1.1f, false);
            GM_ForgetMeNot.Instance.SetLatestBaby(newSapling);
            GM_ForgetMeNot.Instance.UpdateFlowerCounter();
        }
    }

    public bool SetRandomPosition(Vector3 neighborhood, GameObject spawnedObject)
    {
        do
        {
            float randomX = GetRandomValue(neighborhood.x - neighborhoodBounds, neighborhood.x + neighborhoodBounds, neighborhood.x);
            float randomZ = GetRandomValue(neighborhood.z - neighborhoodBounds, neighborhood.z + neighborhoodBounds, neighborhood.z);
            Vector3 randomSpawnPos = new Vector3(randomX, -1, randomZ);
            spawnedObject.transform.position = randomSpawnPos;

            spawnAttempts++;
        } while (CheckForOtherObjects(spawnedObject) && spawnAttempts < maxSpawnAttempts);

        if (spawnAttempts >= maxSpawnAttempts)
        {
            if (CheckForOtherObjects(spawnedObject) == true)
            {
                Destroy(spawnedObject);
                return false;
            }
            else
                return true;
        }

        return false;
    }

    private bool CheckForOtherObjects(GameObject spawnedObject)
    {
        Collider[] colliders = Physics.OverlapSphere(spawnedObject.transform.position, personalSpace);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject != spawnedObject && collider.gameObject.CompareTag("Flower"))
            {
                return true;
            }
        }
        return false;
    }

    private float GetRandomValue(float min, float max, float originValue)
    {
        float randomValue = Random.Range(min, max);
        float bufferMin = originValue - 4f;
        float bufferMax = originValue + 4f;

        do
        {
            randomValue = Random.Range(min, max); // Generate a random number within the range [min, max)
        } while (randomValue >= bufferMin && randomValue <= bufferMax);

        return randomValue;
    }
}
