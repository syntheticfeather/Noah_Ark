using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanelController : MonoBehaviour
{
    public GameObject characterPanel;
    public bool isPanelActive = false;
    public List<GameObject> list = new List<GameObject>();// 动物属性面板
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isPanelActive == false)
            {
                // 切换Panel的活动状态，按c弹出面板            
                characterPanel.SetActive(true);
                isPanelActive = true;
                Time.timeScale = 0;
            }
            else
            {
                // 按C返回
                characterPanel.SetActive(false);
                isPanelActive = false;
                Time.timeScale = 1;
            }
        }
        
    }
    // 点击按钮弹出动物属性面板
    public void OnClickButton(Button button)
    {
        // 按钮父物体的transform
        Transform parentTransform = button.transform.parent;
        // 该按钮对应的Panel的transform
        Transform panel;

        // 先禁用所有属性面板
        foreach (GameObject gameObject in list)
        {
            gameObject.SetActive(false);
        }
        if (parentTransform != null)
        {
            // 获取到按钮对应的Panel                     
            panel = parentTransform.Find("Panel");
            if (panel != null)
            {
                // 启用特定面板
                panel.gameObject.SetActive(true);
            }
            
        }                
    }
    
}
