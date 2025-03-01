using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSequencePlayer : MonoBehaviour
{
    public static AudioSequencePlayer instance;
    public Slider slider;
    public Slider slider2;
    void Awake()
    {
        instance = this;
        slider.value = PlayerPrefs.GetFloat("BKVolumn", 1);
        slider2.value = PlayerPrefs.GetFloat("BKVolumn2", 1);
    }
    [Header("音频配置")]
    public AudioSource audioSource;
    public List<AudioClip> clipList = new List<AudioClip>();
    public bool shufflePlay = false; // 是否随机播放

    private Queue<AudioClip> clipQueue = new Queue<AudioClip>();
    private Coroutine playRoutine;
    private void Update()
    {
        audioSource.volume = slider.value;
    }
    void Start()
    {
   
        InitializeQueue();
    }

    void InitializeQueue()
    {
        clipQueue.Clear();

        // 创建临时列表处理随机播放
        List<AudioClip> tempList = new List<AudioClip>(clipList);
        if (shufflePlay)
        {
            // Fisher-Yates洗牌算法
            for (int i = tempList.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (tempList[i], tempList[j]) = (tempList[j], tempList[i]);
            }
        }

        foreach (var clip in tempList)
        {
            clipQueue.Enqueue(clip);
        }
    }

    public void StartPlayback()
    {
        if (playRoutine != null) StopCoroutine(playRoutine);
        playRoutine = StartCoroutine(PlaySequence());
    }
    public void stopPlayback()
    {
        audioSource.Stop();
        StopCoroutine(playRoutine);
    }
    IEnumerator PlaySequence()
    {
        while (clipQueue.Count > 0)
        {
            AudioClip currentClip = clipQueue.Dequeue();
            audioSource.clip = currentClip;
            audioSource.Play();

            // 等待音频播放结束 + 0.1秒缓冲
            yield return new WaitForSeconds(currentClip.length + 0.1f);

            // 将已播放的音频重新加入队列实现循环
            clipQueue.Enqueue(currentClip);
        }
    }

    // 外部调用切换下一首
    public void SkipToNext()
    {
        audioSource.Stop();
        StopCoroutine(playRoutine);
        StartPlayback();
    }
}