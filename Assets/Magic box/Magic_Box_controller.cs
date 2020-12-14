using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic_Box_controller : MonoBehaviour
{
    public bool Go;
    public GameObject[] Base_objects = new GameObject[10];
    public GameObject box;
    public GameObject ThisObject;
    int thisInt;
    int k,i;
    int Tackt=0;
    System.Random rand;
    [SerializeField] Avocado avocado;
    [SerializeField] Transform InBoxPos;
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
                if (ThisObject)
                {
                    Destroy(ThisObject);
                }
                foreach(Transform transform in transforms)
                {
                    transform.gameObject.SetActive(true);
                }
                //ThisObject = Instantiate(game_Object[i], InBoxPos.transform.position, InBoxPos.transform.rotation, InBoxPos.transform);
                //ThisObject.GetComponent<AudioSource>().Play();
                placeObj();
            }
        }
    }

    public void CheckAnswer(int number)
    {
        if (number == thisInt)
        {
            avocado.ChestGameEnd(true);            
        }
        else
        {
            avocado.ChestGameEnd(false);
        }
        ThisObject.GetComponent<Animator>().enabled = true;
        foreach(Transform transform in transforms)
        {
            transform.gameObject.SetActive(false);
        }
    }

    void placeObj()
    {
        thisInt = Randomizer(0, 3);
        ThisObject = Instantiate(Base_objects[thisInt], transforms[thisInt].position, transforms[thisInt].rotation, transforms[thisInt]);
        ThisObject.transform.localScale *= 5;
        ThisObject.GetComponent<AudioSource>().Play();
        List<GameObject> gameObjects = new List<GameObject>();
        foreach (GameObject gameObject in Base_objects)
        {
            if (ThisObject != gameObject)
            {
                gameObjects.Add(gameObject);
            }
        }

        for (int m = 0; m < 4; m++)
        {
            if (m != thisInt)
            {
                int l = Randomizer(0, gameObjects.Count - 1);
                Instantiate(gameObjects[l], transforms[m].position, transforms[m].rotation, transforms[m]).transform.localScale *= 5;
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
