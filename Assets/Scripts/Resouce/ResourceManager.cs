using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager instance;
    public List<int> Resource = new List<int>();// 定义顺序为 木头 -> 0， 矿石 -> 1，食物 -> 2, crystal -> 3;
    // 引用 ExternalUpgrade 脚本
    public ExternalUpgrade externalUpgrade;
    // 水晶数量的键名
    private const string CrystalCountKey = "CrystalCount";

    void Start()
    {
        
    }
    private void Awake()
    {
        instance = this;
        // 初始化水晶数量数据（如果不存在）
        if (!PlayerPrefs.HasKey(CrystalCountKey))
        {
            PlayerPrefs.SetInt(CrystalCountKey, 0); // 默认水晶数量
            PlayerPrefs.Save(); // 保存数据
        }

        Resource[3] = PlayerPrefs.GetInt("CrystalCount", 0);// 读写局外crystal数据
        if (externalUpgrade != null && externalUpgrade.IsSkillUnlocked("Extra Bed"))
        {
            InitialResource(15, 3, 150);
        }
        else
        {
            InitialResource(5, 1, 50);
        }
            
    }
    public void InitialResource(int initialWood, int initialStone, int initialFood)
    {
        // ，通过文件读写来初始化crystal资源
        Resource[0] = initialWood;
        Resource[1] = initialStone;
        Resource[2] = initialFood;
    }
    public void AddResource(int amount, int type)// 依据索引来添加资源
    {
        Resource[type] += amount;
        UIController.Instance.UpdateResourceUI();
    }

    public bool UseCrystal(int amount)
    {
        if (Resource[3] >= amount)
        {
            Resource[3] -= amount;
            // 保存修改后的数据
            PlayerPrefs.SetInt(CrystalCountKey, Resource[3]);
            PlayerPrefs.Save();
            return true;
        }
        else
        {
            return false;
        }
       
    }

    public bool UseResource(int FoodAmount, int StoneAmount)
    {
        if (Resource[0] >= FoodAmount && Resource[1] >= StoneAmount)
        {
            Resource[1] -= StoneAmount;
            Resource[0] -= FoodAmount;
            return true;
        }

        else
        {
            return false;
        }
    }
}