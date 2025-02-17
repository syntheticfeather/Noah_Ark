using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Resource : MonoBehaviour
{
    public int Amount = 10; // ������ľ����
    public GameObject selectionEffect; // ѡ����Ч
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
            // ȡ�����
            Unmark();
        }
    }

    public Vector3 GetHarvestPosition()
    {
        // ������ľ��Χ�Ŀ�����λ��
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
        // �����ľ
        isMarked = true;
        selectionEffect.SetActive(true);

        // ֪ͨ��Ա������
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
