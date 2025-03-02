using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class ExternalUpgrade : MonoBehaviour
{
    public static ExternalUpgrade Instance;
    
    // ǿ�Ƴ�ʼ��+���л�˫�ر���
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
    // �ļ�·��
    private string saveFilePath;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
        // ��ʼ���ļ�·��
        saveFilePath = Path.Combine(Application.persistentDataPath, "skills_data.json");

        // ���ؼ��ܽ���״̬
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
        // ����ʱǿ������浵������ʱ�Ƴ���
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Delete))
        {
            File.Delete(saveFilePath);
            Debug.Log("�浵������");
        }
#endif
    }

    void Start()
    {
        crystal = PlayerPrefs.GetFloat("CrystalCount", 0); // ��д����crystal����
        if (crystalText!= null)
        crystalText.text = "Crystal:" + crystal.ToString();
        internalUpgrade = GetComponent<InternalUpgrade>();
    }

    public void UnlockSkill(string skillName)
    {

        Skill skill = skills.Find(s => s.name == skillName);

        if (skill != null && skill.CanUnlock(Mathf.FloorToInt(crystal)) && skill.level < skill.maxLevel)
        {
            // �۳�ˮ��
            crystal -= skill.Cost[skill.level]; // �۳���ǰ�ȼ�����������
            PlayerPrefs.SetFloat("CrystalCount", crystal);
            // ��������
            skill.UnlockNextLevel();
             // ����Ч�ػ�ȡ����ͼ�����
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

            // ִ�м��ܽ�������߼�
            Unlock(skill.name, skill.level);
            Debug.Log(skill.name + " unlocked");
            Debug.Log(skill.level);
            // ���漼�ܽ���״̬
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

        // ��ӵ������
        Debug.Log($"Saving to: {saveFilePath}");
        Debug.Log(json);

        File.WriteAllText(saveFilePath, json);
    }

    private void LoadSkills()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            Debug.Log($"Loading from: {saveFilePath}\n{json}"); // ������־

            SkillSaveData saveData = JsonUtility.FromJson<SkillSaveData>(json);

            foreach (var savedState in saveData.skillStates)
            {
                Skill targetSkill = skills.Find(s => s.name == savedState.skillName);
                if (targetSkill != null)
                {
                    targetSkill.isUnlocked = savedState.isUnlocked;
                    targetSkill.level = savedState.level;

                    // ִ�н����߼�
                    if (targetSkill.isUnlocked)
                    {
                        Unlock(targetSkill.name, targetSkill.level);

                        // ����UI״̬
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
    public int maxLevel; // ���ȼ�
    public Skill(string name, List<int> cost)
    {
        this.name = name;
        this.Cost = cost;
        this.isUnlocked = false;
        this.level = 0;
        this.maxLevel = cost.Count;
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

// �޸� SkillSaveData Ϊ�����л��ṹ
[System.Serializable]
public class SkillSaveData
{
    // ʹ�� List ��� Dictionary
    public List<SerializableSkillState> skillStates = new List<SerializableSkillState>();
}

// ���������л��ļ�ֵ�Խṹ
[System.Serializable]
public class SerializableSkillState
{
    public string skillName;
    public bool isUnlocked;
    public int level;
}