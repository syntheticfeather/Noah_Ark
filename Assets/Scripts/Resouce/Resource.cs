using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    private int i;
    public int type;
    //����
    public int Amount;
    //�ɼ�ʱ��
    public float CollectTime;
    public float Counter;

    //��Ƿ�
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
            //ChewController.instance.ChewList[i];�ó�Աǰȥ�ɼ�
            //i++;�л�����һ��
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
        // ��Ǵ��ɼ�
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
