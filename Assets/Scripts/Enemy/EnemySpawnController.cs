using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
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
    public static EnemySpawnController instance;
    public List<Wave> waves; // 波次列表
    public Wave Bosswave;
    public  int currentWaveIndex = 0; // 当前波次索引
    private bool isSpawning = false; // 是否正在生成怪物
    private Transform player;
    public Vector3 minspawn, maxspawn;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {

        player = PlayerHealthController.instance.transform;
    }
    public float Counter = 0;
    void Update()
    {

        Counter += Time.deltaTime;
        // 检测当前波次是否完成
        if (!CameraFollow.instance.BossFight)
        {
            if (!isSpawning && currentWaveIndex < waves.Count)
            {
                // 等待波次间隔后开始下一波          
                if (Counter > waves[currentWaveIndex].waveInterval)
                {
                    Debug.Log("continue" + (currentWaveIndex) + "波次");
                    StartNextWave();
                    Counter = 0;
                }
            }
        }
        else
        {
            if (Counter > Bosswave.waveInterval)
            {
                Debug.Log("boss生成怪");
                StartCoroutine(SpawnWave(Bosswave));
                Counter = 0;
            }
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
        if (!CameraFollow.instance.BossFight)
        {
            minspawn = player.position + new Vector3(-28, -16, 0);
            maxspawn = player.position + new Vector3(+28, 16, 0);
        }
        else
        {
            minspawn = player.position + new Vector3(-32, -32, 0);
            maxspawn = player.position + new Vector3(+32, -16, 0);
        }
        Vector3 spawnPos = Vector3.zero;
        bool spawnVerticalEdge = Random.Range(0f, 1f) >= .5f;
        if (spawnVerticalEdge)
        {
            spawnPos.y = Random.Range(minspawn.y, maxspawn.y);
            if (Random.Range(0f, 1f) > 0.5f)
            {
                spawnPos.x = maxspawn.x;
            }
            else
            {
                spawnPos.x = minspawn.x;
            }
        }
        else
        {
            spawnPos.x = Random.Range(minspawn.x, maxspawn.x);
            if (Random.Range(0f, 1f) > 0.5f)
            {
                spawnPos.y = maxspawn.y;
            }
            else
            {
                spawnPos.y = minspawn.y;
            }
        }
        return spawnPos;
    }
}


