using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanelController : MonoBehaviour
{
    public GameObject characterPanel;
    public bool isPanelActive = false;
    public List<GameObject> list = new List<GameObject>();// 动物属性面板
    public List<GameObject> Button = new List<GameObject>();//动物的六个按钮
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isPanelActive == false)
            {
                // 遍历所有按钮，将其激活
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
            if (gameObject)
            {
                gameObject.SetActive(false);                
            }
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
    //翻页
    public void ChangePage()
    {
        //如果第一页的面板被激活，就打开第二页
        if (Button[0].activeSelf == true)
        {
            Button[0].SetActive(false);
            Button[1].SetActive(false);
            Button[2].SetActive(false);
            Button[3].SetActive(true);
            Button[4].SetActive(true);
            Button[5].SetActive(true);
        }
        else
        {
            Button[3].SetActive(false);
            Button[4].SetActive(false);
            Button[5].SetActive(false);
            Button[0].SetActive(true);
            Button[1].SetActive(true);
            Button[2].SetActive(true);
        }
    }
    public void Exit()
    {
        if (isPanelActive == true)
        {
            characterPanel.SetActive(false);
            isPanelActive = false;
            Time.timeScale = 1;
        }
    }
}
