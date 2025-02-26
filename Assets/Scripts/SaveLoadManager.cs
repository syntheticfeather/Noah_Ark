using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public List<int> resources; // 资源数据
    public Vector3 playerPosition; // 玩家位置
    public List<IslandData> islands; // 岛屿数据
    public float wholeTime; // 游戏时间
}

[System.Serializable]
public class IslandData
{
    public Vector3 position; // 岛屿位置
    public int type; // 岛屿类型
}

public class SaveLoadManager : MonoBehaviour
{
    private string saveFilePath;
    private GameData gameData; // 存储加载的游戏数据

    private void Start()
    {
        saveFilePath = Application.persistentDataPath + "/save.json";
    }

    // 检查是否存在存档
    public bool HasSaveData()
    {
        return File.Exists(saveFilePath);
    }

    // 保存游戏
    public void SaveGame()
    {
        gameData = new GameData();

        // 获取资源数据
        ResourceManager resourceManager = FindObjectOfType<ResourceManager>();
        if (resourceManager != null)
        {
            gameData.resources = new List<int>(resourceManager.Resource);
        }

        // 获取玩家位置
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            gameData.playerPosition = player.transform.position;
        }

        // 获取岛屿数据
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

        // 获取游戏时间
        TimeUI timeUI = FindObjectOfType<TimeUI>();
        if (timeUI != null)
        {
            gameData.wholeTime = timeUI.WholeTime;
        }

        // 将数据序列化为JSON并保存
        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(saveFilePath, json);

        
    }

    // 加载游戏
    public void LoadGame()
    {
        if (!File.Exists(saveFilePath))
        {
            Debug.LogError("存档文件不存在: " + saveFilePath);
            return;
        }

        // 从文件中读取JSON数据
        string json = File.ReadAllText(saveFilePath);
        gameData = JsonUtility.FromJson<GameData>(json);

        // 加载资源数据
        ResourceManager resourceManager = FindObjectOfType<ResourceManager>();
        if (resourceManager != null)
        {
            resourceManager.Resource = new List<int>(gameData.resources);
        }

        // 加载玩家位置
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.transform.position = gameData.playerPosition;
        }

        // 加载游戏时间
        TimeUI timeUI = FindObjectOfType<TimeUI>();
        if (timeUI != null)
        {
            timeUI.WholeTime = gameData.wholeTime;
        }

        
    }

    // 获取岛屿数据
    public List<IslandData> GetIslandsData()
    {
        if (gameData != null)
        {
            return gameData.islands;
        }
        return null;
    }

    // 清空存档
    public void ClearSave()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            
        }
        
    }

    // 初始化新游戏数据
    public void InitializeNewGame()
    {
        // 初始化资源数据
        ResourceManager resourceManager = FindObjectOfType<ResourceManager>();
        if (resourceManager != null)
        {
            resourceManager.Resource = new List<int> { 100, 50, 200, 30 }; // 示例初始资源
        }

        // 初始化玩家位置
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.transform.position = Vector3.zero; // 示例初始位置
        }

        // 初始化游戏时间
        TimeUI timeUI = FindObjectOfType<TimeUI>();
        if (timeUI != null)
        {
            timeUI.WholeTime = 1800f; // 30分钟
        }

        
    }

    public GameData GetTimeData()
    {
        return gameData;
    }
}