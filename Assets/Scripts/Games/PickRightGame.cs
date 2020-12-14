using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickRightGame : MonoBehaviour
{   

    public UnityEvent WrongPicked;

    public UnityEvent RightPicked;

    public UnityEvent EndGameEvent;

    delegate void _ObjPicked(GameObject pickedObj);

    _ObjPicked ObjPicked;

    //UnityAction ObjPicked;

    [SerializeField]
    List<PickObj> pickObjs = new List<PickObj>();
    int CorrectCount;
    [SerializeField] GameObject Poof;

    void Picked(GameObject pickedObj)
    {
        for(int i = 0; i < pickObjs.Count; i++)
        {
            PickObj pickObj = pickObjs[i];
            if (pickObj.gameObject == pickedObj)
            {
                if (pickObj.Correct)
                {
                    CorrectCount--;
                    RightPicked.Invoke();

                }
                else
                {
                    WrongPicked.Invoke();
                }

                DestroyItem(pickObj);
                break;
            }
        }
        if (CorrectCount == 0)
        {
            EndGame();
        }
    }


    void DestroyItem(PickObj item)
    {
        GameObject itemObj = item.gameObject;
        Instantiate(Poof, itemObj.transform.position, Quaternion.identity, transform);
        pickObjs.Remove(item);
        Destroy(itemObj);
    }

    void EndGame()
    {
        if (pickObjs.Count != 0)
        {
            for (int i = 0; i < pickObjs.Count; i++)
            {
                DestroyItem(pickObjs[i]);

            }
            
        }
        EndGameEvent.Invoke();
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        ObjPicked = Picked;

        CorrectCount = 0;

        foreach (Transform Child in transform)
        {
            AddAndSetPickObj(Child);
        }        
    }

    void AddAndSetPickObj(Transform Child)
    {
        if (Child.GetComponent<PickObj>())
        {
            PickObj pickObj = Child.GetComponent<PickObj>();            
            
            if (pickObj.Correct)
            {
                CorrectCount++;
            }
            pickObjs.Add(pickObj);

            GameObject temp = Child.gameObject;
            Child.GetComponent<ClickObject>().unityEvent.AddListener(() => { Picked(temp); });
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
