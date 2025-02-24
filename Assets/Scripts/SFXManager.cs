using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    private void Awake()
    {
        instance = this;
    }

    public AudioSource[] soundEffects;
    public AudioSource[] BossSound;
    public int Bosssoundindex = -1;

    public void PlaysfxPitch(AudioSource[] SFXList, int index) 
    {
        Debug.Log("playsfx函数调用");
        PlaySFX(SFXList, index);
    }

    public void PlaySFX(AudioSource[] SFXList, int index)
    {
        Debug.Log("SFX函数调用");
        if (BossSound == SFXList)
        {
            Debug.Log("播放音乐");

            if (Bosssoundindex != index)
            {
              
                if (Bosssoundindex != -1)
                {
                    SFXList[Bosssoundindex].Stop();
                }
                SFXList[index].Play();
                Bosssoundindex = index; 
            }
        }
        else
        {
            SFXList[index].Stop();
            SFXList[index].Play();
        }
    }
}