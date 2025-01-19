using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        Application.Quit();
    }
}
