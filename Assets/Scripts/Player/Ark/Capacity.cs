using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Capacity : MonoBehaviour
{
    public static Capacity Instance;
    private void Awake()
    {
        Instance = this;
    }
    public int CurrentCapacity;
    public int maxCapacity;    
    public int maxWeapon;
    // Start is called before the first frame update
    void Start()
    {
        if (ExternalUpgrade.Instance.skills[2].level ==1)
        {
            maxCapacity = 2;
            maxWeapon = 2;
        }
        else
        {
            maxCapacity = 1;
            maxWeapon = 1;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public List<GameObject> Buttons;
    public void UpdateCapacity()
    {
        for (int i = 0; i < maxWeapon; i++)
        {

            Buttons[i].GetComponent<Button>().enabled = true;
            Buttons[i].GetComponent<Image>().color = Color.white;
        }
        for (int i = maxWeapon; i < Buttons.Count; i++)// 超出不能用
        {
            Buttons[i].GetComponent<Button>().enabled = false;
            Buttons[i].GetComponent<Image>().color = Color.black;
        }
    }
}
