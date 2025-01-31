using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanelManager : MonoBehaviour
{
    public static PausePanelManager instance{ get; private set; }
    public bool GameIsPaused = false;
    public bool IsSetting = false;
    public bool GameIsDead = false;
    public GameObject settingPanel; // ���ý�������
    public GameObject deathPanel; //������������
    public GameObject pauseMenuUI; // ��ͣ�˵���UI���

    private void Awake()
    {
        instance = this;
    }
    // ���·�������Ƿ�����ͣ����ESC��
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && IsSetting == false && GameIsDead == false)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    // �ָ���Ϸ
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        GameIsPaused = false;
        
    }

    // ��ͣ��Ϸ
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
        
    }

    // �������˵�
    public void LoadMenu()
    {
        Time.timeScale = 1f; // ȷ��ʱ����������
        SceneManager.LoadScene("StartScene"); // �������˵�����
    }

    // �������
    public void Setting()
    {
        settingPanel.SetActive(true);
        IsSetting = true;
    }
    // �˳���Ϸ
    public void QuitGame()
    {
        Application.Quit(); // �˳�Ӧ��
    }
}
