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
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
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
            new Skill("Shield", 20),
            new Skill("Speed", 20),
            new Skill("Extra Bed", 20),
            new Skill("Dash", 20)
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
                    resourceManager.Resource[3] -= skill.cost; // 扣除水晶
                    
                }
            }

            // 解锁技能
            skill.UnLock();
            

            // 执行技能解锁后的逻辑
            Unlock(skill.name);

            // 保存技能解锁状态
            SaveSkills();
            
        }
       
       
    }

    public void Unlock(string name)
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
            saveData.skillStates.Add(skill.name, skill.isUnlocked);
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
                    skill.isUnlocked = saveData.skillStates[skill.name];
                    if (skill.isUnlocked)
                    {
                       
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
public class Skill
{
    public string name;
    public int cost;
    public bool isUnlocked;

    public Skill(string name, int cost)
    {
        this.name = name;
        this.cost = cost;
        this.isUnlocked = false;
    }

    public bool CanUnlock(int crystal)
    {
        // 检查水晶是否足够且技能未解锁
        return !isUnlocked && crystal >= cost;
    }

    public void UnLock()
    {
        isUnlocked = true;
    }
}

// 技能保存数据结构
[System.Serializable]
public class SkillSaveData
{
    public Dictionary<string, bool> skillStates = new Dictionary<string, bool>();
}