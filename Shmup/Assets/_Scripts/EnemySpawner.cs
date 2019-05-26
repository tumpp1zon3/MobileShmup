using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public enum EnemyTypes {enemy1,enemy2,enemy3}
    public enum SpawnLocations {UpLeft, UpCenter, UpRight, MidLeft, MidCenter, MidRight, DownLeft, DownCenter, DownRight }

    [System.Serializable]
    public struct EnemySpawn
    {
        public EnemyTypes EnemyType;
        public float SpawnTime;
        public SpawnLocations SpawnLocation;
    }
    public List<EnemySpawn> EnemySpawns;
    public GameObject enemyContainer;
    public GameObject[] SpawnPositions;
    public GameObject[] EnemyTypePrefabs;
    float timer;

    void Start()
    {
        EnemySpawns.Sort((s1, s2) => s1.SpawnTime.CompareTo(s2.SpawnTime));
    }

    void Update()
    {
        timer += Time.deltaTime;
        for(int i = 0; i < EnemySpawns.Count; i++)
        {
            if(EnemySpawns[i].SpawnTime < timer)
            {
                Instantiate(EnemyTypePrefabs[(int)EnemySpawns[i].EnemyType], SpawnPositions[(int)EnemySpawns[i].SpawnLocation].transform.position, EnemyTypePrefabs[(int)EnemySpawns[i].EnemyType].transform.rotation, enemyContainer.transform);
                EnemySpawns.Remove(EnemySpawns[i]);
            }
            else
            {
                break;
            }
        }
    }
}
