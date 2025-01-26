using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Wave
{
    public List<MonsterGroup> monsterGroups; // �������б�
    public float spawnInterval = .5f; // ���ɼ��
    public float waveInterval = 120; // ���μ��
}

[System.Serializable]
public class MonsterGroup
{
    public GameObject monsterPrefab; // ����Ԥ����
    public int count; // ��������
}
public class EnemySpawnController : MonoBehaviour
{
    public List<Wave> waves; // �����б�
    private int currentWaveIndex = 0; // ��ǰ��������
    private bool isSpawning = false; // �Ƿ��������ɹ���
    private Transform Camera;
    public Transform minspawn, maxspawn;
    void Start()
    {
        Camera = CameraShake.instance.transform;
        StartNextWave();
    }

    void Update()
    {
        // ��⵱ǰ�����Ƿ����
        if (!isSpawning && currentWaveIndex < waves.Count && AllMonstersDead())
        {
            // �ȴ����μ����ʼ��һ��
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
                // ���ѡ��һ�����ɵ�
                Vector3 spawnPoint = SelectSpawnPosition();

                // ���ɹ���
                Instantiate(group.monsterPrefab, spawnPoint, Quaternion.identity);

                // �ȴ����ɼ��
                yield return new WaitForSeconds(wave.spawnInterval);
            }
        }

        isSpawning = false;
    }

    bool AllMonstersDead()
    {
        // ��ⳡ�����Ƿ��й���
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


