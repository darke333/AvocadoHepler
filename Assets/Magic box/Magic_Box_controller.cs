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

    [SerializeField] List<Transform> transforms;

    [HideInInspector]
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
            i = Randomizer(0,9);
            if (k != i)
            {
                Go = false;
                Destroy(ThisObject);
                ThisObject = Instantiate(game_Object[i], box.transform.position, box.transform.rotation, box.transform);
                ThisObject.transform.SetParent(box.transform);
                placeObj();
                //ThisObject = GameObject.Find(game_Object[i].name + "(Clone)");
            }
        }
    }


    void placeObj()
    {
        int j = Randomizer(0, 3);
        GameObject game = Instantiate(ThisObject, transforms[j].position, transforms[j].rotation);
        game.transform.localScale *= 3;

        List<GameObject> gameObjects = new List<GameObject>();
        foreach (GameObject gameObject in game_Object)
        {
            if (game != gameObject)
            {
                gameObjects.Add(gameObject);
            }
        }

        for (int m = 0; m < 4; m++)
        {
            if (m != j)
            {
                int l = Randomizer(0, gameObjects.Count - 1);
                Instantiate(gameObjects[l], transforms[j].position, transforms[j].rotation);
                gameObjects.RemoveAt(l);
            }
        }
    }

    int Randomizer(int start, int finish)
    {
        int rez=0;
        bool log = true;
        while (log)
        {
            log = false;
            rez = rand.Next(start, finish);
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
