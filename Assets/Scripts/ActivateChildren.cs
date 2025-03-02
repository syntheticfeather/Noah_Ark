using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateChildren : MonoBehaviour
{
    [ContextMenu("激活所有子物体")]
    public void ActivateAllChildren()
    {
        ActivateRecursively(transform);
    }

    private void ActivateRecursively(Transform parent)
    {
        // 遍历直接子物体
        foreach (Transform child in parent)
        {
            if (child.name == "Setting")
            {
                continue;
            }
            // 激活当前子物体
            child.gameObject.SetActive(true);

            // 递归处理子物体的子物体
            ActivateRecursively(child);
        }
    }
}
