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
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
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
                    resourceManager.Resource[3] -= skill.cost; // �۳�ˮ��
                    
                }
            }

            // ��������
            skill.UnLock();
            

            // ִ�м��ܽ�������߼�
            Unlock(skill.name);

            // ���漼�ܽ���״̬
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

    // ���漼�ܽ���״̬���ļ�
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
                    skill.isUnlocked = saveData.skillStates[skill.name];
                    if (skill.isUnlocked)
                    {
                       
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
        // ���ˮ���Ƿ��㹻�Ҽ���δ����
        return !isUnlocked && crystal >= cost;
    }

    public void UnLock()
    {
        isUnlocked = true;
    }
}

// ���ܱ������ݽṹ
[System.Serializable]
public class SkillSaveData
{
    public Dictionary<string, bool> skillStates = new Dictionary<string, bool>();
}