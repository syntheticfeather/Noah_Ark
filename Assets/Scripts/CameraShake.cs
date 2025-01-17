using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;
    public float shakeDuration = 1f; // �𶯳���ʱ�䣨���������𶯸�������
    public float shakeMagnitude = 0.05f; // ��ǿ�ȣ���С�����𶯸���΢��
    public float smoothness = 2f; // ƽ���ȣ����������𶯸���ͣ�


    private float currentShakeDuration = 0f; // ��ǰ��ʣ��ʱ��

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        if (currentShakeDuration > 0)
        {
            Debug.Log("��ʼ��");
            // ���ƫ������ͷλ��
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;
            Vector3 targetPosition = transform.position + new Vector3(x, y, 0);

            // ƽ�����ɵ�Ŀ��λ��
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, smoothness * Time.deltaTime);

            // ������ʱ��
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