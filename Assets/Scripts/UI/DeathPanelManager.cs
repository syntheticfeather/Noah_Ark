using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathPanelManager : MonoBehaviour
{
    // 载入主菜单
    public void LoadMenu()
    {
        Time.timeScale = 1f; // 确保时间正常流动
        SceneManager.LoadScene("StartScene"); // 加载主菜单场景
    }
    // 退出游戏
    public void QuitGame()
    {
        Application.Quit(); // 退出应用
    }
}
