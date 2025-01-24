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
        Player = PlayerHealthController.instance.gameObject;
        Instance = this;
    }


    // Update is called once per frame
    void Update()
    {
        StrikeCounter -= Time.deltaTime;
        SweepCounter -= Time.deltaTime;
        if (StrikeCounter < 0)
        {
            StartCoroutine(DelayStrike());
        }
        if (SweepCounter < 0)
        {
            StartCoroutine(DelaySweep());
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
        Instantiate(StrikeATK, CurStrikeArea.transform.position, Quaternion.identity).SetActive(true);
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
        Instantiate(SweepATK, CurSweepArea.transform.position, Quaternion.identity).SetActive(true);
    }
}
