using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    private int i;
    public int type;
    //总量
    public int Amount;
    //采集时长
    public float CollectTime;
    public float Counter;

    //标记符
    private bool isCollecting = false;
    private bool isToCollect = false;
    // Start is called before the first frame update
    void Start()
    {
        Counter = CollectTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (isToCollect && !CameraFollow.instance.followArk)
        {
            //ChewController.instance.ChewList[i];该成员前去采集
            //i++;切换到下一人
        }
        if (isCollecting)
        {
            Counter -= Time.deltaTime;
        }
        if (Counter < 0)
        {
            ResourceManager.instance.AddResource(Amount, type);
        }
    }

    private void OnMouseDown()
    {
        // 标记待采集
        isToCollect = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isCollecting = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isCollecting = false;
    }
}
