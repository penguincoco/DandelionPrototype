using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterableObject : MonoBehaviour
{
    public Flower thisFlower;
    private int currentGrowthStage = 0;
    private AudioSource audioSrc;
    private bool fullyBloomed = false;

    public GameObject currentStageObject;
    [SerializeField] private GameObject flowerSpawnPos;

    private float currentWaterLevel;

    [SerializeField] private string actionMapName;

    private void Start()
    {
        audioSrc = this.gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        //debugging lollll
        /*
        if (Input.GetKeyDown(KeyCode.Space))
            NextGrowthStage(); */
    }

    public void SetWater(float waterAmount)
    {
        if (currentGrowthStage + 1 >= thisFlower.growthStates.Length)
            fullyBloomed = true;

        if (fullyBloomed == true)
            return;

        else
        {
            currentWaterLevel += waterAmount;

            if (currentWaterLevel >= thisFlower.waterStates[currentGrowthStage])
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
            //currentStageObject = Instantiate(thisFlower.growthStates[currentGrowthStage]);
            audioSrc.PlayOneShot(audioSrc.clip);
        }

        if (currentGrowthStage == thisFlower.growthStates.Length - 1)
            fullyBloomed = true;

        if (fullyBloomed == true)
        {
            ActionMapManager.Instance.SetActionMap(actionMapName);
            Input_PlayerFlowerController.Instance.SetFlowerObject(currentStageObject);
            Input_PlayerFlowerController.Instance.SetFlowerMapActive();
        } 
    }
}
