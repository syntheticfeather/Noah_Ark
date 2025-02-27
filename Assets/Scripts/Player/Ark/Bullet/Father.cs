using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Father : MonoBehaviour
{
    // 范围伤害

    public float ChangeRate; // 动画
    public float explosionRadius = 5f; // 爆炸范围
    public int ATK;//攻击伤害
    public float Speed;// 移动速度
    public float ATKSpeed; // 攻击速度
    public float LifeTime;
    public float LifeTimeCounter;
    public Vector3 Direction;
    public LayerMask damageLayers; // 可以受到伤害的图层
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
        // 音效暂定
    }
}
