using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRay : MonoBehaviour
{
    private GameObject flowerObject; 
    private bool isShiningOnFlower = false;
    [SerializeField] private float shineRate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isShiningOnFlower == true && flowerObject != null)
        {
            flowerObject.GetComponent<FlowerObject>().SetSunlight(shineRate);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<FlowerObject>() != null)
        {
            isShiningOnFlower = true;
            flowerObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<FlowerObject>() != null)
        {
            isShiningOnFlower = false;
            flowerObject = null; 
        }
    }
}
