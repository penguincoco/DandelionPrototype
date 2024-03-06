using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class DandelionManager : MonoBehaviour
{
    [SerializeField] private int numberOfDandelions;
    //[SerializeField] private GameObject dandelionPrefab;
    [SerializeField] private GameObject[] dandelionPrefabs;
    [SerializeField] private Vector2 speedRange;

    [SerializeField] Dandelion[] dandelions;
    [SerializeField] SplineContainer[] splineCurves;
    [SerializeField] private Vector2 durationRange;
    [SerializeField] private Vector2 timeRange;
    [SerializeField] private Vector2 cyclesRange;
    [SerializeField] private Vector2 sizeRange;
    [SerializeField] private Vector2 spawnTimeRange;
    [SerializeField] private float initialWaitTime;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip windGust;
    [SerializeField] private GameObject spawnLocation;

    private void Start()
    {
        //StartCoroutine(SpawnOverTime());
    }

    public void StartDandelionFlow()
    {
        StartCoroutine(SpawnOverTime());
    }

    private IEnumerator SpawnOverTime()
    {
        yield return new WaitForSeconds(initialWaitTime / 2f);

        audioSource.PlayOneShot(windGust);

        yield return new WaitForSeconds(initialWaitTime / 2f);

        for (int i = 0; i < numberOfDandelions; i++)
        {
            int randomDandelion = Random.Range(0, dandelionPrefabs.Length);
            GameObject newDandelion = Instantiate(dandelionPrefabs[randomDandelion], spawnLocation.transform);

            newDandelion.GetComponent<Animator>().Rebind();
            float randomScale = Random.Range(sizeRange.x, sizeRange.y);
            newDandelion.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
            float randomSpeed = Random.Range(speedRange.x, speedRange.y);
            newDandelion.GetComponent<Animator>().speed = randomSpeed;

            yield return new WaitForSeconds(Random.Range(spawnTimeRange.x, spawnTimeRange.y));
        }
    }

    /*
    private IEnumerator SpawnOverTime()
    {
        for (int i = 0; i < numberOfDandelions; i++)
        {
            GameObject newDandelion = Instantiate(dandelionPrefab);
            float randomScale = Random.Range(sizeRange.x, sizeRange.y);
            newDandelion.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

            SplineContainer randomCurve = splineCurves[Random.Range(0, splineCurves.Length)];

            Dandelion dandelionScript = newDandelion.gameObject.GetComponent<Dandelion>();

            dandelionScript.Constructor(
                randomCurve,
                Random.Range(durationRange.x, durationRange.y),
                Random.Range(durationRange.x, durationRange.y),
                Random.Range(timeRange.x, timeRange.y),
                Random.Range(timeRange.x, timeRange.y),
                (int)Random.Range(cyclesRange.x, cyclesRange.y));

            yield return new WaitForSeconds(Random.Range(spawnTimeRange.x, spawnTimeRange.y));
        }
    } */
}
