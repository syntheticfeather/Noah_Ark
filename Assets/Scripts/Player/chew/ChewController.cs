using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChewController : MonoBehaviour
{
    public static ChewController instance;
    public List<GameObject> ChewList = new List<GameObject>();
    public int MaxChew;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
