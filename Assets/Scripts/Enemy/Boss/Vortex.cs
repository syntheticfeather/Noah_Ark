using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vortex : MonoBehaviour
{
    public Transform PlayerCenter; // ��������
    public float pullForce = 10f; // ������С
    public float vortexRadius = 5f; // �������÷�Χ
    public float rotationSpeed = 100f; // ��ת�ٶ�
    public float Angle;

    private Rigidbody2D playerRigidbody; // ��ҵ� Rigidbody

    void Start()
    {
        PlayerCenter = PlayerHealthController.instance.transform;
        // ��ȡ��ҵ� Rigidbody
        playerRigidbody = PlayerHealthController.instance.gameObject.GetComponent<Rigidbody2D>();
        Destroy(gameObject, 8);
    }

    void FixedUpdate()
    {
        Angle -= Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, Angle);
        // ����������������ĵľ���
        Vector3 direction = PlayerCenter.position - transform.position;
        float distance = direction.magnitude;

        // �����������з�Χ��
        if (distance <= vortexRadius)
        {
            // ʩ��������
            Vector3 pullDirection = -direction.normalized;
            playerRigidbody.AddForce(pullDirection * pullForce);

            // ʩ����ת��
            Vector3 rotationForce = Vector3.Cross(pullDirection, Vector3.up) * rotationSpeed;
            playerRigidbody.AddForce(rotationForce);
        }
    }

    void OnDrawGizmos()
    {
        // �ڳ����л������з�Χ
        if (transform != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, vortexRadius);
        }
    }
}