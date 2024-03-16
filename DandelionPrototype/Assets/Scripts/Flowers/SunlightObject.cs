using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunlightObject : MonoBehaviour
{
    public Flower thisFlower;
    private int currentGrowthStage = 0;
    private AudioSource audioSrc;
    private bool fullyBloomed = false;

    public GameObject currentStageObject;
    [SerializeField] private GameObject flowerSpawnPos;

    [SerializeField] private float currentLightAmount;

    [SerializeField] private string actionMapName;

    public void SetSunlight(float time)
    {
        if (currentGrowthStage + 1 >= thisFlower.growthStates.Length)
            fullyBloomed = true;

        if (fullyBloomed == true)
            return;

        else
        {
            currentLightAmount += time;

            if (currentLightAmount >= thisFlower.lightStates[currentGrowthStage])
                NextGrowthStage();
        }
    }

    public void NextGrowthStage()
    {
        currentGrowthStage++;

        if (currentGrowthStage < thisFlower.growthStates.Length)
        {
            GameObject temp = currentStageObject;
            Destroy(temp);
            //currentStageObject = Instantiate(thisFlower.growthStates[currentGrowthStage], this.transform);
            currentStageObject = Instantiate(thisFlower.growthStates[currentGrowthStage], flowerSpawnPos.transform);
            currentStageObject.transform.localScale = Vector3.one;
            //currentStageObject = Instantiate(thisFlower.growthStates[currentGrowthStage]);
            //audioSrc.PlayOneShot(audioSrc.clip);
        }

        if (currentGrowthStage == thisFlower.growthStates.Length - 1)
            fullyBloomed = true;

        /*
        if (fullyBloomed == true)
        {
            ActionMapManager.Instance.SetActionMap(actionMapName);
            Input_PlayerFlowerController.Instance.SetFlowerObject(currentStageObject);
            Input_PlayerFlowerController.Instance.SetFlowerMapActive();
        } */
    }

    /*
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sunlight"))
            Debug.Log("triggering with sunlight");
    } */
}
