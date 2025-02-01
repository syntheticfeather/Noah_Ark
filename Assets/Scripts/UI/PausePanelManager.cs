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
    public GameObject settingPanel; // 设置界面的面板
    public GameObject deathPanel; //死亡界面的面板
    public GameObject pauseMenuUI; // 暂停菜单的UI面板

    private void Awake()
    {
        instance = this;
    }
    // 更新方法检查是否按下暂停键（ESC）
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

    // 恢复游戏
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        GameIsPaused = false;
        
    }

    // 暂停游戏
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
        
    }

    // 载入主菜单
    public void LoadMenu()
    {
        Time.timeScale = 1f; // 确保时间正常流动
        SceneManager.LoadScene("StartScene"); // 加载主菜单场景
    }

    // 设置面板
    public void Setting()
    {
        settingPanel.SetActive(true);
        IsSetting = true;
    }
    // 退出游戏
    public void QuitGame()
    {
        Application.Quit(); // 退出应用
    }
}
