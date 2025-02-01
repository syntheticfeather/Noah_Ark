using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public int Wood { get; set; }
    public int Stone { get; set; }
    public int Food { get; set; }
    public int Crystal { get; set; }

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

    public bool UseCrystal(int amount)
    {
        if (Crystal >= amount)
        {
            Crystal -= amount;
            return true;
        }
        else
        {
            return false;
        }
       
    }

    public bool UseResource(int FoodAmount, int StoneAmount)
    {
        if (Food >= FoodAmount && Stone >= StoneAmount)
        {
            Stone -= StoneAmount;
            Food -= FoodAmount;
            return true;
        }

        else
        {
            return false;
        }
    }
}