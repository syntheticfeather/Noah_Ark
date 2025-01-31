using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResouceManager : MonoBehaviour
{
    public int Wood { get; set; }
    public int Stone { get; set; }
    public int Food { get; set; }



    public void InitialResource(int initialWood, int initialStone, int initialFood)
    {
        Wood = initialWood;
        Stone = initialStone;
        Food = initialFood;
    }

    public void AddWood(int amount)
    {
        Wood += amount;

    }

    public void AddStone(int amount)
    {
        Stone += amount;

    }

    public void AddFood(int amount)
    {
        Food += amount;

    }

    public void UseWood(int amount)
    {
        if (Wood >= amount)
        {
            Wood -= amount;

        }
    }

    public void UseStone(int amount)
    {
        if (Stone >= amount)
        {
            Stone -= amount;

        }
    }

    public void UseFood(int amount)
    {
        if (Food >= amount)
        {
            Food -= amount;

        }
    }
}