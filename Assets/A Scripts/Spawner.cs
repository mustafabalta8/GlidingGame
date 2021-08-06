using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] Prefabs;
    [SerializeField] GameObject prefabHolder;

    public float spawnInterval = 0.1f;
    int ZpositionOfPrefab = 10;
    Quaternion baseRotation;
    private int zPositionAmountOfRise=10;
    [SerializeField] Vector3 spawnPos;

    int i = 0;
    private void Start()
    {
        zPositionAmountOfRise = 15;
        baseRotation = transform.rotation;

        StartCoroutine(SpawnObjects());
    }
    public int ZPositionAmountOfRise
    {
        
        get
        {
            return zPositionAmountOfRise;
        }

        set
        {
            zPositionAmountOfRise = value;
        }
    }
    IEnumerator SpawnObjects()
    {
        
        while (true)
        {
            
            ZpositionOfPrefab = ZpositionOfPrefab + 15; ;// To prevent spawning at same place and to spawn futher places
           // Debug.Log("IncreaseZ" + ZpositionOfPrefab)
            Vector3 playerPos = new Vector3(0, 0, transform.position.z/1.3f);
            
            for (i = 1;i < 15; i++){

                spawnPos.y = Random.Range(-4, 4);
                GameObject prefabToInstantiate= Prefabs[Random.Range(0,2)];
                Vector3 SpawnWith = new Vector3( (i*18)+Random.Range(3,15)-150, 0, ZpositionOfPrefab);
                GameObject NewObj = Instantiate(prefabToInstantiate, spawnPos + SpawnWith + playerPos, baseRotation, prefabHolder.transform);

                //Destroy(NewObj, 20);
            }
            
           
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
