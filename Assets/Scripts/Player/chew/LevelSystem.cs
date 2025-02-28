using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Button button;
    private void Start()
    {
        button.onClick.AddListener(LevelUp);
    }

    public void LevelUp()
    {    
        Chew chew = GetComponent<Chew>();
        ChewAI chewAI = GetComponent<ChewAI>();
        int Index = ChewManager.Instance.allCrews.IndexOf(chewAI);
        //属性提升
        chew.UpdateData("Damage", 0.5f);
        chew.UpdateData("Range", 2.0f);
        chew.UpdateData("TimeBetweenAttacks", 0.1f);
        chew.UpdateData("Duration", 0.1f);        
        chew.UpdateData("Cost", 1.0f);
        chew.UpdateData("Level", 1f);


        List<Talent> randomTalents = talentSystem.GetRandomTalents((int)(chew.Stats.Level));
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

