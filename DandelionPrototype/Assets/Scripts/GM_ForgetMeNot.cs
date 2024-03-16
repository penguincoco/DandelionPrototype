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
    }

    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Space))
            SpawnNewFlower(firstFlower.transform.position); */
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
    }

    public void SpawnNewFlower(Vector3 spawnNeighborhood)
    {
        SpawnFlower(flowerScalar, spawnNeighborhood);
    }

    private void SpawnFlower(int numberOfFlowers, Vector3 spawnNeighborhood)
    {
        for (int i = 0; i < numberOfFlowers; i++) 
        {
            flowerSpawn.SpawnSapling(spawnNeighborhood);
            UpdateFlowerCounter();
            UpdateFlowerScalar();
        }
    }
}
