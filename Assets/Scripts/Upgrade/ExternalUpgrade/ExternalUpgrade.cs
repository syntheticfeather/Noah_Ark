using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class ExternalUpgrade : MonoBehaviour
{
    public static ExternalUpgrade Instance;
    
    // 强制初始化+序列化双重保障
    public List<Skill> skills = new List<Skill>
    {
        new Skill("Shield", new List<int> { 1000, 5000, 10000 }),
        new Skill("Speed", new List<int> { 512, 2048, 5000 }),
        new Skill("Extra Bed", new List<int> { 10000 }),
        new Skill("Dash", new List<int> { 500,5000,8198 })
    };
    private InternalUpgrade internalUpgrade;
    public float crystal;
    public TMP_Text crystalText;
    // 文件路径
    private string saveFilePath;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
        // 初始化文件路径
        saveFilePath = Path.Combine(Application.persistentDataPath, "skills_data.json");

        // 加载技能解锁状态
        LoadSkills();
        foreach (var skill in skills)
        {
            if (skill != null && skill.level == skill.maxLevel)
            {
                Text buttonText = GameObject.Find(skill.name + "Button")?.GetComponentInChildren<Text>();
                SkillPic skillPic = GameObject.Find(skill.name)?.GetComponent<SkillPic>();
                if (buttonText != null)
                {
                    buttonText.text = "Max Level";
                }
                if (skill.level == skill.maxLevel)
                {
                    skillPic.SkillCost.text = "Max Level";
                }
                else
                {
                    skillPic.SkillCost.text = "Cost:" + skill.Cost[skill.level].ToString();
                }
            }
        }
#if UNITY_EDITOR
        // 开发时强制清除存档（发布时移除）
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Delete))
        {
            File.Delete(saveFilePath);
            Debug.Log("存档已重置");
        }
#endif
    }

    void Start()
    {
        crystal = PlayerPrefs.GetFloat("CrystalCount", 0); // 读写局外crystal数据
        if (crystalText!= null)
        crystalText.text = "Crystal:" + crystal.ToString();
        internalUpgrade = GetComponent<InternalUpgrade>();
    }

    public void UnlockSkill(string skillName)
    {

        Skill skill = skills.Find(s => s.name == skillName);

        if (skill != null && skill.CanUnlock(Mathf.FloorToInt(crystal)) && skill.level < skill.maxLevel)
        {
            // 扣除水晶
            crystal -= skill.Cost[skill.level]; // 扣除当前等级的升级花费
            PlayerPrefs.SetFloat("CrystalCount", crystal);
            // 解锁技能
            skill.UnlockNextLevel();
             // 更高效地获取技能图标组件
            SkillPic skillPic = GameObject.Find(skillName)?.GetComponent<SkillPic>();
            if (skillPic != null)
            {
                skillPic.Changeimage(skill.level);
                if (skill.level == skill.maxLevel)
                {
                    skillPic.SkillCost.text = "Max Level";
                }
                else
                {
                    skillPic.SkillCost.text = "Cost:" + skill.Cost[skill.level].ToString();
                }
            }            

            // 执行技能解锁后的逻辑
            Unlock(skill.name, skill.level);
            Debug.Log(skill.name + " unlocked");
            Debug.Log(skill.level);
            // 保存技能解锁状态
            SaveSkills();
            crystalText.text = "Crystal:" + crystal.ToString();
        }
        if ( skill != null && skill.level == skill.maxLevel)
        {
            Text buttonText = GameObject.Find(skillName + "Button")?.GetComponentInChildren<Text>();
            if (buttonText != null)
            {
                buttonText.text = "Max Level";
            }
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

    private void SaveSkills()
    {
        SkillSaveData saveData = new SkillSaveData();

        foreach (var skill in skills)
        {
            saveData.skillStates.Add(new SerializableSkillState
            {
                skillName = skill.name,
                isUnlocked = skill.isUnlocked,
                level = skill.level
            });
        }

        string json = JsonUtility.ToJson(saveData, true);

        // 添加调试输出
        Debug.Log($"Saving to: {saveFilePath}");
        Debug.Log(json);

        File.WriteAllText(saveFilePath, json);
    }

    private void LoadSkills()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            Debug.Log($"Loading from: {saveFilePath}\n{json}"); // 调试日志

            SkillSaveData saveData = JsonUtility.FromJson<SkillSaveData>(json);

            foreach (var savedState in saveData.skillStates)
            {
                Skill targetSkill = skills.Find(s => s.name == savedState.skillName);
                if (targetSkill != null)
                {
                    targetSkill.isUnlocked = savedState.isUnlocked;
                    targetSkill.level = savedState.level;

                    // 执行解锁逻辑
                    if (targetSkill.isUnlocked)
                    {
                        Unlock(targetSkill.name, targetSkill.level);

                        // 更新UI状态
                        SkillPic skillPic = GameObject.Find(targetSkill.name)?.GetComponent<SkillPic>();
                        if (skillPic != null)
                        {
                            skillPic.Changeimage(targetSkill.level);

                        }
                    }
                }
            }
        }
        else
        {
            Debug.LogWarning("No save file found, initializing new data");
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
    public int maxLevel; // 最大等级
    public Skill(string name, List<int> cost)
    {
        this.name = name;
        this.Cost = cost;
        this.isUnlocked = false;
        this.level = 0;
        this.maxLevel = cost.Count;
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

// 修改 SkillSaveData 为可序列化结构
[System.Serializable]
public class SkillSaveData
{
    // 使用 List 替代 Dictionary
    public List<SerializableSkillState> skillStates = new List<SerializableSkillState>();
}

// 新增可序列化的键值对结构
[System.Serializable]
public class SerializableSkillState
{
    public string skillName;
    public bool isUnlocked;
    public int level;
}