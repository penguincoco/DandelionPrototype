using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindArea : MonoBehaviour
{

    public float windStrength = 10f;

    [SerializeField] private float baseWindStrength;
    [SerializeField] private float maxGustStrength = 20f;
    [SerializeField] private float gustInterval = 5f;
    [SerializeField] private float gustDuration = 2f;
    [SerializeField] private float declerationRate;
    [SerializeField] private float gustDeclerationTime;

    private float nextGustTime;
    private bool isGusting;

    private bool playerInZone = false;

    private Rigidbody playerRB;

    // Direction of the wind force
    public Vector3 windDirection = Vector3.forward;

    // Called when another Collider enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Rigidbody>() != null)
        {
            playerRB = other.gameObject.GetComponent<Rigidbody>();
            playerInZone = true;
        }
    }

    private void Update()
    {
        if (playerInZone == true)
        //if (isGusting == true)
        {
            playerRB.AddForce(windDirection * windStrength, ForceMode.Force);
        } 

        if (Input.GetKeyDown(KeyCode.T))
        {
            isGusting = true;
            StartCoroutine(StartGust());
        }
    }

    private IEnumerator StartGust()
    {
        windStrength = maxGustStrength;

        // Wait for the gust duration
        yield return new WaitForSeconds(gustDuration);

        float elapsedTime = 0f;
        float rate = (maxGustStrength - baseWindStrength) / gustDeclerationTime; // Calculate the rate of change

        while (elapsedTime < gustDeclerationTime)
        {
            // Decrease the value
            float currentValue = Mathf.Lerp(maxGustStrength, baseWindStrength, elapsedTime / gustDeclerationTime);
            windStrength = currentValue;

            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        isGusting = false;
        windStrength = baseWindStrength;
    }
}
