using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input_PlayerWaterController : MonoBehaviour
{
    [SerializeField] private bool isCarryingWater = false;
    [SerializeField] private float raycastDist;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private bool isFacingTarget = false;
    [SerializeField] private GameObject raycastObject;

    [SerializeField] private GameObject waterObject;

    private float waterLevel = 0;
    [SerializeField] private float pourRate;
    
    void OnWater()
    {
        //if the player is facing a water source
        if (isFacingTarget && raycastObject.CompareTag("Water Source") == true && isCarryingWater == false)
        {
            isCarryingWater = true;
            waterObject.SetActive(true);
        }
        else if (isFacingTarget && raycastObject.CompareTag("Flower") == true && isCarryingWater == true)
        {
            raycastObject.GetComponent<FlowerObject>().SetWater(pourRate);
            isCarryingWater = false;
            waterObject.SetActive(false);
        }
    }

    private void PourWater()
    {
        //if (Gamepad.current.buttonSouth.isPressed && waterLevel.value > 0)
        if (Gamepad.current.buttonSouth.isPressed && waterLevel > 0)
        {
            raycastObject.GetComponent<FlowerObject>().SetWater(pourRate);
        }
        if (waterLevel <= 0)
            isCarryingWater = false;
    }

    private void Update()
    {
        /* 
        if (isCarryingWater == true && raycastObject != null && raycastObject.CompareTag("Flower") == true)
            PourWater(); */

        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * raycastDist, Color.red);
        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDist, interactableLayer))
        //if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDist))
        {
            //UIManager.Instance.SetPromptText();
            raycastObject = hit.collider.gameObject; 
            isFacingTarget = true;
        }
        else
        {
            isFacingTarget = false;
            raycastObject = null;
        }
        /*
        else if (GameManager.Instance.GetFlowerStatus() == false)
        {
            //UIManager.Instance.ClearPromptText();
            isFacingTarget = false;
        } */
    }

}
