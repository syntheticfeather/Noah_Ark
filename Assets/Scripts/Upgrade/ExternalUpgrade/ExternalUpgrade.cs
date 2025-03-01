using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ExternalUpgrade : MonoBehaviour
{
    // ǿ�Ƴ�ʼ��+���л�˫�ر���
    public List<Skill> skills = new List<Skill>();
    
    private InternalUpgrade internalUpgrade;

    // �ļ�·��
    private string saveFilePath;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        // ���ؼ��ܽ���״̬
        LoadSkills();

    }
    void Start()
    {
        // ��ʼ���ļ�·��
        saveFilePath = Path.Combine(Application.persistentDataPath, "skills_data.json");

        internalUpgrade = GetComponent<InternalUpgrade>();

    }

    public void UnlockSkill(string skillName)
    {
        int crystal = 0;
        ResourceManager resourceManager = ResourceManager.instance;
        crystal = resourceManager.Resource[3]; // ��ȡˮ������                    
        Skill skill = skills.Find(s => s.name == skillName);
        if (skill != null && skill.CanUnlock(crystal))
        {
            // �۳�ˮ��
            resourceManager.Resource[3] -= skill.Cost[skill.level]; // �۳�ˮ��
            skill.level++;
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
[System.Serializable]
// ������
public class Skill
{
    public int level = 0;
    public string name;
    public List<int> Cost;
    public bool isUnlocked;

    public bool CanUnlock(int crystal)
    {
        // ���ˮ���Ƿ��㹻�Ҽ���δ����
        return !isUnlocked && crystal >= Cost[level];
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