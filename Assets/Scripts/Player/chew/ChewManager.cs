using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChewManager : MonoBehaviour
{
    public static ChewManager Instance;

    public List<ChewAI> allCrews = new List<ChewAI>();
    public List<Resource> ResourceList = new List<Resource>();
    public int MaxChew = 5;
    private Queue<ChewAI> idleCrews = new Queue<ChewAI>();
    public void AllBack()
    {
        foreach (var item in allCrews)
        {
            item.DirectToShip = true;
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
            idleCrews.Enqueue(crew);
        }
    }

    public void AssignHarvestTask(Resource resource)
    {
        ResourceList.Add(resource);
        if (idleCrews.Count > 0)
        {
            ChewAI crew = idleCrews.Dequeue();
            crew.AssignTask(resource);
        }
        else
        {
            // 显示UI提示"无可用船员"
            Debug.Log("No available crew");
        }
    }

    public void ReturnIdleCrew(ChewAI crew)
    {
        idleCrews.Enqueue(crew);
    }
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.G))
    //    {
    //        AllBackToShip();
    //    }
    //}

    //public void AllBackToShip()
    //{
    //    foreach (var crew in allCrews)
    //    {
    //        crew.GoArk = true;
    //    }
    //}
}