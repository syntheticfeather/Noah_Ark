using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SkillLevelLoader : MonoBehaviour
{
    // ����ʵ��
    public static SkillLevelLoader Instance { get; private set; }

    // �ļ�·��
    private string saveFilePath;

    void Awake()
    {
        // ����ģʽ��ʼ��
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ���ֶ����ڳ����л�ʱ������
        }
        else
        {
            Destroy(gameObject); // ����Ѵ���ʵ���������¶���
            return;
        }

        // ��ʼ���ļ�·��
        saveFilePath = Path.Combine(Application.persistentDataPath, "skills_data.json");
    }

    // ��ȡָ�����ܵĵȼ�
    public int GetSkillLevel(string skillName)
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath); // ��ȡ�ļ�
            SkillSaveData saveData = JsonUtility.FromJson<SkillSaveData>(json); 

            if (saveData.skillStates.TryGetValue(skillName, out SkillState state))
            {
                return state.currentLevel; // ���ؼ��ܵȼ�
            }
        }
        return -1; // �ļ������ڻ���δ�ҵ�
    }

    // ���ܱ������ݽṹ
    [System.Serializable]
    private class SkillSaveData
    {
        public Dictionary<string, SkillState> skillStates = new Dictionary<string, SkillState>();
    }

    // ����״̬
    [System.Serializable]
    private class SkillState
    {
        public bool isUnlocked;
        public int currentLevel;
    }
}