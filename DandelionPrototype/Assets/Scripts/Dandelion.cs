using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class Dandelion : MonoBehaviour
{
    [SerializeField] SplineAnimate splineCurve;
    [SerializeField] private float minDuration;
    [SerializeField] private float maxDuration;
    [SerializeField] private float totalTime;
    private Vector2 timeRange;

    private int cycles;
    private int cyclesCounter;

    public void Constructor(SplineContainer splineCurve, float minDuration, float maxDuration, float minRange, float maxRange, int cycles)
    {
        this.splineCurve.Container = splineCurve;
        this.minDuration = minDuration;
        this.maxDuration = maxDuration;
        this.timeRange.x = minRange;
        this.timeRange.y = maxRange;
        this.cycles = cycles;

        StartCoroutine(ChangeSpeedOverTime());
    }

    IEnumerator ChangeSpeedOverTime()
    {
        float elapsedTime = 0f;

        while (elapsedTime < totalTime)
        {
            float normalizedTime = Mathf.Clamp01(elapsedTime / totalTime);
            float speedMultiplier = Mathf.Sin(normalizedTime * Mathf.PI); // Sine curve from 0 to 1

            // Calculate the speed based on the speed multiplier
            float speed = Mathf.Lerp(minDuration, maxDuration, speedMultiplier);
            splineCurve.Duration = speed;

            // Apply the speed to your game object, for example, move it
            //transform.Translate(Vector3.forward * speed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            cyclesCounter++;
            yield return null;
        }

        totalTime = Random.Range(timeRange.x, timeRange.y);

        if (cyclesCounter <= cycles)
        {
            StartCoroutine(ChangeSpeedOverTime());
        }
    }
}
