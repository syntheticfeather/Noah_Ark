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
    public List<int> Resource = new List<int>();// ����˳��Ϊ ľͷ�� ��ʯ��ʳ��, crystal;
    private void Awake()
    {
        instance = this;
        Resource[3] = PlayerPrefs.GetInt("CrystalCount", 0);// ��д����crystal���ݡ�
        InitialResource(5, 0, 5);
    }
    public void InitialResource(int initialWood, int initialStone, int initialFood)
    {
        // ��ͨ���ļ���д����ʼ��crystal��Դ
        Resource[0] = initialWood;
        Resource[1] = initialStone;
        Resource[2] = initialFood;
    }
    public void AddResource(int amount, int type)// ���������������Դ
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