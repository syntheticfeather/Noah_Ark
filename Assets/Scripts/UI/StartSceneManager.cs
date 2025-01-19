using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        Application.Quit();
    }
}
