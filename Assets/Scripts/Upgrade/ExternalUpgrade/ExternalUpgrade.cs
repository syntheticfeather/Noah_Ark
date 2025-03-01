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
        // ��ʼ���ļ�·��
        saveFilePath = Path.Combine(Application.persistentDataPath, "skills_data.json");

        // ���ؼ��ܽ���״̬
        LoadSkills();
    }

    void Start()
    {
        internalUpgrade = GetComponent<InternalUpgrade>();
    }

    public void UnlockSkill(string skillName)
    {
        int crystal = PlayerPrefs.GetInt("CrystalCount", 0); // ��д����crystal����
        Skill skill = skills.Find(s => s.name == skillName);

        if (skill != null && skill.CanUnlock(crystal))
        {
            // �۳�ˮ��
            crystal -= skill.Cost[skill.level]; // �۳���ǰ�ȼ�����������
            PlayerPrefs.SetInt("CrystalCount", crystal);

            // ��������
            skill.UnlockNextLevel();

            // ִ�м��ܽ�������߼�
            Unlock(skill.name, skill.level);

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
            saveData.skillStates.Add(skill.name, new SkillState
            {
                isUnlocked = skill.isUnlocked,
                level = skill.level
            });
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
                    skill.level = state.level;

                    if (skill.isUnlocked)
                    {
                        // ִ�м��ܽ�������߼�
                        Unlock(skill.name, skill.level);
                    }
                }
            }
        }
    }

    // ��鼼���Ƿ����
    public bool IsSkillUnlocked(string skillName)
    {
        Skill skill = skills.Find(s => s.name == skillName);
        return skill != null && skill.isUnlocked;
    }

    // ��ȡ���ܵ�ǰ�ȼ�
    public int GetSkillLevel(string skillName)
    {
        Skill skill = skills.Find(s => s.name == skillName);
        return skill?.level ?? -1; // �������δ�ҵ������� -1
    }
}

[System.Serializable]
// ������
public class Skill
{
    public string name; // ��������
    public List<int> Cost; // ÿ���ȼ�����������
    public bool isUnlocked; // �Ƿ��ѽ���
    public int level; // ��ǰ�ȼ�

    public Skill(string name, List<int> cost)
    {
        this.name = name;
        this.Cost = cost;
        this.isUnlocked = false;
        this.level = 0;
    }

    // ����Ƿ���Խ�����һ�ȼ�
    public bool CanUnlock(int crystal)
    {
        return level < Cost.Count && crystal >= Cost[level];
    }

    // ������һ�ȼ�
    public void UnlockNextLevel()
    {
        if (level < Cost.Count)
        {
            level++;
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
    public int level;
}