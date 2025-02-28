using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentSystem : MonoBehaviour
{
    public List<Talent> LowLevelTalents { get; private set; }
    public List<Talent> MidLevelTalents { get; private set; }
    public List<Talent> HighLevelTalents { get; private set; }
    private void Start()
    {
    }
    public TalentSystem()
    {
        LowLevelTalents = new List<Talent>
            {
                new Talent("初级攻击", "增加10%攻击力", stats => stats.Damage *= 1.1f),
                new Talent("初级射程", "提高10%攻击距离", stats => stats.Range *= 1.1f),
                new Talent("初级攻速", "提高10%攻击速度", stats => stats.TimeBetweenAttacks *= 0.9f),                
                new Talent("快速学习", "减少30%升级花费", stats => stats.Cost *= 0.7f),
                new Talent("初级负重", "提高50%采集上限", stats => stats.Duration *= (int)1.5f),
            };       
        MidLevelTalents = new List<Talent>
            {
                new Talent("二连发", "10%概率二连发", stats => stats.DoubleProjectile=true),
                new Talent("高级攻击", "增加20%攻击力", stats => stats.Damage *= 1.2f),                
                new Talent("高级射程", "提高20%攻击距离", stats => stats.Range *= 1.2f),
                new Talent("高级攻速", "提高20%攻击速度", stats => stats.TimeBetweenAttacks *= 0.8f),
                new Talent("高级负重", "提高100%采集上限", stats => stats.Duration *= (int)2.0f),
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
        List<Talent> availableTalents = new List<Talent>();       
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
            while (randomTalents.Count < 1)
            {              
                Talent talent = availableTalents[Random.Range(0, availableTalents.Count)];
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
