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
    public int Bosssoundindex = -1;// 用以记录当前播放的boss音效
    

    public void PlaysfxPitch(AudioSource[] SFXList, int index) // 选列表播放
    {    
        PlaySFX(SFXList,index);
    }
    public void PlaySFX(AudioSource[] SFXList,int index) // 立即截至上一次播放，快开始新的播放
    {
        if (BossSound == SFXList)// boss为循环音效
        {            
            if (Bosssoundindex != index)
            { 
                //boss音效为停止上一个，播放下一个
                if (Bosssoundindex != -1)
                {
                    Debug.Log("停止" + Bosssoundindex + "个boss音效");
                    SFXList[Bosssoundindex].Stop(); // 原来停止
                }
                SFXList[index].Play(); // 现在新播放
                Debug.Log("播放" + index + "个boss音效");
                Bosssoundindex = index; // 更新索引
            }
        }
        else// 其他不循环音效直接暂停播放下一轮
        {
            soundEffects[index].Stop();
            soundEffects[index].Play();
        }
    }
}