using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talent 
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Action<ChewStats> ApplyEffect { get; set; }

    public Talent(string name, string description, Action<ChewStats> applyEffect)
    {
        Name = name;
        Description = description;
        ApplyEffect = applyEffect;
    }
    
}
