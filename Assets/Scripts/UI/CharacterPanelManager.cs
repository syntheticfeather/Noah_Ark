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
                // �л�Panel�Ļ״̬����c�������            
                characterPanel.SetActive(true);
                isPanelActive = true;
                Time.timeScale = 0;
            }
            else
            {
                // ��C����
                characterPanel.SetActive(false);
                isPanelActive = false;
                Time.timeScale = 1;
            }
        }
        
    }
}