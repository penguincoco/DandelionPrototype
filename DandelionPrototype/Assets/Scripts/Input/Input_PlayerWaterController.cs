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

    [SerializeField] private Animator playerAnimator;

    [SerializeField] private AudioSource audioSrc;
    [SerializeField] private AudioClip sfx_pour;

    void OnWater()
    {
        //if the player is facing a water source
        if (isFacingTarget && raycastObject.CompareTag("Water Source") == true && isCarryingWater == false)
        {
            isCarryingWater = true;
            waterLevel = 1;
            waterObject.SetActive(true);
        }
        /*
        else if (isFacingTarget && raycastObject.CompareTag("Flower") == true && isCarryingWater == true)
        {
            //set pourRate to 1 to make this line work again. This line below is specifically for just tapping once 
            //to water the flower 
            //raycastObject.GetComponent<FlowerObject>().SetWater(pourRate);
            //isCarryingWater = false;
            //waterObject.SetActive(false);
        }*/
    }

    private void PourWater()
    {
        //if (Gamepad.current.buttonSouth.isPressed && waterLevel.value > 0)
        if (Gamepad.current.buttonSouth.isPressed && waterLevel > 0)
        {
            raycastObject.GetComponent<FlowerObject>().SetWater(pourRate);
            waterLevel -= pourRate;
            
            playerAnimator.Play("Anim_Wisp_Watering_Start");

            if (audioSrc.isPlaying == false)
            {
                audioSrc.clip = sfx_pour;
                audioSrc.Play();
            }
        }
        else if (Gamepad.current.buttonSouth.wasReleasedThisFrame && waterLevel > 0)
        {
            playerAnimator.Play("Anim_Wisp_Watering_End");
            audioSrc.Stop();
        }

        if (waterLevel <= 0)
        {
            isCarryingWater = false;
            waterObject.SetActive(false);
            playerAnimator.Play("Anim_Wisp_Watering_End");
            audioSrc.Stop();
        }
    }

    private void Update()
    {
        if (isCarryingWater == true && raycastObject != null && raycastObject.CompareTag("Flower") == true)
            PourWater();
        else if (raycastObject == null)
        {
            if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Anim_Wisp_Watering_Start"))
            {
                audioSrc.Stop();
                playerAnimator.Play("Anim_Wisp_Watering_End");
            }
        }

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
