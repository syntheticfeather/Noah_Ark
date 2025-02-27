using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Father : MonoBehaviour
{
    // ��Χ�˺�

    public float ChangeRate; // ����
    public float explosionRadius = 5f; // ��ը��Χ
    public float ATK;//�����˺�
    public float Speed;// �ƶ��ٶ�
    public float ATKSpeed; // �����ٶ�
    public float LifeTime;
    public float LifeTimeCounter;
    public Vector3 Direction;
    public LayerMask damageLayers; // �����ܵ��˺���ͼ��
    public ParticleSystem ParticleSystem;
    public ParticleSystem BloodSystem;
    // Start is called before the first frame update
    private void Awake()
    {
        Direction = transform.forward;
    }
    public void PlayExplosionEffect()
    {

        if (ParticleSystem != null)
            Instantiate(ParticleSystem, transform.position, Quaternion.identity);
        // ��Ч�ݶ�
    }
}
