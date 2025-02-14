using System.Collections;
using System.Collections.Generic;
using System.Resources;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class ExternalUpgrade : MonoBehaviour
{
    public List<Skill> skills;
    GameObject resourceManager_ = GameObject.Find("TotalManager");
    private InternalUpgrade internalUpgrade;


    void Start()
    {
        // 初始化技能系统
        InitializeSkills();
        internalUpgrade = GetComponent<InternalUpgrade>();
    }

    void InitializeSkills()
    {
        // 添加技能
        skills = new List<Skill>
        {
            new Skill("Shield", 200),
            new Skill("Speed", 200),
            new Skill("Extra Bed", 200),
            new Skill("Dash", 200)
        };
    }

    public bool UnlockSkill(string skillName)
    {
        int crystal=0;
        if(resourceManager_ != null)
{
            ResourceManager resourceManager = resourceManager_.GetComponent<ResourceManager>();
            if (resourceManager != null)
            {
                crystal = resourceManager.Resource[3];
            }
        }
        Skill skill = skills.Find(s => s.name == skillName);
        if (skill != null && skill.CanUnlock(crystal))
        {
            Unlock(skill.name);
            skill.UnLock();
            return true;
        }
        return false;
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
                internalUpgrade.Beds++;
                break;
            case "Dash":

                break;
            default:

                break;
        }
    }
}

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
        // 检查水晶是否足够
        return !isUnlocked && crystal >= cost;
    }

    public void UnLock()
    {
        isUnlocked = true;
    }
}


