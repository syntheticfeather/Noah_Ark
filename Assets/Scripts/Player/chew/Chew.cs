using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chew : MonoBehaviour
{   
    public List<ChewStats> Stats;
    public Sprite sprite;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
}

[System.Serializable]
public class ChewStats
{
    public float Damage, Range, TimeBetweenAttacks, Duration;
    //public string UpGradeText;?
}
