using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vortex : MonoBehaviour
{
    public Transform PlayerCenter; // 漩涡中心
    public float pullForce = 10f; // 拉力大小
    public float vortexRadius = 5f; // 漩涡作用范围
    public float rotationSpeed = 100f; // 旋转速度
    public float Angle;

    private Rigidbody2D playerRigidbody; // 玩家的 Rigidbody

    void Start()
    {
        PlayerCenter = PlayerHealthController.instance.transform;
        // 获取玩家的 Rigidbody
        playerRigidbody = PlayerHealthController.instance.gameObject.GetComponent<Rigidbody2D>();
        Destroy(gameObject, 8);
    }

    void FixedUpdate()
    {
        Angle -= Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, Angle);
        // 计算玩家与漩涡中心的距离
        Vector3 direction = PlayerCenter.position - transform.position;
        float distance = direction.magnitude;

        // 如果玩家在漩涡范围内
        if (distance <= vortexRadius)
        {
            // 施加向心力
            Vector3 pullDirection = -direction.normalized;
            playerRigidbody.AddForce(pullDirection * pullForce);

            // 施加旋转力
            Vector3 rotationForce = Vector3.Cross(pullDirection, Vector3.up) * rotationSpeed;
            playerRigidbody.AddForce(rotationForce);
        }
    }

    void OnDrawGizmos()
    {
        // 在场景中绘制漩涡范围
        if (transform != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, vortexRadius);
        }
    }
}