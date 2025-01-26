using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;
    public float shakeDuration = 1f; // 震动持续时间（增加以让震动更缓慢）
    public float shakeMagnitude = 0.05f; // 震动强度（减小以让震动更轻微）
    public float smoothness = 2f; // 平滑度（增加以让震动更柔和）


    private float currentShakeDuration = 0f; // 当前震动剩余时间

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {

    }

    void Update()
    {
        if (currentShakeDuration > 0)
        {            
            // 随机偏移摄像头位置
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;
            Vector3 targetPosition = transform.position + new Vector3(x, y, 0);

            // 平滑过渡到目标位置
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, smoothness * Time.deltaTime);

            // 减少震动时间
            currentShakeDuration -= Time.deltaTime;
        }
        else
        {
             CameraFollow.instance.enabled = true;
        }
    }

    public void Shake()
    {
        currentShakeDuration = shakeDuration;
        CameraFollow.instance.enabled = false;
    }
}