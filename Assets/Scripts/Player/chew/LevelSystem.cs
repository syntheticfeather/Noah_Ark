using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{   
    private TalentSystem talentSystem;
    private ChewStats stats;   
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

        talentSystem = new TalentSystem();      
        List<Talent> randomTalents = talentSystem.GetRandomTalents((int)chew.Stats.Level);
        Debug.Log("randomTalent" + randomTalents[0].Name);
        if (randomTalents != null)
        {            
            for (int i = 0; i < randomTalents.Count; i++)
            {
                chew.talent[i].text = randomTalents[1].Name 
                    + randomTalents[i].Description;


            }


            //选择天赋
            //int choice = 0;
            //randomTalents[choice].ApplyEffect(stats);

        }
    }
}

