using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM_ForgetMeNot : MonoBehaviour
{
    private static GM_ForgetMeNot _instance;
    public static GM_ForgetMeNot Instance { get { return _instance; } }

    [SerializeField] private string mapName;

    [SerializeField] private int numberOfFlowers;
    [SerializeField] private int flowerScalar = 1;
    [SerializeField] private int[] scalars;     //checking how many flowers have been spawned to determine whether or not to increase
                                                //the multiplier
    private int scalarCounter = 0;
    [SerializeField] private FlowerSpawner flowerSpawn;

    [SerializeField] private GameObject firstFlower;
    [SerializeField] private bool spawnSelfGrowingFlowers;
    private GameObject lastFlower;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //start with the watering map on 
        ActionMapManager.Instance.SetActionMap(mapName);
        lastFlower = firstFlower;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SpawnNewFlowers(lastFlower.transform.position); 
    }

    public void UpdateFlowerCounter()
    {
        numberOfFlowers += 1;
    }

    public void UpdateFlowerScalar()
    {
        if (scalarCounter < scalars.Length && numberOfFlowers >= scalars[scalarCounter]) 
        {
            flowerScalar = (int)Mathf.Pow((float)flowerScalar, 2f);
            scalarCounter += 1;
        }

        if (scalarCounter >= 2)
        {
            spawnSelfGrowingFlowers = true;
        }
    }

    public void SpawnNewFlowers(Vector3 spawnNeighborhood)
    {
        SpawnFlowers(flowerScalar, spawnNeighborhood);
    }

    private void SpawnFlowers(int numberToSpawn, Vector3 spawnNeighborhood)
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            bool isSelfGrowing = false;
            if (spawnSelfGrowingFlowers == true)
            {
                float randomValue = Random.value;
                
                //heavily bias it towards self growing flowers 
                if (randomValue <= 0.75f)
                    isSelfGrowing = true;
                else
                    isSelfGrowing = false;
            }

            flowerSpawn.SpawnSapling(spawnNeighborhood, isSelfGrowing);
        }
    }

    public void SetLatestBaby(GameObject spawnedFlower)
    {
        lastFlower = spawnedFlower;
    }
}
