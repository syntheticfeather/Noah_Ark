using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathPanelManager : MonoBehaviour
{
    // �������˵�
    public void LoadMenu()
    {
        Time.timeScale = 1f; // ȷ��ʱ����������
        SceneManager.LoadScene("StartScene"); // �������˵�����
    }
    // �˳���Ϸ
    public void QuitGame()
    {
        Application.Quit(); // �˳�Ӧ��
    }
}
