using System;
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
    Chew chew;
    private void Start()
    {
        button.onClick.AddListener(LevelUp);
        
    }

    public void LevelUp()
    {    
        chew = GetComponent<Chew>();
        ChewAI chewAI = GetComponent<ChewAI>();
        int Index = ChewManager.Instance.allCrews.IndexOf(chewAI);
        // Ù–‘Ã·…˝
        chew.UpdateData("Damage", 0.5f);
        chew.UpdateData("Range", 2.0f);
        chew.UpdateData("TimeBetweenAttacks", 0.1f);
        chew.UpdateData("Duration", 0.1f);        
        chew.UpdateData("Cost", 1.0f);
        chew.UpdateData("Level", 1f);

        talentSystem = new TalentSystem();      
        List<Talent> randomTalents = talentSystem.GetRandomTalents((int)chew.Stats.Level);        
        if (randomTalents != null)
        {
            if ((int)chew.Stats.Level == 4)
            {
                chew.talent[0].text = randomTalents[0].Name+ "\n"
                    + randomTalents[0].Description;
            }
            if ((int)chew.Stats.Level == 7)
            {
                chew.talent[1].text = randomTalents[0].Name+ "\n"
                    + randomTalents[0].Description;
            }
            if ((int)chew.Stats.Level == 10)
            {
                chew.talent[2].text = randomTalents[0].Name+ "\n"
                    + randomTalents[0].Description;
            }
            randomTalents[0].ApplyEffect(chew.Stats);

        }
        if (chew.Stats.Leader == true)
        {
            GameObject[] chews = GameObject.FindGameObjectsWithTag("Chew");
            for (int i = 0; i < chews.Length; i++)
            {
                chews[i].GetComponent<ChewStats>().Damage *= 1.25f;
            }
            chew.Stats.Leader = false;
        }
        
        if (chew.Stats.DoubleProjectile == true)
        {
            
        }
    }
    private void HealthAdd(bool i)
    {
        if (i == true)
        {
            float timeToRepair = 30;            
            if (timeToRepair >= 0)
            {
                timeToRepair -= Time.deltaTime;
            }
            else
            {
                timeToRepair = 30;
                PlayerHealthController.instance.Repair(0.2f);
            }
        }
    }
    private void Update()
    {
        if (chew != null)
        {
            HealthAdd(chew.Stats.AutoRepair);
        }
    }
    
}

