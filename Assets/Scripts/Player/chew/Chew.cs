using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chew : MonoBehaviour
{   
    // 小动物的父类，所有小动物继承该类
    public List<ChewStats> Stats; // 用于小动物升级的列表
    public Sprite sprite;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
}

[System.Serializable]
public class ChewStats
{
    public float Damage, Range, TimeBetweenAttacks, Duration, Labor, Cost;
    //public string UpGradeText;?
}
