using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickOfThree : MonoBehaviour
{
    public Cup Left;
    public Cup Right;
    public Cup Center;

    public int Count;

    public float speed = 1;

    float timer;

    [HideInInspector] public enum Flip
    {
        RightLeft,
        CenterLeft,
        CenterRight
    }

    Flip _flip;
    public Flip flip
    {
        get
        {
            return _flip;
        }
        set
        {
            _flip = value;
            if(value == Flip.RightLeft)
            {
                CupFlip(Right, Left);                
            }

            if (value == Flip.CenterLeft)
            {
                CupFlip(Left, Center);
            }

            if (value == Flip.CenterRight)
            {
                CupFlip(Center, Right);
            }
        }
    }

    void CupFlip(Cup First, Cup Second)
    {
        First.AssignAndStart(Second.transform.localPosition, 1);
        Second.AssignAndStart(First.transform.localPosition, -1);

        Cup Temp = First;
        First = Second;
        Second = Temp;
    }

    private void Start()
    {
        timer = 1;
    }

    private void Update()
    {
        if(Count > 0)
        {
            if (timer > 0)
                timer -= Time.deltaTime;
            else { timer = 1; Count--; flip = GetRandomEnum(); }
                
        }
    }

    Flip GetRandomEnum()
    {
        Flip randomBar;
        do
        {
            Array values = Enum.GetValues(typeof(Flip));
            System.Random random = new System.Random();
            randomBar = (Flip)values.GetValue(random.Next(values.Length));

        } while (randomBar == _flip);
        

        return randomBar;
    }


}
