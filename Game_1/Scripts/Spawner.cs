using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnDatas;

    int i = 1;
    int level = 0;
    float timer = 0f;
    float spawnTime = 2f;
    float playTime = 0f;
    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }
    void Update()
    {
        if (!GameManager.instance.isLive) return;

        timer += Time.deltaTime;
        if(timer >= spawnTime)
        {
            spawn();
            timer = 0f;
        }
        
        if(playTime + 20f <= GameManager.instance.gameTime && level < spawnDatas.Length - 1)
        {
            spawnTime = Mathf.Max(1.0f, spawnTime - 0.1f);
            playTime = GameManager.instance.gameTime;
            level++;
        }
    }
    void spawn()
    {
        List<int> usedIndexes = new List<int>();

        for (int i = 0; i < 4; i++)
        {
            int randomIndex;

            do
            {
                randomIndex = Random.Range(1, spawnPoint.Length);
            } while (usedIndexes.Contains(randomIndex));

            usedIndexes.Add(randomIndex);

            GameObject enemy = GameManager.instance.pool.Get(0);
            enemy.transform.position = spawnPoint[randomIndex].position;
            enemy.GetComponent<EnemyController>().Init(spawnDatas[level]);
        }
    }
}

[System.Serializable]
public class SpawnData
{
    public int maxHealth;
    public int speed;
}