using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ChewManager : MonoBehaviour
{
    public static ChewManager Instance;

    public List<ChewAI> allCrews = new List<ChewAI>();
    public List<ChewAI> CrewsToBuy = new List<ChewAI>();
    public List<Resource> ResourceList = new List<Resource>();    
    public Queue<ChewAI> idleCrews = new Queue<ChewAI>();
    public void AllBack()
    {
        foreach (var item in allCrews)
        {            
            if (item != null)
            {
                if (!item.DirectToShip)
                {
                    item.DirectToShip = true;
                }
                else
                {
                    item.DirectToShip = false;
                    item.gameObject.SetActive(true);
                }
            }
        }
    }
    void Awake()
    {
        Instance = this;
        InitializeCrews();
    }

    private void Update()
    {
    }

    void InitializeCrews()
    {
        foreach (var crew in allCrews)
        {
            if (!idleCrews.Contains(crew))
            {
                idleCrews.Enqueue(crew);
            }
        }
    }

    public void AssignHarvestTask(Resource resource)
    {
        ResourceList.Add(resource);
        if (idleCrews.Count > 0)
        {
            ChewAI crew = idleCrews.Dequeue();
            if (crew != null)
            {
                Debug.Log(idleCrews.Count);
                crew.AssignTask(resource);
            }
        }
        else
        {
            // 显示UI提示"无可用船员"
            Debug.Log("No available crew");
        }
    }

    public void ReturnIdleCrew(ChewAI crew)
    {
        if (!idleCrews.Contains(crew))
        {
            idleCrews.Enqueue(crew);
        }
    }

    public TMP_Text text;
    public void tryBuyCrew()// button调用
    {
        Debug.Log("tryBuyCrew");
        if (ResourceManager.instance.Resource[2] >= ChewManager.Instance.CrewsToBuy[ChewBuyUI.instance.CurChewindex].GetComponent<Chew>().Stats.Cost)
        {
            // 可以购买
            ResourceManager.instance.Resource[2] -= Mathf.FloorToInt(ChewManager.Instance.CrewsToBuy[ChewBuyUI.instance.CurChewindex].GetComponent<Chew>().Stats.Cost);
            //丢进去
            ChewManager.Instance.allCrews.Add( ChewManager.Instance.CrewsToBuy[ChewBuyUI.instance.CurChewindex]);
        }
        else
        {
            text.text = "资源不足";
        }
        // 显示UI提示"购买船员"// 点击船员时
        //显示其数据，价格，以及资源消耗。
        // 调用资源系统的资源，对比是否可以。
        
        //若可以，将其添加至。allCrews中，并初始化。
        //若不可以，显示UI提示"资源不足"
        // 按esc退出？或者再按一次退出？
    }
}