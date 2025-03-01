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
        // 加载技能解锁状态
        LoadSkills();

    }
    void Start()
    {
        // 初始化文件路径
        saveFilePath = Path.Combine(Application.persistentDataPath, "skills_data.json");

        internalUpgrade = GetComponent<InternalUpgrade>();

    }

    public void UnlockSkill(string skillName)
    {
        int crystal = 0;
        ResourceManager resourceManager = ResourceManager.instance;
        crystal = resourceManager.Resource[3]; // 获取水晶数量                    
        Skill skill = skills.Find(s => s.name == skillName);
        if (skill != null && skill.CanUnlock(crystal))
        {
            // 扣除水晶
            resourceManager.Resource[3] -= skill.Cost[skill.level]; // 扣除水晶
            skill.level++;
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
[System.Serializable]
// 技能类
public class Skill
{
    public int level = 0;
    public string name;
    public List<int> Cost;
    public bool isUnlocked;

    public bool CanUnlock(int crystal)
    {
        // 检查水晶是否足够且技能未解锁
        return !isUnlocked && crystal >= Cost[level];
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