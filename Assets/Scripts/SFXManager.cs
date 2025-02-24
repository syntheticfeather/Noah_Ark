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
    public int Bosssoundindex = -1;// ���Լ�¼��ǰ���ŵ�boss��Ч
    

    public void PlaysfxPitch(AudioSource[] SFXList, int index) // ѡ�б���
    {    
        PlaySFX(SFXList,index);
    }
    public void PlaySFX(AudioSource[] SFXList,int index) // ����������һ�β��ţ��쿪ʼ�µĲ���
    {
        if (BossSound == SFXList)// bossΪѭ����Ч
        {            
            if (Bosssoundindex != index)
            { 
                //boss��ЧΪֹͣ��һ����������һ��
                if (Bosssoundindex != -1)
                {
                    Debug.Log("ֹͣ" + Bosssoundindex + "��boss��Ч");
                    SFXList[Bosssoundindex].Stop(); // ԭ��ֹͣ
                }
                SFXList[index].Play(); // �����²���
                Debug.Log("����" + index + "��boss��Ч");
                Bosssoundindex = index; // ��������
            }
        }
        else// ������ѭ����Чֱ����ͣ������һ��
        {
            soundEffects[index].Stop();
            soundEffects[index].Play();
        }
    }
}