using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    public LevelSystem levelSystemInstance;
    ChewStats initialStats_1 = new ChewStats
    {
        Damage = 1f,         // ʾ����ֵ������ʵ���������
        Range = 15f,           // ʾ����ֵ������ʵ���������
        TimeBetweenAttacks = 1.5f, // ʾ����ֵ������ʵ���������
        Duration = 10f,       // ʾ����ֵ������ʵ���������
        Labor = 10f,          // ʾ����ֵ������ʵ���������
        Cost = 10f,          // ʾ����ֵ������ʵ���������
        Level = 1f,           // ʾ����ֵ������ʵ���������
        Maxtake = 3f,         // ʾ����ֵ������ʵ���������
        Leader = false,       // ʾ������ֵ������ʵ���������
        DoubleProjectile = false, // ʾ������ֵ������ʵ���������
        AutoRepair = false     // ʾ������ֵ������ʵ���������
    };
    // Start is called before the first frame update
    void Start()
    {
        //ʵ����Ƕ����
        levelSystemInstance = new LevelSystem(initialStats_1);
    }

    //������������
    public void Onclick(int index)
    {
        levelSystemInstance.LevelUp(index);
    }
    
}
