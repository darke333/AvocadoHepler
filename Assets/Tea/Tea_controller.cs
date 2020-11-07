using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tea_controller : MonoBehaviour
{
    public GameObject[] Inventar = new GameObject[12];

    void Start()
    {
        for (int i=0; i<12; i++)
        {
            Instantiate(Inventar[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
