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
    public List<int> Resource = new List<int>();// ����˳��Ϊ ľͷ -> 0�� ��ʯ -> 1��ʳ�� -> 2, crystal -> 3;
    // ���� ExternalUpgrade �ű�
    public ExternalUpgrade externalUpgrade;
    // ˮ�������ļ���
    private const string CrystalCountKey = "CrystalCount";

    void Start()
    {
        
    }
    private void Awake()
    {
        instance = this;
        // ��ʼ��ˮ���������ݣ���������ڣ�
        if (!PlayerPrefs.HasKey(CrystalCountKey))
        {
            PlayerPrefs.SetInt(CrystalCountKey, 0); // Ĭ��ˮ������
            PlayerPrefs.Save(); // ��������
        }

        Resource[3] = PlayerPrefs.GetInt("CrystalCount", 0);// ��д����crystal����
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
            // �����޸ĺ������
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