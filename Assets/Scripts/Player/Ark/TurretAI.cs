using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurretAI : MonoBehaviour
{
    // ��̨��̽�ⷶΧ
    public float range = 15f;
    // ���˵ı�ǩ������ʶ����˶���
    public string targetTag = "Enemy";
    public LayerMask layerMask;
    // ������λ�ã����ڿ�λ�ã�
    public Transform firePoint;
    // �ӵ�Ԥ�Ƽ�������ʵ�����ӵ�
    public GameObject bulletPrefab;
    // ����Ƶ�ʣ���/�Σ�
    public float fireRate = 1f;
    // ��ʱ�������ڿ��ƹ������
    private float fireCountdown = 0f;

    public GameObject Animal;// ��̬��ȡ��ͬ����
      
    void Update()
    {
        if( fireCountdown >= 0)
        {
            fireCountdown -= Time.deltaTime;
        }
        DetectEnemies();
    }
    //��Ⲣ��׼����
    private void DetectEnemies()
    {          
        // ʹ��OverlapCircle����������̽�ⷶΧ�ڵ���ײ��
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position,range,layerMask);
        Transform bestTarget = null; // ���Ŀ��
        float closestDistanceSqr = Mathf.Infinity; // ��������ƽ��,������Ϊ�����
        Vector2 currentPosition = transform.position; // ��ǰ��̨��λ��
        // �������м�⵽����ײ��
        foreach (var hitCollider in hitColliders)
        {           
            
            var possibleTarget = hitCollider.transform;
            // �������ײ�����ڵ���
            if (hitCollider.CompareTag(targetTag) || hitCollider.CompareTag("Boss")) 
            {               
                // ��������ƽ���Ա��⿪������
                float distanceToTargetSqr = ((Vector2)possibleTarget.position - currentPosition).sqrMagnitude;
                // �ҵ������Ŀ��
                if (distanceToTargetSqr < closestDistanceSqr)
                {
                    closestDistanceSqr = distanceToTargetSqr;
                    bestTarget = possibleTarget;
                }
            }
        }
        // ����ҵ���Ŀ�겢�Ҽ�ʱ���Ѿ����㣬������Ŀ�겢����
        if (bestTarget != null)
        {
            LockOnTarget(bestTarget); // ����Ŀ��
            if (fireCountdown <= 0)
            {
                FireAtTarget(bestTarget); // ���𹥻�
            }
        }          
        
    }
    // ����Ŀ�꣬������̨����Ŀ��
    void LockOnTarget(Transform target)
    {
        // �������̨��Ŀ��ķ���
        Vector2 dir = target.position - transform.position;
        // ������Ҫ��ת�ĽǶ�
        float angle = Mathf.Atan2(dir.y, dir.x)*Mathf.Rad2Deg;
        // ƽ������ת��̨��ʹ���׼Ŀ��
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f,0f,angle - 90f), Time.deltaTime * 10f);
    }
    // ��Ŀ�귢���ӵ�
    void FireAtTarget(Transform target)
    {
        // �ڷ����λ��ʵ�����ӵ�Ԥ�Ƽ�       
        GameObject BombToSpawn = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
        BombToSpawn.SetActive(true);
        // ���ü�ʱ�����ȴ��´ι���
        fireCountdown = 1f / fireRate;
      
    }
    // ��ѡ�����ӻ���ⷶΧ
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}


