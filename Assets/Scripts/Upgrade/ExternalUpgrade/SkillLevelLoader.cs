using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SkillLevelLoader : MonoBehaviour
{
    // 单例实例
    public static SkillLevelLoader Instance { get; private set; }

    // 文件路径
    private string saveFilePath;

    void Awake()
    {
        // 单例模式初始化
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 保持对象在场景切换时不销毁
        }
        else
        {
            Destroy(gameObject); // 如果已存在实例，销毁新对象
            return;
        }

        // 初始化文件路径
        saveFilePath = Path.Combine(Application.persistentDataPath, "skills_data.json");
    }

    // 获取指定技能的等级
    public int GetSkillLevel(string skillName)
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath); // 读取文件
            SkillSaveData saveData = JsonUtility.FromJson<SkillSaveData>(json); 

            if (saveData.skillStates.TryGetValue(skillName, out SkillState state))
            {
                return state.currentLevel; // 返回技能等级
            }
        }
        return -1; // 文件不存在或技能未找到
    }

    // 技能保存数据结构
    [System.Serializable]
    private class SkillSaveData
    {
        public Dictionary<string, SkillState> skillStates = new Dictionary<string, SkillState>();
    }

    // 技能状态
    [System.Serializable]
    private class SkillState
    {
        public bool isUnlocked;
        public int currentLevel;
    }
}