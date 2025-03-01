using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumnManager : MonoBehaviour
{
    [Header("组件绑定")]
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Text volumeText; // 可选：显示百分比
    [SerializeField] private AudioSource audioSource; // 直接控制音频源
    [SerializeField] private AudioMixer audioMixer; // 若使用 Mixer
    [SerializeField] private string volumeParameter = "MasterVolume"; // Mixer 参数名

    [Header("配置")]
    [SerializeField] private bool useLogScale = true; // Mixer 音量按对数缩放

    private void Start()
    {
        // 初始化滑块值（加载保存的音量）
        float savedVolume = PlayerPrefs.GetFloat("Volume", 0.75f);
        volumeSlider.value = savedVolume;

        // 绑定滑块事件
        volumeSlider.onValueChanged.AddListener(SetVolume);

        // 初始更新一次显示
        SetVolume(savedVolume);
    }

    public void SetVolume(float value)
    {
        // 直接控制 AudioSource
        if (audioSource != null)
            audioSource.volume = value;

        // 使用 AudioMixer 控制全局音量
        if (audioMixer != null)
        {
            // 将线性值（0~1）转换为对数（-80dB~0dB）
            float dB = useLogScale ? Mathf.Log10(value) * 20 : value;
            audioMixer.SetFloat(volumeParameter, dB);
        }

        // 更新音量显示（如 "50%"）
        if (volumeText != null)
            volumeText.text = Mathf.RoundToInt(value * 100) + "%";

        // 保存音量设置
        PlayerPrefs.SetFloat("Volume", value);
        PlayerPrefs.Save();
    }
}
