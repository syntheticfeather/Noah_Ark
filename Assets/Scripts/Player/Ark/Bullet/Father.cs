using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Father : MonoBehaviour
{
    // ∑∂Œß…À∫¶

    public float ChangeRate; // ∂Øª≠
    public float explosionRadius = 5f; // ±¨’®∑∂Œß
    public int ATK;
    public float LifeTime;
    public float LifeTimeCounter;
    public Vector3 Direction;
    public LayerMask damageLayers; // ø…“‘ ‹µΩ…À∫¶µƒÕº≤„
    public ParticleSystem ParticleSystem;
    public ParticleSystem BloodSystem;
    public float Speed;
    public float ATKSpeed;
    // Start is called before the first frame update
    private void Awake()
    {
        Direction = transform.forward;
    }
    public void PlayExplosionEffect()
    {

        if (ParticleSystem != null)
            Instantiate(ParticleSystem, transform.position, Quaternion.identity);
        // “Ù–ß‘›∂®
    }
}
