using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanelController : MonoBehaviour
{
    public GameObject characterPanel;
    public bool isPanelActive = false;
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
}