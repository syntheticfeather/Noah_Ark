using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public static Boss Instance;
    public int StrikeDelayTime;
    public int StrikeBetTime;
    public float StrikeCounter;
    public GameObject StrikeArea;
    public GameObject CurStrikeArea;
    public GameObject StrikeATK;

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
        if (StrikeCounter < 0)
        {
            StartCoroutine(DelayStrike());
        }
    }

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
}
