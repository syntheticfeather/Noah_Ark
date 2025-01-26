using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanelManager : MonoBehaviour
{
    public GameObject settingPanel; //设置界面
    // public List<AudioSource> audioSources1 = new List<AudioSource>(); //游戏音频文件list
    // public List<AudioSource> audioSources2 = new List<AudioSource>(); //游戏音效文件list 
    public Slider slider1; //音量大小进度条
    public Slider slider2; //音效大小进度条
    public Text text1; //音量大小文本显示
    public Text text2; //音效大小文本显示

    // Update is called once per frame
    void Update()
    {
        SoundControl();
        // esc退出界面
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Exit();
        }
    }

    //控制声音的函数
    public void SoundControl()
    {
        //控制音量大小
        //foreach (AudioSource audioSource in audioSources1)
        //{
        //    audioSource.volume = slider1.value;
        //}
        //控制音效大小
        //foreach (AudioSource audioSource in audioSources2)
        //{
        //    audioSource.volume = slider2.value;
        //}
        //音量大小文本显示
        text1.text = ((int)(slider1.value * 100)).ToString();
        //音效大小文本显示
        text2.text = ((int)(slider2.value * 100)).ToString();
    }

    //退出设置界面
    public void Exit()
    {
        settingPanel.SetActive(false);
        PausePanelManager.instance.IsSetting = false;
    }
}
