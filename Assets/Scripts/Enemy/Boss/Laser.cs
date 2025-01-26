using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float RemainTime;
    public float KnockBet;
    public float KnockBetCounter;
    public int ATK;
    public float Speed;
    public float Radius;
    public LineRenderer Line1;
    public LineRenderer Line2;
    public GameObject EndPar;
    private Transform Player;
    private Collision2D Collision1;
    private Vector3 Target;
    // Start is called before the first frame update
    void Start()
    {
        Player = PlayerHealthController.instance.transform;        
        KnockBetCounter = KnockBet;
        Target = Boss.Instance.CurLaserArea.transform.position;
        Destroy(gameObject, RemainTime);
    }

    // Update is called once per frame
    void Update()
    {
        KnockBetCounter -= Time.deltaTime;
        Vector3 Direction = Target- transform.position;
        Vector3 Direction1 = Player.transform.position - Target;
        float Angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, Angle);
        Target += Direction1 * Speed * Time.deltaTime; 
        float Distance = Vector3.Distance(Target, Player.transform.position);
        if (Distance <= Radius && KnockBetCounter <= 0)
        {
            KnockBetCounter = KnockBet;
            PlayerHealthController.instance.TakeDamage(ATK);
        }
        EndPar.gameObject.transform.position = Target;
        Line1.SetPosition(1, Target);
        Line2.SetPosition(1, Target);        
    }



}
