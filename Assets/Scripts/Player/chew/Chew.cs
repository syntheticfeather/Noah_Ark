using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine.UI;
using UnityEngine;
using static Chew;


public class Chew : MonoBehaviour
{
    public static Chew instance;

    public List<ChewStats> Stats; // 用于小动物升级的列表
    public Sprite sprite;
    public Sprite Sprite_Pic;
    //文本显示
    public Text damage;
    public Text range;
    public Text timeBetweenAttacks;
    public Text duration;
    public Text labor;
    public Text cost;
    public Text level;
    private void Start()
    {
        //初始化
        for (int i = 0; i < 6; i++)
        {
            UpdateUI(i);
        }
    }
    public void UpdateData(int index, string attribute, float change)
    {
        var stat = Stats[index];
        switch (attribute.ToLower())
        {
            case "damage":
                stat.Damage += change;
                break;
            case "range":
                stat.Range += change;
                break;
            case "timebetweenattacks":
                stat.TimeBetweenAttacks -= change;
                break;
            case "duration":
                stat.Duration -= change;
                break;
            case "labor":
                stat.Labor += change;
                break;
            case "cost":
                stat.Cost += change;
                break;
            default:
                break;
        }
        UpdateUI(index);
    }
    public void UpdateUI(int index)
    {
        //文本实时显示
        damage.text = Chew.instance.Stats[index].Damage.ToString();
        range.text = Chew.instance.Stats[index].Range.ToString();
        timeBetweenAttacks.text = Chew.instance.Stats[index].TimeBetweenAttacks.ToString();
        duration.text = Chew.instance.Stats[index].Duration.ToString();
        labor.text = Chew.instance.Stats[index].Labor.ToString();
        cost.text = Chew.instance.Stats[index].Cost.ToString();
        level.text = Chew.instance.Stats[index].Level.ToString();
    }
}