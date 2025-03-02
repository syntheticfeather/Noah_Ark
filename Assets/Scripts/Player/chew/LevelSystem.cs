using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{   
    private TalentSystem talentSystem;
    public ChewStats stats;   
    public Button button;
    Chew chew;
    private void Start()
    {
        button.onClick.AddListener(LevelUp);
        
    }

    public void LevelUp()
    {    
        if (ResourceManager.instance.Resource[2] < stats.Cost)
        {
            return;
        }
        ResourceManager.instance.Resource[2] -= (int)stats.Cost;
        chew = GetComponent<Chew>();
        // Ù–‘Ã·…˝
        chew.UpdateData("Damage", UnityEngine.Random.Range(.2f, .7f));
        chew.UpdateData("Range", UnityEngine.Random.Range(.1f, .3f));
        chew.UpdateData("TimeBetweenAttacks", UnityEngine.Random.Range(.1f, .2f));
        chew.UpdateData("Duration", 0.1f);        
        chew.UpdateData("Cost", UnityEngine.Random.Range(2f, 5f));
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

