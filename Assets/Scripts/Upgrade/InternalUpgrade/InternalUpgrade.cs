using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class InternalUpgrade : MonoBehaviour
{
    public int Beds { get; set; }
    public int CannoneerSlots { get; set; }

    private ResourceManager resourceManager;

    void Start()
    {
        resourceManager = GetComponent<ResourceManager>();
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
}
