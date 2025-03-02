using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using UnityEngine;

public class InternalUpgrade : MonoBehaviour
{
    void Start()
    {        
    }
    public TMP_Text capacityText;
    public TMP_Text weaponText;

    public void UpgradeBeds()
    {
        if (Capacity.Instance.maxCapacity >= 6)
        {
            UpgradeText();
            return;
        }
        if (ResourceManager.instance.UseResource(10,6))
        {
            Capacity.Instance.maxCapacity += 1;
            UpgradeText();
        }
        
    }

    public void UpgradeCannoneerSlots()
    {
        if (Capacity.Instance.maxWeapon >= 6)
        {
            UpgradeText();
            return;
        }
        if (ResourceManager.instance.UseResource(20, 10))
        {
            Capacity.Instance.maxWeapon += 1;
            UpgradeText();
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
    public void UpgradeText()
    {
        capacityText.text = "Capacity: " + Capacity.Instance.maxCapacity;
        weaponText.text = "Cannoneer Slots: " + Capacity.Instance.maxWeapon;
        if (Capacity.Instance.maxCapacity >= 6)
        {
            capacityText.text = "MaxCapacity: 6";            
        }
        if (Capacity.Instance.maxWeapon >=6)
        {
            weaponText.text = "MaxCannoneerSlots: 6";            
        }
    }
}
