﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForCamera : MonoBehaviour
{
    public GameObject kust;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += new Vector3(0, 0.01f, 0);
        gameObject.transform.LookAt(kust.transform);
    }
}
