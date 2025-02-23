using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class InternalUpgrade : MonoBehaviour
{
    public int Beds { get; set; }
    public int CannoneerSlots { get; set; }

    private ResourceManager resourceManager;
    private PlayerHealthController healthController;

    void Start()
    {
        resourceManager = GetComponent<ResourceManager>();
        healthController = GetComponent<PlayerHealthController>();
    }

    public void UpgradeBeds(int woodCost, int stoneCost)
    {
        if (resourceManager.UseResource(woodCost, stoneCost))
        {
            Beds++;
        }
       
    }

    public void UpgradeCannoneerSlots(int woodCost, int stoneCost)
    {
        if (resourceManager.UseResource(woodCost, stoneCost))
        {
            CannoneerSlots++;   
        }
    }

    public void RepairBoat(float percent)
    {

        int woodCost = (int)(percent * 10);
        int stoneCost = (int)(percent * 5);
        if (ResourceManager.instance.UseResource(woodCost, stoneCost))
        {
            PlayerHealthController.instance.Repair(percent);
        }
    }
}
