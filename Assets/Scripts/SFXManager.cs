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
        BKCounter = BKTime;
    }
    public void Update()
    {
        BKCounter -= Time.deltaTime;
        if (BKCounter <= 0)
        {
            PlayerBK();
            BKCounter = BKTime;
        }
    }
    public AudioSource[] soundEffects;// 0 Ϊ��������
    public AudioSource[] BossSound;
    public int Bosssoundindex = -1;
    public int BKindex = 0;

    public float BKTime;
    private float BKCounter;

    public void PlayerBK()
    {
        PlaySFX(soundEffects, BKindex);
    }

    public void PlaysfxPitch(AudioSource[] SFXList, int index) 
    {
        Debug.Log("playsfx��������");
        PlaySFX(SFXList, index);
    }

    public void PlaySFX(AudioSource[] SFXList, int index)
    {
        Debug.Log("SFX��������");
        if (BossSound == SFXList)
        {
            Debug.Log("��������");

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