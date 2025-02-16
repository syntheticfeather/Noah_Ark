using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Resource : MonoBehaviour
{
    public int woodAmount = 10; // ������ľ����
    public GameObject selectionEffect; // ѡ����Ч
    public int type;
    private bool isMarked;
    private Vector3 harvestPosition;

    void OnMouseDown()
    {
        if (!isMarked && !CameraFollow.instance.followArk)
        {
            // �����ľ
            isMarked = true;
            selectionEffect.SetActive(true);

            // ֪ͨ��Ա������
            ChewManager.Instance.AssignHarvestTask(this);
        }
    }

    public Vector3 GetHarvestPosition()
    {
        // ������ľ��Χ�Ŀ�����λ��
        return transform.position + Vector3.up * 2;
    }
}
