using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumnManager : MonoBehaviour
{
    [Header("�����")]
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Text volumeText; // ��ѡ����ʾ�ٷֱ�
    [SerializeField] private AudioSource audioSource; // ֱ�ӿ�����ƵԴ
    [SerializeField] private AudioMixer audioMixer; // ��ʹ�� Mixer
    [SerializeField] private string volumeParameter = "MasterVolume"; // Mixer ������

    [Header("����")]
    [SerializeField] private bool useLogScale = true; // Mixer ��������������

    private void Start()
    {
        // ��ʼ������ֵ�����ر����������
        float savedVolume = PlayerPrefs.GetFloat("Volume", 0.75f);
        volumeSlider.value = savedVolume;

        // �󶨻����¼�
        volumeSlider.onValueChanged.AddListener(SetVolume);

        // ��ʼ����һ����ʾ
        SetVolume(savedVolume);
    }

    public void SetVolume(float value)
    {
        // ֱ�ӿ��� AudioSource
        if (audioSource != null)
            audioSource.volume = value;

        // ʹ�� AudioMixer ����ȫ������
        if (audioMixer != null)
        {
            // ������ֵ��0~1��ת��Ϊ������-80dB~0dB��
            float dB = useLogScale ? Mathf.Log10(value) * 20 : value;
            audioMixer.SetFloat(volumeParameter, dB);
        }

        // ����������ʾ���� "50%"��
        if (volumeText != null)
            volumeText.text = Mathf.RoundToInt(value * 100) + "%";

        // ������������
        PlayerPrefs.SetFloat("Volume", value);
        PlayerPrefs.Save();
    }
}
