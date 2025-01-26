using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanelController : MonoBehaviour
{
    public GameObject characterPanel;
    public bool isPanelActive = false;
    public List<GameObject> list = new List<GameObject>();// �����������
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
    // �����ť���������������
    public void OnClickButton(Button button)
    {
        // ��ť�������transform
        Transform parentTransform = button.transform.parent;
        // �ð�ť��Ӧ��Panel��transform
        Transform panel;

        // �Ƚ��������������
        foreach (GameObject gameObject in list)
        {
            gameObject.SetActive(false);
        }
        if (parentTransform != null)
        {
            // ��ȡ����ť��Ӧ��Panel                     
            panel = parentTransform.Find("Panel");
            if (panel != null)
            {
                // �����ض����
                panel.gameObject.SetActive(true);
            }
            
        }                
    }
    
}
