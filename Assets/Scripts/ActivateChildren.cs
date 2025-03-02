using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateChildren : MonoBehaviour
{
    [ContextMenu("��������������")]
    public void ActivateAllChildren()
    {
        ActivateRecursively(transform);
    }

    private void ActivateRecursively(Transform parent)
    {
        // ����ֱ��������
        foreach (Transform child in parent)
        {
            if (child.name == "Setting")
            {
                continue;
            }
            // ���ǰ������
            child.gameObject.SetActive(true);

            // �ݹ鴦���������������
            ActivateRecursively(child);
        }
    }
}
