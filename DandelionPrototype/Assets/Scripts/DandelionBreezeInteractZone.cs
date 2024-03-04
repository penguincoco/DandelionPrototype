using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DandelionBreezeInteractZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == true)
        {
            GameManager.Instance.SetDandelionInteractStatus(true);
            Debug.Log("player in zone");
        }
    }
}
