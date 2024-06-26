using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float spawnRange = 9.0f;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave();


    }

    // Update is called once per frame
    void Update()
    {

    }
    void SpawnEnemyWave()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(enemyPrefab, generateSpawnPos(), enemyPrefab.transform.rotation);
        }
    }
    private Vector3 generateSpawnPos()
        {
        float spawnPosX = Random.Range(-spawnRange, spawnRange), spawnPosZ = Random.Range(-spawnRange, spawnRange);
       Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }
}
