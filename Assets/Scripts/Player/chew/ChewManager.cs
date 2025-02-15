using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChewManager : MonoBehaviour
{
    public static ChewManager Instance;

    public List<ChewAI> allCrews = new List<ChewAI>();
    public int MaxChew = 5;
    private Queue<ChewAI> idleCrews = new Queue<ChewAI>();

    void Awake()
    {
        Instance = this;
        InitializeCrews();
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
        if (idleCrews.Count > 0)
        {
            ChewAI crew = idleCrews.Dequeue();
            crew.AssignTask(resource);
        }
        else
        {
            // 显示UI提示"无可用船员"
            Debug.LogWarning("No available crew!");
        }
    }

    public void ReturnIdleCrew(ChewAI crew)
    {
        idleCrews.Enqueue(crew);
    }
}