using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject playerTarget;
    [SerializeField] private float animationTime;

    public void MoveWrapper()
    {
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        Transform playerStartPos = playerObject.transform;
        Transform playerTargetPos = playerTarget.transform;

        // Record the start time
        float startTime = Time.time;

        // Loop until the interpolation is complete
        while (Time.time - startTime < animationTime)
        {
            // Calculate the interpolation factor
            float t = (Time.time - startTime) / animationTime;

            // Lerp position
            playerObject.transform.position = Vector3.Lerp(playerStartPos.position, playerTargetPos.position, t);

            // Lerp rotation
            playerObject.transform.rotation = Quaternion.Lerp(playerStartPos.rotation, playerTargetPos.rotation, t);

            // Wait for the next frame
            yield return null;
        }

        // Ensure the final position and rotation match the end transform exactly
        playerObject.transform.position = playerTargetPos.position;
        playerObject.transform.rotation = playerTargetPos.rotation;
    }
}
