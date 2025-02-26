using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public List<int> resources; // ��Դ����
    public Vector3 playerPosition; // ���λ��
    public List<IslandData> islands; // ��������
    public float wholeTime; // ��Ϸʱ��
}

[System.Serializable]
public class IslandData
{
    public Vector3 position; // ����λ��
    public int type; // ��������
}

public class SaveLoadManager : MonoBehaviour
{
    private string saveFilePath;
    private GameData gameData; // �洢���ص���Ϸ����

    private void Start()
    {
        saveFilePath = Application.persistentDataPath + "/save.json";
    }

    // ����Ƿ���ڴ浵
    public bool HasSaveData()
    {
        return File.Exists(saveFilePath);
    }

    // ������Ϸ
    public void SaveGame()
    {
        gameData = new GameData();

        // ��ȡ��Դ����
        ResourceManager resourceManager = FindObjectOfType<ResourceManager>();
        if (resourceManager != null)
        {
            gameData.resources = new List<int>(resourceManager.Resource);
        }

        // ��ȡ���λ��
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            gameData.playerPosition = player.transform.position;
        }

        // ��ȡ��������
        IsLandController isLandController = FindObjectOfType<IsLandController>();
        if (isLandController != null)
        {
            gameData.islands = new List<IslandData>();
            foreach (var island in isLandController.CurIsLand_List)
            {
                Land islandScript = island.GetComponent<Land>();
                if (islandScript != null)
                {
                    IslandData islandData = new IslandData
                    {
                        position = island.transform.position,
                        type = islandScript.type
                    };
                    gameData.islands.Add(islandData);
                }
            }
        }

        // ��ȡ��Ϸʱ��
        TimeUI timeUI = FindObjectOfType<TimeUI>();
        if (timeUI != null)
        {
            gameData.wholeTime = timeUI.WholeTime;
        }

        // ���������л�ΪJSON������
        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(saveFilePath, json);

        
    }

    // ������Ϸ
    public void LoadGame()
    {
        if (!File.Exists(saveFilePath))
        {
            Debug.LogError("�浵�ļ�������: " + saveFilePath);
            return;
        }

        // ���ļ��ж�ȡJSON����
        string json = File.ReadAllText(saveFilePath);
        gameData = JsonUtility.FromJson<GameData>(json);

        // ������Դ����
        ResourceManager resourceManager = FindObjectOfType<ResourceManager>();
        if (resourceManager != null)
        {
            resourceManager.Resource = new List<int>(gameData.resources);
        }

        // �������λ��
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.transform.position = gameData.playerPosition;
        }

        // ������Ϸʱ��
        TimeUI timeUI = FindObjectOfType<TimeUI>();
        if (timeUI != null)
        {
            timeUI.WholeTime = gameData.wholeTime;
        }

        
    }

    // ��ȡ��������
    public List<IslandData> GetIslandsData()
    {
        if (gameData != null)
        {
            return gameData.islands;
        }
        return null;
    }

    // ��մ浵
    public void ClearSave()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            
        }
        
    }

    // ��ʼ������Ϸ����
    public void InitializeNewGame()
    {
        // ��ʼ����Դ����
        ResourceManager resourceManager = FindObjectOfType<ResourceManager>();
        if (resourceManager != null)
        {
            resourceManager.Resource = new List<int> { 100, 50, 200, 30 }; // ʾ����ʼ��Դ
        }

        // ��ʼ�����λ��
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.transform.position = Vector3.zero; // ʾ����ʼλ��
        }

        // ��ʼ����Ϸʱ��
        TimeUI timeUI = FindObjectOfType<TimeUI>();
        if (timeUI != null)
        {
            timeUI.WholeTime = 1800f; // 30����
        }

        
    }

    public GameData GetTimeData()
    {
        return gameData;
    }
}