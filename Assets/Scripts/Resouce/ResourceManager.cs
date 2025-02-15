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
    public List<int> Resource = new List<int>();// 定义顺序为 木头， 矿石，食物, crystal;
    private void Awake()
    {
        instance = this;
        Resource[3] = PlayerPrefs.GetInt("CrystalCount", 0);// 读写局外crystal数据。
        InitialResource(5, 0, 5);
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