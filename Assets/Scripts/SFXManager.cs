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

    public void PlaySFX(int sfxT)
    {
        soundEffects[sfxT].Stop();
        soundEffects[sfxT].Play();
    }

    public void PlaysfxPitch(int sfxToplay)
    {
        soundEffects[sfxToplay].pitch = Random.Range(.8f, 1.2f);
        PlaySFX(sfxToplay);
    }
}