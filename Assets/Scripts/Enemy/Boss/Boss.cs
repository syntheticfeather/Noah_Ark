using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public static Boss Instance;
    private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        StrikeCounter = StrikeBetTime;
        SweepCounter = SweepBetTime;
        Player = PlayerHealthController.instance.gameObject;
        Instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        StrikeCounter -= Time.deltaTime;
        SweepCounter -= Time.deltaTime;
        DropCounter -= Time.deltaTime;
        LaserCounter -= Time.deltaTime;
        if (StrikeCounter < 0)
        {
            StartCoroutine(DelayStrike());
        }
        if (SweepCounter < 0)
        {
            StartCoroutine(DelaySweep());
        }
        if (DropCounter < 0)
        {
            StartCoroutine(DelayDrop());
        }
        if (LaserCounter < 0)
        {
            StartCoroutine(DelayLaser());
        }
    }
    public int StrikeDelayTime;
    public int StrikeBetTime;
    public float StrikeCounter;
    public GameObject StrikeArea;
    public GameObject CurStrikeArea;
    public GameObject StrikeATK;
    // Í»´Ì
    IEnumerator DelayStrike()
    {   
        StrikeCounter = StrikeBetTime + StrikeDelayTime;
        CurStrikeArea = Instantiate(StrikeArea, Player.transform.position, Quaternion.identity);
        CurStrikeArea.SetActive(true);
        yield return new WaitForSeconds(StrikeDelayTime);
        Strike();               
    }
    public void Strike()
    {
        Instantiate(StrikeATK, CurStrikeArea.transform.position + new Vector3(0,-.8f,0), Quaternion.identity).SetActive(true);
        Destroy(CurStrikeArea);
    }

    public int SweepDelayTime;
    public int SweepBetTime;
    public float SweepCounter;
    public GameObject SweepArea;
    public GameObject CurSweepArea;
    public GameObject SweepATK;
    // Í»´Ì
    IEnumerator DelaySweep()
    {
        SweepCounter = SweepBetTime + SweepDelayTime;
        CurSweepArea = Instantiate(SweepArea, Player.transform.position, Quaternion.identity);
        CurSweepArea.SetActive(true);
        yield return new WaitForSeconds(SweepDelayTime);
        Sweep();
    }
    public void Sweep()
    {
        Instantiate(SweepATK, CurSweepArea.transform.position + new Vector3(0, 9f, 0), Quaternion.identity).SetActive(true);
        Destroy(CurSweepArea);
    }
    public int DropDelayTime;
    public int DropBetTime;
    public float DropCounter;
    public GameObject DropArea;
    public GameObject CurDropArea;
    public GameObject DropATK;
    public Vector2 Direction;
    public float Angle;
    // Í»´Ì
    IEnumerator DelayDrop()
    {               
        Direction = Player.transform.position - transform.position;
        Angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
        DropCounter = DropBetTime + DropDelayTime;
        CurDropArea = Instantiate(DropArea, transform.position + new Vector3(DropArea.transform.lossyScale.y / 2 * Mathf.Cos(Mathf.Atan2(Direction.y, Direction.x)) ,
            DropArea.transform.lossyScale.y / 2 * Mathf.Sin(Mathf.Atan2(Direction.y, Direction.x)), 0), Quaternion.Euler(0, 0, Angle - 90));
        CurDropArea.SetActive(true);
        yield return new WaitForSeconds(DropDelayTime);
        Drop();
    }
    public void Drop()
    {        
        Instantiate(DropATK, CurDropArea.transform.position, Quaternion.Euler(0, 0, Angle - 90)).SetActive(true);
        Destroy(CurDropArea);
    }

    public int LaserDelayTime;
    public int LaserBetTime;
    public float LaserCounter;
    public GameObject LaserArea;
    public GameObject CurLaserArea;
    public GameObject LaserATK;
    // Í»´Ì
    IEnumerator DelayLaser()
    {        
        Direction = Player.transform.position - transform.position;
        Angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
        LaserCounter = LaserBetTime + LaserDelayTime;
        CurLaserArea = Instantiate(LaserArea, Player.transform.position, Quaternion.identity);
        CurLaserArea.SetActive(true);
        yield return new WaitForSeconds(LaserDelayTime);
        Laser();
    }   
    public void Laser()
    {
        GameObject Laser = Instantiate(LaserATK, transform.position, Quaternion.Euler(0, 0, Angle));
        Laser.SetActive(true);
        Laser.GetComponent<Laser>().Line1.SetPosition(1, CurLaserArea.transform.position);
        Laser.GetComponent<Laser>().Line2.SetPosition(1, CurLaserArea.transform.position);
        Destroy(CurLaserArea);
    }
}
