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
    public List<float> Resource = new List<float>();// 定义顺序为 木头 -> 0， 矿石 -> 1，食物 -> 2, crystal -> 3;
    // 引用 ExternalUpgrade 脚本
    
    // 水晶数量的键名
    public static string CrystalCountKey = "CrystalCount";

    void Start()
    {
        
    }
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        // 初始化水晶数量数据（如果不存在）
        if (!PlayerPrefs.HasKey(CrystalCountKey))
        {
            PlayerPrefs.SetFloat(CrystalCountKey, 0); // 默认水晶数量
            PlayerPrefs.Save(); // 保存数据
        }

        Resource[3] = PlayerPrefs.GetFloat("CrystalCount", 0);// 读写局外crystal数据
        if (ExternalUpgrade.Instance.IsSkillUnlocked("Extra Bed"))
        {
            InitialResource(10, 5, 20);
        }
        else
        {
            InitialResource(5, 2, 5);
        }
            
    }
    public void InitialResource(int initialWood, int initialStone, int initialFood)
    {
        // ，通过文件读写来初始化crystal资源
        Resource[0] = initialWood;
        Resource[1] = initialStone;
        Resource[2] = initialFood;
    }
    public void AddResource(float amount, int type)// 依据索引来添加资源
    {
        Resource[type] += amount;
        UIController.Instance.UpdateResourceUI();
    }

    public void AddCrystal()
    {
        AddResource(UnityEngine.Random.Range(1, 5), 3);// 随机掉落资源
        PlayerPrefs.SetFloat(CrystalCountKey, Resource[3]); // 默认水晶数量
        PlayerPrefs.Save(); // 保存数据
    }

    public bool UseCrystal(int amount)
    {
        if (Resource[3] >= amount)
        {
            Resource[3] -= amount;
            // 保存修改后的数据
            PlayerPrefs.SetFloat(CrystalCountKey, Resource[3]);
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