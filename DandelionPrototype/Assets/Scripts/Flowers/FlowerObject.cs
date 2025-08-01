using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerObject : MonoBehaviour
{
    public Flower thisFlower;
    private int currentGrowthStage = 0;
    [SerializeField] private AudioSource audioSrc;
    private bool fullyBloomed = false;      //this is for managing if the flower has reached adulthood/full bloom

    public GameObject currentStageObject;
    [SerializeField] private GameObject flowerSpawnPos;

    private float currentLightAmount;
    private float currentWaterLevel;
    [SerializeField] private bool needsLight;
    [SerializeField] private bool needsWater;
    private bool lightThresholdMet;       //this is for if the sun threshold has been met and the flower will move onto the next stage of growth
    private bool waterThresholdMet;     //this is for if the water threshold has been met and the flower will move onto the next stage of growth
    [SerializeField] private float wiltTimer;
    [SerializeField] private float wiltWaterThreshold;
    [SerializeField] private float wiltLightThreshold;
    private bool isWilted;

    [SerializeField] private float shineRate;

    [SerializeField] private string actionMapName;
    private bool babySpawned = false;

    [SerializeField] private bool isSelfGrowing;
    [SerializeField] private float selfWaterRate;
    [SerializeField] private float selfLightRate;

    private void Start()
    {
        needsLight = thisFlower.needsLight;
        needsWater = thisFlower.needsWater;

        wiltTimer = thisFlower.wiltTimer;
    }

    void Update()
    {
        //debugging lollll
        /*
        if (Input.GetKeyDown(KeyCode.Space))
            NextGrowthStage(); */

        if (wiltTimer > 0 && fullyBloomed == true)
        {
            wiltTimer -= Time.deltaTime;        //wilt the flower over time

            if (wiltTimer < 0)
            {
                GameObject temp = currentStageObject;
                Destroy(temp);

                currentStageObject = Instantiate(thisFlower.wiltState, flowerSpawnPos.transform);
                currentStageObject.transform.localScale = Vector3.one;
                isWilted = true;
                SetAudio(0.6f, true, 0.8f);

                fullyBloomed = false;

                wiltTimer = thisFlower.wiltTimer;
            }
        }

        if (isSelfGrowing)
        {
            SetWater(selfWaterRate);
            SetSunlight(selfLightRate);
        }
    }

    public void SetSelfGrowing(bool growsOnItsOwn)
    {
        this.isSelfGrowing = growsOnItsOwn;
    }

    public void SetAudio(float pitch, bool isLooping, float volume)
    {
        audioSrc.pitch = pitch;
        audioSrc.loop = isLooping;
        audioSrc.volume = volume;

        if (isLooping == false)
            audioSrc.PlayOneShot(audioSrc.clip);
        else
            audioSrc.Play();
    }

    public void CheckWilt()
    {
        if (needsLight == true && needsWater == true) //if the flower needs light and water
        {
            if (currentWaterLevel >= wiltWaterThreshold && currentLightAmount >= wiltLightThreshold)
            {
                fullyBloomed = true;
            }
        }
        else if (needsLight == true && needsWater == false) //if the flower needs light, but not water
        {
            if (currentLightAmount >= wiltLightThreshold)
            {
                fullyBloomed = true;
            }
        }
        else if (needsLight == false && needsWater == true) //if the flower does not need light, but needs water 
        {
            if (currentWaterLevel >= wiltWaterThreshold)
                fullyBloomed = true;
        }

        if (fullyBloomed == true)
        {
            currentWaterLevel = 0;
            currentLightAmount = 0;

            GameObject temp = currentStageObject;
            Destroy(temp);

            currentStageObject = Instantiate(thisFlower.growthStates[currentGrowthStage], flowerSpawnPos.transform);
            currentStageObject.transform.localScale = Vector3.one;
            SetAudio(1f, false, 0.8f);
        }
    }

    public void CheckStage()
    {
        //make sure the flower doesn't grow past it's max length
        if (currentGrowthStage + 1 >= thisFlower.growthStates.Length)
            fullyBloomed = true;
        if (fullyBloomed == true)
        {
            if (babySpawned == false)
            {
                GM_ForgetMeNot.Instance.SpawnNewFlowers(this.transform.position);
                babySpawned = true;
            }
            currentWaterLevel = 0;
            currentLightAmount = 0;
            return;
        }

        if (needsLight == true && needsWater == true) //if the flower needs light and water
        {
            if (lightThresholdMet == true && waterThresholdMet == true)
            {
                NextGrowthStage();
            }
        }
        else if (needsLight == true && needsWater == false) //if the flower needs light, but not water
        {
            if (lightThresholdMet == true)
                NextGrowthStage();
        }
        else if (needsLight == false && needsWater == true) //if the flower does not need light, but needs water 
        {
            if (waterThresholdMet == true)
                NextGrowthStage();
        }
    }

    public void SetWater(float waterAmount)
    {
        if (isWilted == true)
        {
            currentWaterLevel += waterAmount;
            if (currentWaterLevel >= wiltWaterThreshold)
                CheckWilt();
            return;
        } 

        currentWaterLevel += waterAmount;

        if (currentWaterLevel >= thisFlower.waterStates[currentGrowthStage])
        {
            waterThresholdMet = true;
            //NextGrowthStage();
            CheckStage();
        }
    }

    public void SetSunlight(float time)
    {
        if (isWilted == true)
        {
            currentLightAmount += time;
            if (currentLightAmount >= wiltLightThreshold)
                CheckWilt();
            return;
        } 

        if (currentLightAmount >= thisFlower.lightStates[currentGrowthStage])
        {
            lightThresholdMet = true;
            CheckStage();
        }
        else
            currentLightAmount += time;
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
            SetAudio(1f, false, 0.8f);
        }

        if (currentGrowthStage == thisFlower.growthStates.Length - 1)
            fullyBloomed = true;

        lightThresholdMet = false;
        waterThresholdMet = false;

        /*
        if (fullyBloomed == true)
        {
            ActionMapManager.Instance.SetActionMap(actionMapName);
            Input_PlayerFlowerController.Instance.SetFlowerObject(currentStageObject);
            Input_PlayerFlowerController.Instance.SetFlowerMapActive();
        } */
    }
}
