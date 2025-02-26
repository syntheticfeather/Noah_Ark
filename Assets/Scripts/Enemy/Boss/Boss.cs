using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public static Boss Instance;
    private GameObject Player;
    public EnemyHealthController EnemyHealthController;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }
    // 100 œ¬‘“”ÎÕª¥Ã
    // 80 ø™…®
    // 50 ‰ˆŒ–
    // 40 º§π‚
    void Start()
    {
        StrikeCounter = StrikeBetTime;
        DropCounter = DropBetTime;
        SweepCounter = SweepBetTime;
        LaserCounter = LaserBetTime;
        VortexCounter = VortexBetTime;
        Player = PlayerHealthController.instance.gameObject;

        
    }
    public int soundindex = 0;
    void Update()
    {
        if (EnemyHealthController.CurHealth <= 0)
        {
            Destroy(gameObject);
        }
        StrikeCounter -= Time.deltaTime;
        DropCounter -= Time.deltaTime;
        if (EnemyHealthController.CurHealth < (EnemyHealthController.MaxHealth * 0.8))
        {
            SweepCounter -= Time.deltaTime;
            soundindex = 1;
            SFXManager.instance.PlaySFX(SFXManager.instance.BossSound, 5);
        }
        if (EnemyHealthController.CurHealth < (EnemyHealthController.MaxHealth * 0.5))
        {
            VortexCounter -= Time.deltaTime;
            soundindex = 2;
            SFXManager.instance.PlaySFX(SFXManager.instance.BossSound, 5);
        }
        if (EnemyHealthController.CurHealth < (EnemyHealthController.MaxHealth * 0.4))
        {
            LaserCounter -= Time.deltaTime;
            SFXManager.instance.PlaySFX(SFXManager.instance.BossSound, 5);
        }
        if (EnemyHealthController.CurHealth < (EnemyHealthController.MaxHealth * 0.3))
        {
            soundindex = 3;
        }
        if (EnemyHealthController.CurHealth <= 0)
        {
            soundindex = 4;
            EventController.instance.End();
            Destroy(gameObject);
        }
        SFXManager.instance.PlaysfxPitch(SFXManager.instance.BossSound, soundindex);
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
        if (VortexCounter < 0)
        {
            StartCoroutine(DelayVortex());
        }
    }
    public float StrikeDelayTime;
    public int StrikeBetTime;
    public float StrikeCounter;
    public GameObject StrikeArea;
    public GameObject CurStrikeArea;
    public GameObject StrikeATK;
    // Õª¥Ã
    IEnumerator DelayStrike()
    {   
        StrikeCounter = StrikeBetTime + StrikeDelayTime;
        CurStrikeArea = Instantiate(StrikeArea, Player.transform.position + new Vector3(Random.Range(-2, 3), Random.Range(-2, 3), 0), Quaternion.identity);
        CurStrikeArea.SetActive(true);
        yield return new WaitForSeconds(StrikeDelayTime);
        Strike();               
    }
    public void Strike()
    {
        Instantiate(StrikeATK, CurStrikeArea.transform.position + new Vector3(0,-.8f,0), Quaternion.identity).SetActive(true);
        Destroy(CurStrikeArea);
    }

    public float SweepDelayTime;
    public int SweepBetTime;
    public float SweepCounter;
    public GameObject SweepArea;
    public GameObject CurSweepArea;
    public GameObject SweepATK;
    // Õª¥Ã
    IEnumerator DelaySweep()
    {
        SweepCounter = SweepBetTime + SweepDelayTime;
        CurSweepArea = Instantiate(SweepArea, Player.transform.position + new Vector3(Random.Range(-2, 3), Random.Range(-2, 3), 0), Quaternion.identity);
        CurSweepArea.SetActive(true);
        yield return new WaitForSeconds(SweepDelayTime);
        Sweep();
    }
    public void Sweep()
    {
        Instantiate(SweepATK, CurSweepArea.transform.position + new Vector3(0, 9f, 0), Quaternion.identity).SetActive(true);
        Destroy(CurSweepArea);
    }
    public float DropDelayTime;
    public int DropBetTime;
    public float DropCounter;
    public GameObject DropArea;
    public GameObject CurDropArea;
    public GameObject DropATK;
    public Vector2 Direction;
    public float Angle;
    // Õª¥Ã
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
    // Õª¥Ã
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

    public int VortexDelayTime;
    public int VortexBetTime;
    public float VortexCounter;
    public GameObject VortexArea;
    public GameObject CurVortexArea;
    public GameObject VortexATK;
    // Õª¥Ã
    IEnumerator DelayVortex()
    {        
        VortexCounter = VortexBetTime + VortexDelayTime;
        CurVortexArea = Instantiate(VortexArea, Player.transform.position + new Vector3(Random.Range(-2, 3), Random.Range(-2, 3), 0), Quaternion.identity);
        CurVortexArea.SetActive(true);
        yield return new WaitForSeconds(VortexDelayTime);
        Vortex();
    }
    public void Vortex()
    {
        GameObject Vortex = Instantiate(VortexATK, CurVortexArea.transform.position, Quaternion.Euler(0, 0, Angle));
        Vortex.SetActive(true);              
        Destroy(CurVortexArea);
    }
}
