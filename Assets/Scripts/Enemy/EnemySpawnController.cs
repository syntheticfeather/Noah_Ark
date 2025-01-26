using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Wave
{
    public List<MonsterGroup> monsterGroups; // 怪物组列表
    public float spawnInterval = .5f; // 生成间隔
    public float waveInterval = 120; // 波次间隔
}

[System.Serializable]
public class MonsterGroup
{
    public GameObject monsterPrefab; // 怪物预制体
    public int count; // 生成数量
}
public class EnemySpawnController : MonoBehaviour
{
    public List<Wave> waves; // 波次列表
    private int currentWaveIndex = 0; // 当前波次索引
    private bool isSpawning = false; // 是否正在生成怪物
    private Transform Camera;
    public Transform minspawn, maxspawn;
    void Start()
    {
        Camera = CameraShake.instance.transform;
        StartNextWave();
    }

    void Update()
    {
        // 检测当前波次是否完成
        if (!isSpawning && currentWaveIndex < waves.Count && AllMonstersDead())
        {
            // 等待波次间隔后开始下一波
            Invoke("StartNextWave", waves[currentWaveIndex].waveInterval);
        }
    }

    void StartNextWave()
    {
        if (currentWaveIndex < waves.Count)
        {
            StartCoroutine(SpawnWave(waves[currentWaveIndex]));
            currentWaveIndex++;
        }
    }

    IEnumerator SpawnWave(Wave wave)
    {
        isSpawning = true;

        foreach (MonsterGroup group in wave.monsterGroups)
        {
            for (int i = 0; i < group.count; i++)
            {
                // 随机选择一个生成点
                Vector3 spawnPoint = SelectSpawnPosition();

                // 生成怪物
                Instantiate(group.monsterPrefab, spawnPoint, Quaternion.identity);

                // 等待生成间隔
                yield return new WaitForSeconds(wave.spawnInterval);
            }
        }

        isSpawning = false;
    }

    bool AllMonstersDead()
    {
        // 检测场景中是否还有怪物
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Enemy");
        return monsters.Length == 0;
    }
    public Vector3 SelectSpawnPosition()
    {
        Vector3 spawnPos = Vector3.zero;
        bool spawnVerticalEdge = Random.Range(0f, 1f) >= .5f;
        if (spawnVerticalEdge)
        {
            spawnPos.y = Random.Range(minspawn.position.y, maxspawn.position.y);
            if (Random.Range(0f, 1f) > 0.5f)
            {
                spawnPos.x = maxspawn.position.x;
            }
            else
            {
                spawnPos.x = minspawn.position.x;
            }
        }
        else
        {
            spawnPos.x = Random.Range(minspawn.position.x, maxspawn.position.x);
            if (Random.Range(0f, 1f) > 0.5f)
            {
                spawnPos.y = maxspawn.position.y;
            }
            else
            {
                spawnPos.y = minspawn.position.y;
            }
        }
        return spawnPos;
    }
}


