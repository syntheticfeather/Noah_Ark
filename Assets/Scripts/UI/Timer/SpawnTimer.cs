using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;

public class SpawnTimer : MonoBehaviour
{
    public static SpawnTimer instance;
    public Slider Slider;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {

        Slider.maxValue = EnemySpawnController.instance.waves[EnemySpawnController.instance.currentWaveIndex].waveInterval;
    }

    // Update is called once per frame
    void Update()
    {
        Slider.value += Time.deltaTime;
        if (Slider.value >= Slider.maxValue)
        {
            Slider.value = 0;
        }
    }

    public void UpdateMaxSlider()
    {
        Slider.maxValue = EnemySpawnController.instance.Bosswave.waveInterval;
    }
}
