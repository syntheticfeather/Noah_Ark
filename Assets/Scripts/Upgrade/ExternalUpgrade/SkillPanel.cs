using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : MonoBehaviour
{
    public GameObject panel; // ����������
    public Button openButton; // ���ô����İ�ť
    public Button exitButton; // �����˳����İ�ť
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        
        openButton.onClick.AddListener(OpenPanel);
        exitButton.onClick.AddListener(ClosePanel);

        // ȷ������ʼ״̬Ϊ�ر�
        panel.SetActive(false);
    }

    public void OpenPanel()
    {
        // �����
        panel.SetActive(true);
    }

    public void ClosePanel()
    {
        // �ر����
        panel.SetActive(false);
    }
}