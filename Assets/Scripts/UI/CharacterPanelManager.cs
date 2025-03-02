using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanelController : MonoBehaviour
{
    public GameObject characterPanel;
    public bool isPanelActive = false;
    public List<GameObject> list = new List<GameObject>();// �����������
    public List<GameObject> Button = new List<GameObject>();//�����������ť
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isPanelActive == false)
            {
                // �������а�ť�����伤��
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
            if (gameObject)
            {
                gameObject.SetActive(false);                
            }
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
    //��ҳ
    public void ChangePage()
    {
        //�����һҳ����屻����ʹ򿪵ڶ�ҳ
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
