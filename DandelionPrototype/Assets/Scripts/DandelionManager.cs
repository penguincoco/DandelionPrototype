using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class DandelionManager : MonoBehaviour
{
    [SerializeField] private int numberOfDandelions;
    [SerializeField] private GameObject dandelionPrefab;
    [SerializeField] Dandelion[] dandelions;
    [SerializeField] SplineContainer[] splineCurves;
    [SerializeField] private Vector2 durationRange;
    [SerializeField] private Vector2 timeRange;
    [SerializeField] private Vector2 cyclesRange;
    [SerializeField] private Vector2 sizeRange;
    [SerializeField] private Vector2 spawnTimeRange;

    private void Start()
    {
        StartCoroutine(SpawnOverTime());
    }

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
    }
}
