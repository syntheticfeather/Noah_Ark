using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class ExternalUpgrade : MonoBehaviour
{
    private ResourceManager resourceManager;
    void Start()
    {
        resourceManager = GetComponent<ResourceManager>();
    }
}
