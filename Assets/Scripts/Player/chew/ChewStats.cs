using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChewStats : MonoBehaviour
{
    public float Damage, Range, TimeBetweenAttacks, Duration, Cost, Level;
    public int Maxtake;
    public bool Leader, DoubleProjectile, AutoRepair;    
}
