using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Chew : MonoBehaviour
{
    public static Chew instance;
    [System.Serializable]
    public class ChewStats
    {
        public float Damage, Range, TimeBetweenAttacks, Duration, Labor, Cost, Level, Maxtake;
        public bool Leader, DoubleProjectile, AutoRepair;
    }

    public List<ChewStats> Stats; // 用于小动物升级的列表
    public Sprite sprite;
    public Sprite Sprite_Pic;

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
    }

    public class Talent
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Action<ChewStats> ApplyEffect { get; set; }

        public Talent(string name, string description, Action<ChewStats> applyEffect)
        {
            Name = name;
            Description = description;
            ApplyEffect = applyEffect;
        }
    }

    public class TalentSystem
    {
        public List<Talent> LowLevelTalents { get; private set; }
        public List<Talent> MidLevelTalents { get; private set; }
        public List<Talent> HighLevelTalents { get; private set; }

        public TalentSystem()
        {
            LowLevelTalents = new List<Talent>
            {
                new Talent("初级攻击", "增加10%攻击力", stats => stats.Damage *= 1.1f),
                new Talent("初级射程", "提高10%攻击距离", stats => stats.Range *= 1.1f),
                new Talent("初级攻速", "提高10%攻击速度", stats => stats.TimeBetweenAttacks *= 0.9f),
                new Talent("初级劳动", "提高30%采集速度", stats => stats.Labor *= 1.3f),
                new Talent("快速学习", "减少30%升级花费", stats => stats.Cost *= 0.7f),
                new Talent("初级负重", "提高50%采集上限", stats => stats.Maxtake *= 1.5f),
            };

            MidLevelTalents = new List<Talent>
            {
                new Talent("二连发", "10%概率二连发", stats => stats.DoubleProjectile=true),
                new Talent("高级攻击", "增加20%攻击力", stats => stats.Damage *= 1.2f),
                new Talent("高级劳动", "增加60%采集速度", stats => stats.Labor *= 1.6f),
                new Talent("高级射程", "提高20%移动速度", stats => stats.Range *= 1.2f),
                new Talent("高级攻速", "提高20%攻击速度", stats => stats.TimeBetweenAttacks *= 0.8f),
                new Talent("高级负重", "提高100%采集上限", stats => stats.Maxtake *= 2.0f),
            };

            HighLevelTalents = new List<Talent>
            {
                new Talent("领袖气质", "所有成员提升25%攻击力", stats => stats.Leader = true),
                new Talent("船只维护", "每隔30秒修补船只20%生命", stats => stats.AutoRepair = true),
            };
        }

        //升级时抽取天赋
        public List<Talent> GetRandomTalents(int level)
        {
            List<Talent> availableTalents = null;

            if (level == 4)
            {
                availableTalents = LowLevelTalents;
            }
            else if (level == 7)
            {
                availableTalents = MidLevelTalents;
            }
            else if (level == 10)
            {
                availableTalents = HighLevelTalents;
            }

            if (availableTalents != null)
            {
                List<Talent> randomTalents = new List<Talent>();
                System.Random rand = new System.Random();
                while (randomTalents.Count < 3)
                {
                    var talent = availableTalents[rand.Next(availableTalents.Count)];
                    if (!randomTalents.Contains(talent))
                    {
                        randomTalents.Add(talent);
                    }
                }
                return randomTalents;
            }

            return null;
        }
    }

    public class LevelSystem
    {
        public int Level { get; private set; }
        private TalentSystem talentSystem;
        private ChewStats stats;

        public LevelSystem(ChewStats initialStats)
        {
            Level = 1;
            talentSystem = new TalentSystem();
            stats = initialStats;
        }

        public void LevelUp(int index)
        {
            Level++;
            //属性提升
            Chew.instance.UpdateData(index, "Damage", 0.5f);
            Chew.instance.UpdateData(index, "Range", 2.0f);
            Chew.instance.UpdateData(index, "TimeBetweenAttacks", 0.05f);
            Chew.instance.UpdateData(index, "Duration", 0.1f);
            Chew.instance.UpdateData(index, "Labor", 1.0f);
            Chew.instance.UpdateData(index, "Cost", 1.0f);

            List<Talent> randomTalents = talentSystem.GetRandomTalents(Level);
            if (randomTalents != null)
            {
                //展示升级UI
                for (int i = 0; i < randomTalents.Count; i++)
                {

                }


                //选择天赋
                int choice = 0;
                randomTalents[choice].ApplyEffect(stats);

            }
        }
    }
}