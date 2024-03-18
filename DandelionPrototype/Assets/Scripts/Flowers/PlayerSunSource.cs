using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSunSource : MonoBehaviour
{
    [SerializeField] private GameObject flowerObject;
    private bool isShiningOnFlower = false;
    [SerializeField] private float shineRate;

    void Update()
    {
        if (isShiningOnFlower == true && flowerObject != null)
        {
            flowerObject.GetComponent<FlowerObject>().SetSunlight(shineRate);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") == true)
        {
            isShiningOnFlower = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") == true)
        {
            isShiningOnFlower = false;
        }
    } 
}
