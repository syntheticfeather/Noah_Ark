using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }        
    public void Update()
    {
        BossSound[0].volume = AudioSequencePlayer.instance.slider.value;
        soundEffects[0].volume = AudioSequencePlayer.instance.slider2.value;
    }
    public AudioClip[] sound;
    public AudioSource[] soundEffects;
    public AudioClip[] music;
    public AudioSource[] BossSound;
    public int Bosssoundindex = -1;
    public int BKindex = 0;
    public void SaveVolumn()
    {
        PlayerPrefs.SetFloat("BKVolumn", AudioSequencePlayer.instance.slider.value);
        PlayerPrefs.SetFloat("BKVolumn2", AudioSequencePlayer.instance.slider2.value);
        PlayerPrefs.Save();
    }


    public void PlaysfxPitch(AudioSource[] SFXList, int index) 
    {
        //Debug.Log("playsfx函数调用");
        PlaySFX(SFXList, index);
    }

    public void PlaySFX(AudioSource[] SFXList, int index)
    {
        //Debug.Log("SFX函数调用");
        if (BossSound == SFXList)
        {
            //Debug.Log("播放音乐");

            if (Bosssoundindex != index)
            {
              
                if (Bosssoundindex != -1)
                {
                    SFXList[0].Stop();
                }
                SFXList[0].clip = music[index];
                SFXList[0].Play();
                Bosssoundindex = index; 
            }
        }
        else
        {
            SFXList[0].clip = sound[index];
            SFXList[0].Stop();
            SFXList[0].Play();
        }
    }
}