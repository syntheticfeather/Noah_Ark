using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Resource : MonoBehaviour
{
    public int Amount = 10; // 单棵树木材量
    public GameObject selectionEffect; // 选中特效
    public int type;
    private bool isMarked;
    private Vector3 harvestPosition;

    void OnMouseDown()
    {
        if (!isMarked && !CameraFollow.instance.followArk)
        {
            Mark();
        }
        else
        {
            // 取消标记
            Unmark();
        }
    }

    public Vector3 GetHarvestPosition()
    {
        // 计算树木周围的可行走位置
        return transform.position + Vector3.up * 2;
    }

    public void Unmark()
    {
        isMarked = false;
        selectionEffect.SetActive(false);
        ChewManager.Instance.ResourceList.Remove(this);
    }
    public void Mark()
    {
        // 标记树木
        isMarked = true;
        selectionEffect.SetActive(true);

        // 通知船员管理器
        ChewManager.Instance.AssignHarvestTask(this);
    }    
    public bool IsEmpty()
    {
        if (Amount <= 0)
        {
            gameObject.SetActive(false);
            selectionEffect.SetActive(false);
            ChewManager.Instance.ResourceList.Remove(this);
            return true;
        }
        return false;
    }
}
