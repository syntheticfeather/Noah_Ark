using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : MonoBehaviour
{
    public GameObject panel; // 引用面板对象
    public Button openButton; // 引用打开面板的按钮
    public Button exitButton; // 引用退出面板的按钮
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        
        openButton.onClick.AddListener(OpenPanel);
        exitButton.onClick.AddListener(ClosePanel);

        // 确保面板初始状态为关闭
        panel.SetActive(false);
    }

    public void OpenPanel()
    {
        // 打开面板
        panel.SetActive(true);
    }

    public void ClosePanel()
    {
        // 关闭面板
        panel.SetActive(false);
    }
}