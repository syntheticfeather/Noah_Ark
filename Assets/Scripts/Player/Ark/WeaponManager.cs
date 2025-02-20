using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;
    public List<GameObject> WeaponList = new List<GameObject>();
    public List<GameObject> AnimalList = new List<GameObject>();
    // 动物与所操控的武器索引一致
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateStatus()
    {
        for (int i = 0; i < AnimalList.Count; i++)
        {
            if (AnimalList[i])
            {
                if (AnimalList[i].GetComponent<ChewAI>().GetToWork)// 去工作则不能攻击
                {
                    if (AnimalList[i].name == "Noah")
                    {
                        WeaponList[i].GetComponent<PlayerWeapon>().CanAtk = false;
                    }
                    else
                    {
                        WeaponList[i].GetComponent<TurretAI>().CanAtk = false;
                    }
                }
                else
                {
                    if (AnimalList[i].name == "Noah")
                    {
                        WeaponList[i].GetComponent<PlayerWeapon>().CanAtk = true;
                    }
                    else
                    {
                        WeaponList[i].GetComponent<TurretAI>().CanAtk = true;
                    }
                }
            }
        }
    }
}
