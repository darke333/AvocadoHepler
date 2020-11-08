using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Park_controller : MonoBehaviour
{
    public GameObject[] Bushs = new GameObject[8];
    public GameObject Bicycle;
    public bool Go;
    System.Random rand;
    int rez;
    
    void Start()
    {
        rand = new System.Random((int)DateTime.Now.Second);
        rez = rand.Next(0, 7);
        Instantiate(Bicycle, Bushs[rez].transform).transform.SetParent(Bushs[rez].transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (Go)
        {
            Go = false;
            
        }
    }
}
