using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] Prefabs;
    [SerializeField] GameObject prefabHolder;

    public float spawnInterval = 0.1f;
    int ZpositionOfPrefab = 10;
    Quaternion baseRotation;
    [SerializeField] Vector3 spawnPos;

    int i = 0;
    private void Start()
    {
        baseRotation = transform.rotation;

        StartCoroutine(SpawnObjects());
    }
    IEnumerator SpawnObjects()
    {       
        while (true)
        {           
            ZpositionOfPrefab = ZpositionOfPrefab + 15; ;// To prevent spawning at same place and to spawn futher places
          
            Vector3 playerPos = new Vector3(0, 0, transform.position.z/1.3f);
            
            for (i = 1;i < 13; i++){

                spawnPos.y = Random.Range(-4, 4);
                int random = Random.Range(3, 17);
                GameObject prefabToInstantiate= Prefabs[Random.Range(0,2)];
                Vector3 SpawnWith = new Vector3( (i*23)+ random - 160, 0, ZpositionOfPrefab+ random);
                Instantiate(prefabToInstantiate, spawnPos + SpawnWith + playerPos, baseRotation, prefabHolder.transform);

            }                
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
