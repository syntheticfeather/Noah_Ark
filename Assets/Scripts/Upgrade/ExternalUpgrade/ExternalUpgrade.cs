using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ExternalUpgrade : MonoBehaviour
{
    public List<Skill> skills;
    private GameObject resourceManager_;
    private InternalUpgrade internalUpgrade;

    // 文件路径
    private string saveFilePath;

    void Start()
    {
        // 初始化文件路径
        saveFilePath = Path.Combine(Application.persistentDataPath, "skills_data.json");

        // 初始化技能系统
        InitializeSkills();
        internalUpgrade = GetComponent<InternalUpgrade>();

        // 加载技能解锁状态
        LoadSkills();
    }

    void InitializeSkills()
    {
        // 添加技能
        skills = new List<Skill>
        {
            new Skill("Shield", new List<int> { 10, 20, 30 }), // 每个等级的升级花费
            new Skill("Speed", new List<int> { 15, 25, 35 }),
            new Skill("Extra Bed", new List<int> { 20 }), // Extra Bed 只有一个等级
            new Skill("Dash", new List<int> { 12, 22, 32 })
        };
    }

    public void UnlockSkill(string skillName)
    {
        int crystal = 0;
        if (resourceManager_ != null)
        {
            ResourceManager resourceManager = resourceManager_.GetComponent<ResourceManager>();
            if (resourceManager != null)
            {
                crystal = resourceManager.Resource[3]; // 获取水晶数量
            }
        }

        Skill skill = skills.Find(s => s.name == skillName);
        if (skill != null && skill.CanUnlock(crystal))
        {
            // 扣除水晶
            if (resourceManager_ != null)
            {
                ResourceManager resourceManager = resourceManager_.GetComponent<ResourceManager>();
                if (resourceManager != null)
                {
                    resourceManager.Resource[3] -= skill.GetUpgradeCost(); // 扣除当前等级的升级花费
                }
            }

            // 解锁技能
            skill.UnlockNextLevel();

            // 执行技能解锁后的逻辑
            Unlock(skill.name, skill.currentLevel);

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
            saveData.skillStates.Add(skill.name, new SkillState { isUnlocked = skill.isUnlocked, currentLevel = skill.currentLevel });
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
                    skill.currentLevel = state.currentLevel;
                    if (skill.isUnlocked)
                    {
                        // 执行技能解锁后的逻辑
                        Unlock(skill.name, skill.currentLevel);
                    }
                }
            }
        }
    }

    // 检查技能是否解锁
    public bool IsSkillUnlocked(string skillName)
    {
        Skill skill = skills.Find(s => s.name == skillName);
        if (skill != null)
        {
            return skill.isUnlocked;
        }
        return false;
    }
}

// 技能类
[System.Serializable]
public class Skill
{
    public string name; // 技能名称
    public List<int> upgradeCosts; // 每个等级的升级花费
    public bool isUnlocked; // 是否已解锁
    public int currentLevel; // 当前等级

    public Skill(string name, List<int> upgradeCosts)
    {
        this.name = name;
        this.upgradeCosts = upgradeCosts;
        this.isUnlocked = false;
        this.currentLevel = 0;
    }

    // 获取当前等级的升级花费
    public int GetUpgradeCost()
    {
        if (currentLevel < upgradeCosts.Count)
        {
            return upgradeCosts[currentLevel];
        }
        return -1; // 已达到最大等级
    }

    // 检查是否可以解锁下一等级
    public bool CanUnlock(int crystal)
    {
        return currentLevel < upgradeCosts.Count && crystal >= GetUpgradeCost();
    }

    // 解锁下一等级
    public void UnlockNextLevel()
    {
        if (currentLevel < upgradeCosts.Count)
        {
            currentLevel++;
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
    public int currentLevel;
}