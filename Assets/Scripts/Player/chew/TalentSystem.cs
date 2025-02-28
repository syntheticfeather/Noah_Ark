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
                new Talent("��������", "����10%������", stats => stats.Damage *= 1.1f),
                new Talent("�������", "���10%��������", stats => stats.Range *= 1.1f),
                new Talent("��������", "���10%�����ٶ�", stats => stats.TimeBetweenAttacks *= 0.9f),                
                new Talent("����ѧϰ", "����30%��������", stats => stats.Cost *= 0.7f),
                new Talent("��������", "���50%�ɼ�����", stats => stats.Duration *= (int)1.5f),
            };       
        MidLevelTalents = new List<Talent>
            {
                new Talent("������", "10%���ʶ�����", stats => stats.DoubleProjectile=true),
                new Talent("�߼�����", "����20%������", stats => stats.Damage *= 1.2f),                
                new Talent("�߼����", "���20%��������", stats => stats.Range *= 1.2f),
                new Talent("�߼�����", "���20%�����ٶ�", stats => stats.TimeBetweenAttacks *= 0.8f),
                new Talent("�߼�����", "���100%�ɼ�����", stats => stats.Duration *= (int)2.0f),
            };

        HighLevelTalents = new List<Talent>
            {
                new Talent("��������", "���г�Ա����25%������", stats => stats.Leader = true),
                new Talent("��ֻά��", "ÿ��30���޲���ֻ20%����", stats => stats.AutoRepair = true),
            };
    }

    //����ʱ��ȡ�츳
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
