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
        // 初始化科技树
        InitializeTechTree();
    }

    void InitializeTechTree()
    {
        // 添加科技(分别为科技名，花费，前置科技）
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
        // 检查资源是否足够以及前置科技是否已解锁
        return !isUnlocked && availableResources >= cost && ArePrerequisitesMet();
    }

    public void Unlock()
    {
        isUnlocked = true;
        // 解锁科技的效果
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
        // 检查所有前置科技是否已解锁
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

