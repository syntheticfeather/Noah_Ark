using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ExternalUpgrade : MonoBehaviour
{
    // 强制初始化+序列化双重保障
    public List<Skill> skills = new List<Skill>();

    private InternalUpgrade internalUpgrade;

    // 文件路径
    private string saveFilePath;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        // 初始化文件路径
        saveFilePath = Path.Combine(Application.persistentDataPath, "skills_data.json");

        // 加载技能解锁状态
        LoadSkills();
    }

    void Start()
    {
        internalUpgrade = GetComponent<InternalUpgrade>();
    }

    public void UnlockSkill(string skillName)
    {
        int crystal = PlayerPrefs.GetInt("CrystalCount", 0); // 读写局外crystal数据
        Skill skill = skills.Find(s => s.name == skillName);

        if (skill != null && skill.CanUnlock(crystal))
        {
            // 扣除水晶
            crystal -= skill.Cost[skill.level]; // 扣除当前等级的升级花费
            PlayerPrefs.SetInt("CrystalCount", crystal);

            // 解锁技能
            skill.UnlockNextLevel();

            // 执行技能解锁后的逻辑
            Unlock(skill.name, skill.level);

            // 保存技能解锁状态
            SaveSkills();
        }
    }

    public void Unlock(string name, int level)
    {
        switch (name)
        {
            case "Shield":
                
                break;
            case "Speed":
                
                break;
            case "Extra Bed":
                
                break;
            case "Dash":
                
                break;
            default:
                
                break;
        }
    }

    // 保存技能解锁状态到文件
    private void SaveSkills()
    {
        SkillSaveData saveData = new SkillSaveData();
        foreach (var skill in skills)
        {
            saveData.skillStates.Add(skill.name, new SkillState
            {
                isUnlocked = skill.isUnlocked,
                level = skill.level
            });
        }

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(saveFilePath, json);
    }

    // 从文件加载技能解锁状态
    private void LoadSkills()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            SkillSaveData saveData = JsonUtility.FromJson<SkillSaveData>(json);

            foreach (var skill in skills)
            {
                if (saveData.skillStates.ContainsKey(skill.name))
                {
                    SkillState state = saveData.skillStates[skill.name];
                    skill.isUnlocked = state.isUnlocked;
                    skill.level = state.level;

                    if (skill.isUnlocked)
                    {
                        // 执行技能解锁后的逻辑
                        Unlock(skill.name, skill.level);
                    }
                }
            }
        }
    }

    // 检查技能是否解锁
    public bool IsSkillUnlocked(string skillName)
    {
        Skill skill = skills.Find(s => s.name == skillName);
        return skill != null && skill.isUnlocked;
    }

    // 获取技能当前等级
    public int GetSkillLevel(string skillName)
    {
        Skill skill = skills.Find(s => s.name == skillName);
        return skill?.level ?? -1; // 如果技能未找到，返回 -1
    }
}

[System.Serializable]
// 技能类
public class Skill
{
    public string name; // 技能名称
    public List<int> Cost; // 每个等级的升级花费
    public bool isUnlocked; // 是否已解锁
    public int level; // 当前等级

    public Skill(string name, List<int> cost)
    {
        this.name = name;
        this.Cost = cost;
        this.isUnlocked = false;
        this.level = 0;
    }

    // 检查是否可以解锁下一等级
    public bool CanUnlock(int crystal)
    {
        return level < Cost.Count && crystal >= Cost[level];
    }

    // 解锁下一等级
    public void UnlockNextLevel()
    {
        if (level < Cost.Count)
        {
            level++;
            isUnlocked = true;
        }
    }
}

// 技能保存数据结构
[System.Serializable]
public class SkillSaveData
{
    public Dictionary<string, SkillState> skillStates = new Dictionary<string, SkillState>();
}

// 技能状态
[System.Serializable]
public class SkillState
{
    public bool isUnlocked;
    public int level;
}