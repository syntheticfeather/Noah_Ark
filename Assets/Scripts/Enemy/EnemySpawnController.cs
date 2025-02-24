using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
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
    public static EnemySpawnController instance;
    public List<Wave> waves; // �����б�
    public Wave Bosswave;
    public  int currentWaveIndex = 0; // ��ǰ��������
    private bool isSpawning = false; // �Ƿ��������ɹ���
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
        // ��⵱ǰ�����Ƿ����
        if (!CameraFollow.instance.BossFight)
        {
            if (!isSpawning && currentWaveIndex < waves.Count)
            {
                // �ȴ����μ����ʼ��һ��          
                if (Counter > waves[currentWaveIndex].waveInterval)
                {
                    Debug.Log("continue" + (currentWaveIndex) + "����");
                    StartNextWave();
                    Counter = 0;
                }
            }
        }
        else
        {
            if (Counter > Bosswave.waveInterval)
            {
                Debug.Log("boss���ɹ�");
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


