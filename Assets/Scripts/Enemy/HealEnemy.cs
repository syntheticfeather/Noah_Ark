using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HealEnemy : MonoBehaviour
{
    public int HealAmount;

    public float Speed = 1;
    public float CircleSpeed;
    public float Radius;
    public float HealRadius;

    public float KnockTime = 5;
    private float KnockCounter = 0;

    private float Angle;
    private float currentAngle;
    private float Direc;
    private bool toOrbit = true; // �Ƿ����ں���
    private bool isCircling = false; // �Ƿ�������Ȧ

    private Vector2 Direction;

    private Transform PlayerTransform;
    public Rigidbody2D rb;
    public LayerMask Enemy;
    // Start is called before the first frame update
    void Start()
    {
        Direc = Random.value;
        PlayerTransform = PlayerHealthController.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        KnockCounter -= Time.deltaTime;
        Direction = PlayerTransform.position - transform.position;
        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        if (KnockCounter<0)
        {
            KnockCounter = KnockTime;
            Heal();
        }
        Movement();
    }

    private void Movement()
    {
        if (toOrbit)
        {
            ToOrbit();
        }
        else if (isCircling)
        {
            CirclePlayer();
        }
    }
    void CirclePlayer()
    {
         Angle -= CircleSpeed * Time.deltaTime;
        // ������λ��
        float x = PlayerTransform.position.x + Radius * Mathf.Cos(Angle);
        float y = PlayerTransform.position.y + Radius * Mathf.Sin(Angle);
        // ���µ���λ��        
        transform.position = Vector3.Lerp(transform.position, new Vector3(x, y, transform.position.z), .1f);      
    }
    public void ToOrbit()
    {

        // ����Ƿ�ﵽ���˾���
        float distanceToPlayer = Vector2.Distance(transform.position, PlayerTransform.position);
        if (distanceToPlayer <= Radius)
        {
            toOrbit = false;
            isCircling = true;            
            // ��ʼ��Բ���˶��ĽǶ�
            Vector2 direction = transform.position - PlayerTransform.position;
            Angle = Mathf.Atan2(direction.y, direction.x);
        }
        else
        {
            transform.position += (Vector3)Direction * Speed * Time.deltaTime;

        }
    }
    public void Heal()
    {
        
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, HealRadius, Enemy);
        Debug.Log(hitColliders.Length);
        foreach (Collider2D hitCollider in hitColliders)
        {            
            EnemyHealthController health = hitCollider.GetComponent<EnemyHealthController>();
            if (health != null)
            {
                health.TakeHeal(HealAmount);
            }
        }
    }
}
