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
    [Header("��Ƶ����")]
    public AudioSource audioSource;
    public List<AudioClip> clipList = new List<AudioClip>();
    public bool shufflePlay = false; // �Ƿ��������

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

        // ������ʱ�б����������
        List<AudioClip> tempList = new List<AudioClip>(clipList);
        if (shufflePlay)
        {
            // Fisher-Yatesϴ���㷨
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

            // �ȴ���Ƶ���Ž��� + 0.1�뻺��
            yield return new WaitForSeconds(currentClip.length + 0.1f);

            // ���Ѳ��ŵ���Ƶ���¼������ʵ��ѭ��
            clipQueue.Enqueue(currentClip);
        }
    }

    // �ⲿ�����л���һ��
    public void SkipToNext()
    {
        audioSource.Stop();
        StopCoroutine(playRoutine);
        StartPlayback();
    }
}