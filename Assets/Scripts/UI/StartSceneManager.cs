using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class StartSceneManager : MonoBehaviour
{
    //��Ϸ��ʼ��ť���
    public void OnStartGame()
    {
        SceneManager.LoadScene("MainScreen");
    }
    //�˳���Ϸ��ť���
    public void OnExitGame()
    {
        PlayerPrefs.SetInt("CrystalCount", ResourceManager.instance.Resource[3]);
        PlayerPrefs.Save();
        Application.Quit();
    }
}
