using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    public LevelSystem levelSystemInstance;
    ChewStats initialStats_1 = new ChewStats
    {
        Damage = 1f,         // 示例数值，根据实际情况调整
        Range = 15f,           // 示例数值，根据实际情况调整
        TimeBetweenAttacks = 1.5f, // 示例数值，根据实际情况调整
        Duration = 10f,       // 示例数值，根据实际情况调整
        Labor = 10f,          // 示例数值，根据实际情况调整
        Cost = 10f,          // 示例数值，根据实际情况调整
        Level = 1f,           // 示例数值，根据实际情况调整
        Maxtake = 3f,         // 示例数值，根据实际情况调整
        Leader = false,       // 示例布尔值，根据实际情况调整
        DoubleProjectile = false, // 示例布尔值，根据实际情况调整
        AutoRepair = false     // 示例布尔值，根据实际情况调整
    };
    // Start is called before the first frame update
    void Start()
    {
        //实例化嵌套类
        levelSystemInstance = new LevelSystem(initialStats_1);
    }

    //调用升级函数
    public void Onclick(int index)
    {
        levelSystemInstance.LevelUp(index);
    }
    
}
