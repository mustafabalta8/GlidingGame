using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject prefabToInstantiate;
    [SerializeField] GameObject prefabHolder;
    public float maxDistance = 1f;
    public float spawnInterval = 0.1f;
    int ZpositionOfPrefab = 10;
    Quaternion baseRotation;
    private int zPositionAmountOfRise=10;
    [SerializeField] Vector3 spawnPos;

    int i = 0;
    private void Start()
    {
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

            Debug.Log("zPositionAmountOfRise:" + zPositionAmountOfRise);
        }
    }
    IEnumerator SpawnObjects()
    {
        
        while (true)
        {
            /*
            Vector3 currentPosition = transform.position;
            Vector3 randomOffset = Random.insideUnitSphere * maxDistance;
            Vector3 spawnPosition = currentPosition + randomOffset;
            Instantiate(prefabToInstantiate, spawnPosition, baseRotation);
            */
            ZpositionOfPrefab = ZpositionOfPrefab + zPositionAmountOfRise; ;// To prevent spawning at same place and to spawn futher places
           // Debug.Log("IncreaseZ" + ZpositionOfPrefab)
            Vector3 playerPos = new Vector3(0, 0, transform.position.z/1.3f);

            for (i = 1;i < 20; i++){
                Vector3 SpawnWith = new Vector3( (i*13)+Random.Range(1,10)-120, 0, ZpositionOfPrefab);
                GameObject NewObj = Instantiate(prefabToInstantiate, spawnPos + SpawnWith + playerPos, baseRotation, prefabHolder.transform);

                Destroy(NewObj, 20);
            }
            
           
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
