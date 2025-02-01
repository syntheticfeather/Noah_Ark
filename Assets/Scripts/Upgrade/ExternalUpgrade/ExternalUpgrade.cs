using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class ExternalUpgrade : MonoBehaviour
{
    private ResourceManager resourceManager;
    void Start()
    {
        resourceManager = GetComponent<ResourceManager>();
    }

}

public class CompleteTechTree : MonoBehaviour
{
    public static CompleteTechTree Instance;
    public List<TechNode> techNodes;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // ��ʼ���Ƽ���
        InitializeTechTree();
    }

    void InitializeTechTree()
    {
        // ��ӿƼ�(�ֱ�Ϊ�Ƽ��������ѣ�ǰ�ÿƼ���
        techNodes = new List<TechNode>
        {
            new TechNode("Resource Gathering", 100, null),
            new TechNode("Enemy Delaying", 150, null)
        };
    }

    public bool UnlockTech(string techName, int crystal)
    {
        TechNode node = techNodes.Find(t => t.name == techName);
        if (node != null && node.CanUnlock(crystal))
        {
            node.Unlock();
            return true;
        }
        return false;
    }

    public bool IsTechUnlocked(string techName)
    {
        TechNode node = techNodes.Find(t => t.name == techName);
        return node != null && node.isUnlocked;
    }
}

public class TechNode
{
    public string name;
    public int cost;
    public bool isUnlocked;
    public List<string> prerequisites;

    public TechNode(string name, int cost, List<string> prerequisites)
    {
        this.name = name;
        this.cost = cost;
        this.isUnlocked = false;
        this.prerequisites = prerequisites ?? new List<string>();
    }

    public bool CanUnlock(int availableResources)
    {
        // �����Դ�Ƿ��㹻�Լ�ǰ�ÿƼ��Ƿ��ѽ���
        return !isUnlocked && availableResources >= cost && ArePrerequisitesMet();
    }

    public void Unlock()
    {
        isUnlocked = true;
        // �����Ƽ���Ч��
        switch (name)
        {
            case "Resource Gathering":

                break;
            default:
                break;
 
        }
    }

    private bool ArePrerequisitesMet()
    {
        // �������ǰ�ÿƼ��Ƿ��ѽ���
        foreach (var prerequisite in prerequisites)
        {
            if (!CompleteTechTree.Instance.IsTechUnlocked(prerequisite))
            {
                return false;
            }
        }
        return true;
    }
}

