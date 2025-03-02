using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class StartSceneManager : MonoBehaviour
{
    public static StartSceneManager Instance;
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //游戏开始按钮点击
    public void OnStartGame()
    {
        SceneManager.LoadScene("MainScreen");
    }
    //退出游戏按钮点击
    public void OnExitGame()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }
}
