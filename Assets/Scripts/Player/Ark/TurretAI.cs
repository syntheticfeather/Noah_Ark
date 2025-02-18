using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurretAI : MonoBehaviour
{
    // 炮台的探测范围
    public float range = 15f;
    // 敌人的标签，用于识别敌人对象
    public string targetTag = "Enemy";
    public LayerMask layerMask;
    // 发射点的位置（即炮口位置）
    public Transform firePoint;
    // 子弹预制件，用于实例化子弹
    public GameObject bulletPrefab;
    // 攻击频率（秒/次）
    public float fireRate = 1f;
    // 计时器，用于控制攻击间隔
    private float fireCountdown = 0f;

    public GameObject Animal;// 动态获取不同动物
      
    void Update()
    {
        if( fireCountdown >= 0)
        {
            fireCountdown -= Time.deltaTime;
        }
        DetectEnemies();
    }
    //检测并瞄准敌人
    private void DetectEnemies()
    {          
        // 使用OverlapCircle查找所有在探测范围内的碰撞体
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position,range,layerMask);
        Transform bestTarget = null; // 最佳目标
        float closestDistanceSqr = Mathf.Infinity; // 最近距离的平方,先设置为无穷大
        Vector2 currentPosition = transform.position; // 当前炮台的位置
        // 遍历所有检测到的碰撞体
        foreach (var hitCollider in hitColliders)
        {           
            
            var possibleTarget = hitCollider.transform;
            // 如果该碰撞体属于敌人
            if (hitCollider.CompareTag(targetTag) || hitCollider.CompareTag("Boss")) 
            {               
                // 计算距离的平方以避免开方运算
                float distanceToTargetSqr = ((Vector2)possibleTarget.position - currentPosition).sqrMagnitude;
                // 找到最近的目标
                if (distanceToTargetSqr < closestDistanceSqr)
                {
                    closestDistanceSqr = distanceToTargetSqr;
                    bestTarget = possibleTarget;
                }
            }
        }
        // 如果找到了目标并且计时器已经归零，则锁定目标并攻击
        if (bestTarget != null)
        {
            LockOnTarget(bestTarget); // 锁定目标
            if (fireCountdown <= 0)
            {
                FireAtTarget(bestTarget); // 开火攻击
            }
        }          
        
    }
    // 锁定目标，调整炮台朝向目标
    void LockOnTarget(Transform target)
    {
        // 计算从炮台到目标的方向
        Vector2 dir = target.position - transform.position;
        // 计算需要旋转的角度
        float angle = Mathf.Atan2(dir.y, dir.x)*Mathf.Rad2Deg;
        // 平滑地旋转炮台，使其对准目标
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f,0f,angle - 90f), Time.deltaTime * 10f);
    }
    // 向目标发射子弹
    void FireAtTarget(Transform target)
    {
        // 在发射点位置实例化子弹预制件       
        GameObject BombToSpawn = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
        BombToSpawn.SetActive(true);
        // 重置计时器，等待下次攻击
        fireCountdown = 1f / fireRate;
      
    }
    // 可选：可视化检测范围
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}


