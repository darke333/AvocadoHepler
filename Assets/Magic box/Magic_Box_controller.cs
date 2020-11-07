using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic_Box_controller : MonoBehaviour
{
    public bool Go;
    public GameObject[] game_Object = new GameObject[10];
    public GameObject box;
    public GameObject ThisObject;
    int k,i;
    int Tackt=0;
    System.Random rand;

    public int [] znach = new int [4];

    private void Start()
    {
        rand = new System.Random((int)DateTime.Now.Second);
        
    }

    void Update()
    {
        if (Go)
        {
            k = i;
            i = Randomizer();
            if (k != i)
            {
                Go = false;
                Destroy(ThisObject);
                Instantiate(game_Object[i]).transform.SetParent(box.transform);
                ThisObject = GameObject.Find(game_Object[i].name + "(Clone)");
            }
        }
    }

    int Randomizer()
    {
        int rez=0;
        bool log = true;
        while (log)
        {
            log = false;
            rez = rand.Next(0, 9);
            for (int i = 0; i < 4; i++)
            {
                if (rez == znach[i])
                {
                    log = true;
                }
            }
        }
        znach[Tackt] = rez;
        if (Tackt == 3)
        {
            Tackt = 0;
        }
        else
        {
            Tackt++;
        }
        return rez;
    }
}
