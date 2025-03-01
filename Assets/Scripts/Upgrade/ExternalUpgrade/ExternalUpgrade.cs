using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ExternalUpgrade : MonoBehaviour
{
    public List<Skill> skills;
    private GameObject resourceManager_;
    private InternalUpgrade internalUpgrade;

    // �ļ�·��
    private string saveFilePath;

    void Start()
    {
        // ��ʼ���ļ�·��
        saveFilePath = Path.Combine(Application.persistentDataPath, "skills_data.json");

        // ��ʼ������ϵͳ
        InitializeSkills();
        internalUpgrade = GetComponent<InternalUpgrade>();

        // ���ؼ��ܽ���״̬
        LoadSkills();
    }

    void InitializeSkills()
    {
        // ��Ӽ���
        skills = new List<Skill>
        {
            new Skill("Shield", new List<int> { 10, 20, 30 }), // ÿ���ȼ�����������
            new Skill("Speed", new List<int> { 15, 25, 35 }),
            new Skill("Extra Bed", new List<int> { 20 }), // Extra Bed ֻ��һ���ȼ�
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
                crystal = resourceManager.Resource[3]; // ��ȡˮ������
            }
        }

        Skill skill = skills.Find(s => s.name == skillName);
        if (skill != null && skill.CanUnlock(crystal))
        {
            // �۳�ˮ��
            if (resourceManager_ != null)
            {
                ResourceManager resourceManager = resourceManager_.GetComponent<ResourceManager>();
                if (resourceManager != null)
                {
                    resourceManager.Resource[3] -= skill.GetUpgradeCost(); // �۳���ǰ�ȼ�����������
                }
            }

            // ��������
            skill.UnlockNextLevel();

            // ִ�м��ܽ�������߼�
            Unlock(skill.name, skill.currentLevel);

            // ���漼�ܽ���״̬
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

    // ���漼�ܽ���״̬���ļ�
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

    // ���ļ����ؼ��ܽ���״̬
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
                        // ִ�м��ܽ�������߼�
                        Unlock(skill.name, skill.currentLevel);
                    }
                }
            }
        }
    }

    // ��鼼���Ƿ����
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

// ������
[System.Serializable]
public class Skill
{
    public string name; // ��������
    public List<int> upgradeCosts; // ÿ���ȼ�����������
    public bool isUnlocked; // �Ƿ��ѽ���
    public int currentLevel; // ��ǰ�ȼ�

    public Skill(string name, List<int> upgradeCosts)
    {
        this.name = name;
        this.upgradeCosts = upgradeCosts;
        this.isUnlocked = false;
        this.currentLevel = 0;
    }

    // ��ȡ��ǰ�ȼ�����������
    public int GetUpgradeCost()
    {
        if (currentLevel < upgradeCosts.Count)
        {
            return upgradeCosts[currentLevel];
        }
        return -1; // �Ѵﵽ���ȼ�
    }

    // ����Ƿ���Խ�����һ�ȼ�
    public bool CanUnlock(int crystal)
    {
        return currentLevel < upgradeCosts.Count && crystal >= GetUpgradeCost();
    }

    // ������һ�ȼ�
    public void UnlockNextLevel()
    {
        if (currentLevel < upgradeCosts.Count)
        {
            currentLevel++;
            isUnlocked = true;
        }
    }
}

// ���ܱ������ݽṹ
[System.Serializable]
public class SkillSaveData
{
    public Dictionary<string, SkillState> skillStates = new Dictionary<string, SkillState>();
}

// ����״̬
[System.Serializable]
public class SkillState
{
    public bool isUnlocked;
    public int currentLevel;
}