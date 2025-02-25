using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
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
        //��������
        Chew.instance.UpdateData(index, "Damage", 0.5f);
        Chew.instance.UpdateData(index, "Range", 2.0f);
        Chew.instance.UpdateData(index, "TimeBetweenAttacks", 0.1f);
        Chew.instance.UpdateData(index, "Duration", 0.1f);
        Chew.instance.UpdateData(index, "Labor", 1.0f);
        Chew.instance.UpdateData(index, "Cost", 1.0f);


        List<Talent> randomTalents = talentSystem.GetRandomTalents(Level);
        if (randomTalents != null)
        {
            //չʾ����UI
            for (int i = 0; i < randomTalents.Count; i++)
            {

            }


            //ѡ���츳
            int choice = 0;
            randomTalents[choice].ApplyEffect(stats);

        }
    }
}

