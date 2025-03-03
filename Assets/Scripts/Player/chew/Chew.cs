﻿using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine.UI;
using UnityEngine;



public class Chew : MonoBehaviour
{

    public ChewStats Stats; // 用于小动物升级的列表
    public Father father;// 对应的子弹的预制体父类
    public Sprite sprite;
    public Sprite Sprite_Pic;
    public GameObject mygameObject;
    
    //文本显示
    public Text damage;
    public Text range;
    public Text timeBetweenAttacks;
    public Text duration;    
    public Text cost;
    public Text level;
    public List<Text> talent;
   
    private void Start()
    {
        UpdateUI();
    }
    public void UpdateData(string attribute, float change)
    {
        switch (attribute.ToLower())
        {
            case "level":
                Stats.Level += change;
                break;
            case "damage":
                Stats.Damage += change;
                break;
            case "range":
                Stats.Range += change;
                break;
            case "timebetweenattacks":
                Stats.TimeBetweenAttacks += change;
                break;
            case "duration":
                Stats.Duration += change;
                break;
            case "cost":
                Stats.Cost += change;
                break;
            default:
                break;
        }
        Stats.Maxtake = Stats.Duration;
        UpdateUI();
    }
    public void UpdateUI()
    {
        ChewAI chewAI = mygameObject.GetComponent<ChewAI>();
        int index = ChewManager.Instance.allCrews.IndexOf(chewAI);
        //文本实时显示
        
        damage.text = (Math.Round(Stats.Damage,1)).ToString("F2");
        range.text = (Math.Round(Stats.Range,1)).ToString("F2");
        timeBetweenAttacks.text = (Math.Round(Stats.TimeBetweenAttacks, 1)).ToString("F2");
        duration.text = (Math.Round(Stats.Duration, 1)).ToString("F2");        
        cost.text = (Math.Round(Stats.Cost, 1)).ToString("F2");
        level.text = (Math.Round(Stats.Level, 1)).ToString();       
    }
    public void ApplyToBullet()
    {
        father.ATK += Stats.Damage;
        father.ATKSpeed += Stats.TimeBetweenAttacks;
        father.explosionRadius += Stats.Range;
    }
}