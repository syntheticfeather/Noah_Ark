using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Resource : MonoBehaviour
{
    public int woodAmount = 10; // 单棵树木材量
    public GameObject selectionEffect; // 选中特效
    public int type;
    private bool isMarked;
    private Vector3 harvestPosition;

    void OnMouseDown()
    {
        if (!isMarked && !CameraFollow.instance.followArk)
        {
            // 标记树木
            isMarked = true;
            selectionEffect.SetActive(true);

            // 通知船员管理器
            ChewManager.Instance.AssignHarvestTask(this);
        }
    }

    public Vector3 GetHarvestPosition()
    {
        // 计算树木周围的可行走位置
        return transform.position + Vector3.up * 2;
    }
}
