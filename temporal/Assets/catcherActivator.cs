using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catcherActivator : MonoBehaviour
{
    public GameObject ai;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time  >3)
        {
            ai.SetActive ( true);
        }
    }
}
