using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanelManager : MonoBehaviour
{
    public GameObject settingPanel; //���ý���
    // public List<AudioSource> audioSources1 = new List<AudioSource>(); //��Ϸ��Ƶ�ļ�list
    // public List<AudioSource> audioSources2 = new List<AudioSource>(); //��Ϸ��Ч�ļ�list 
    public Slider slider1; //������С������
    public Slider slider2; //��Ч��С������
    public Text text1; //������С�ı���ʾ
    public Text text2; //��Ч��С�ı���ʾ

    // Update is called once per frame
    void Update()
    {
        SoundControl();
        // esc�˳�����
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Exit();
        }
    }

    //���������ĺ���
    public void SoundControl()
    {
        //����������С
        //foreach (AudioSource audioSource in audioSources1)
        //{
        //    audioSource.volume = slider1.value;
        //}
        //������Ч��С
        //foreach (AudioSource audioSource in audioSources2)
        //{
        //    audioSource.volume = slider2.value;
        //}
        //������С�ı���ʾ
        text1.text = ((int)(slider1.value * 100)).ToString();
        //��Ч��С�ı���ʾ
        text2.text = ((int)(slider2.value * 100)).ToString();
    }

    //�˳����ý���
    public void Exit()
    {
        settingPanel.SetActive(false);
        PausePanelManager.instance.IsSetting = false;
    }
}
