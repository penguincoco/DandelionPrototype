using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DandelionBreezeInteractZone : MonoBehaviour
{
    private void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == true)
        {
            GameManager.Instance.SetDandelionInteractStatus(true);

            if (GameManager.Instance.GetBellStatus() == true)
            {
                UIManager.Instance.SetPromptText("press [space] to follow the breeze");
                //UIManager.Instance.IncreasePromptCounter();
                //UIManager.Instance.SetPromptText();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") == true)
        {
            GameManager.Instance.SetDandelionInteractStatus(false);

            //if the player is carrying the flower, re-show the instruction to press space to ring the bell 
            if (GameManager.Instance.GetFlowerStatus() == true)
            {
                UIManager.Instance.SetPromptText("press [space] to make music");
                //UIManager.Instance.DecreasePromptCounter();
                //UIManager.Instance.SetPromptText();
            }
            else
            {
                UIManager.Instance.ClearPromptText();
            } 
        }
    }
}
