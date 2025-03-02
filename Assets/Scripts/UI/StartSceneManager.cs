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
    //��Ϸ��ʼ��ť���
    public void OnStartGame()
    {
        SceneManager.LoadScene("MainScreen");
    }
    //�˳���Ϸ��ť���
    public void OnExitGame()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }
}
