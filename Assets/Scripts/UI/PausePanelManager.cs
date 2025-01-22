using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanelManager : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject settingPanel; // ���ý�������
    public GameObject pauseMenuUI; // ��ͣ�˵���UI���

    // ���·�������Ƿ�����ͣ����ESC��
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
    }
    // �˳���Ϸ
    public void QuitGame()
    {
        Application.Quit(); // �˳�Ӧ��
    }
}
