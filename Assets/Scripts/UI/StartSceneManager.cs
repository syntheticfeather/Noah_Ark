using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class StartSceneManager : MonoBehaviour
{
    //游戏开始按钮点击
    public void OnStartGame()
    {
        SceneManager.LoadScene("MainScreen");
    }
    //退出游戏按钮点击
    public void OnExitGame()
    {
        PlayerPrefs.SetInt("CrystalCount", ResourceManager.instance.Resource[3]);
        PlayerPrefs.Save();
        Application.Quit();
    }
}
