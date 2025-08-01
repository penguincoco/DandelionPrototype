using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject saplingPrefab;
    //[SerializeField] private Vector2 xBounds;
    //[SerializeField] private Vector2 yBounds;
    [SerializeField] private float neighborhoodOuterBounds;
    [SerializeField] private float neighborhoodInnerBounds;
    [SerializeField] private float personalSpace; //flowers shouldn't spawn too close to each other. they each have a personal bubble :)
    private int spawnAttempts;
    [SerializeField] private int maxSpawnAttempts;

    public void SpawnSapling(Vector3 neighborhood, bool isSelfGrowing)
    {
        GameObject newSapling = Instantiate(saplingPrefab);
        //float randomX = Random.Range(xBounds.x, xBounds.y);
        //float randomY = Random.Range(yBounds.x, yBounds.y);

        //float randomX = Random.Range(neighborhood.x - neighborhoodOuterBounds, neighborhood.x + neighborhoodOuterBounds);
        //float randomZ = Random.Range(neighborhood.z - neighborhoodOuterBounds, neighborhood.z + neighborhoodOuterBounds);
        bool positionOutcome = SetRandomPosition(neighborhood, newSapling);
        //newSapling.transform.position = randomSpawnPos;

        //if it had to destroy a baby because of position overlap, return
        if (positionOutcome == false)
            return;
        else
        {
            newSapling.GetComponent<FlowerObject>().SetSelfGrowing(isSelfGrowing);
            //play the sound for spawning 
            newSapling.GetComponent<FlowerObject>().SetAudio(1.1f, true, 1f);

            GM_ForgetMeNot.Instance.SetLatestBaby(newSapling);
            GM_ForgetMeNot.Instance.UpdateFlowerCounter();
            GM_ForgetMeNot.Instance.UpdateFlowerScalar();
        }
    }

    public bool SetRandomPosition(Vector3 neighborhood, GameObject spawnedObject)
    {
        do
        {
            float randomX = GetRandomValue(neighborhood.x - neighborhoodOuterBounds, neighborhood.x + neighborhoodOuterBounds, neighborhood.x);
            float randomZ = GetRandomValue(neighborhood.z - neighborhoodOuterBounds, neighborhood.z + neighborhoodOuterBounds, neighborhood.z);
            Vector3 randomSpawnPos = new Vector3(randomX, -1, randomZ);
            spawnedObject.transform.position = randomSpawnPos;

            spawnAttempts++;
        } while (CheckForOtherObjects(spawnedObject) && spawnAttempts < maxSpawnAttempts);

        if (spawnAttempts >= maxSpawnAttempts)
        {
            if (CheckForOtherObjects(spawnedObject) == true)
            {
                Debug.Log("too close to another object, object destroyed");
                Destroy(spawnedObject);
                return false;
            }
        }

        return true;
    }

    private bool CheckForOtherObjects(GameObject spawnedObject)
    {
        Collider[] colliders = Physics.OverlapSphere(spawnedObject.transform.position, personalSpace);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject != spawnedObject && collider.gameObject.CompareTag("Flower") && collider.gameObject.CompareTag("Water Source"))
            {
                return true;
            }
        }
        return false;
    }

    private float GetRandomValue(float min, float max, float originValue)
    {
        float randomValue = Random.Range(min, max);
        float bufferMin = originValue - neighborhoodInnerBounds;
        float bufferMax = originValue + neighborhoodInnerBounds;

        do
        {
            randomValue = Random.Range(min, max); // Generate a random number within the range [min, max)
        } while (randomValue >= bufferMin && randomValue <= bufferMax);

        return randomValue;
    }
}
