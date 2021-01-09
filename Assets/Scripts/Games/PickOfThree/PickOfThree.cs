using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickOfThree : Game
{
    public Cup Left;
    public Cup Right;
    public Cup Center;

    public int Count;

    public float Speed = 1;

    float timer;

    Difficulty _diff;
    public Difficulty Diff
    {
        get
        {
            return _diff;
        }
        set
        {
            _diff = value;
            if (value == Difficulty.Easy)
            {
                StartCoroutine(StartGame(5,1));
            }

            if (value == Difficulty.Normal)
            {
                StartCoroutine(StartGame(10, 2));
            }

            if (value == Difficulty.Hard)
            {
                StartCoroutine(StartGame( 15, 3));
            }
        }
    }

    IEnumerator StartGame(int count, int speed)
    {
        Center.Show();
        yield return new WaitForSeconds(4f);
        Center.animator.SetBool("Show", false);
        yield return new WaitForSeconds(1f);

        Center.animator.enabled = false;
        Count = count;
        Speed = speed;
    }

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

    public void CupPicked(Cup cup)
    {
        if(cup == Center)
        {
            EndGameEvent.Invoke();
        }
        else
        {
            WrongPicked.Invoke();
        }
        cup.Show();
    }

    private void Update()
    {
        if(Count > 0)
        {
            if (timer > 0)
                timer -= Time.deltaTime * Speed;
            else { timer = 1; Count--; flip = GetRandomEnum(); }
                
        }
        if (Count == 0)
        {
            Left.GetComponent<Collider>().enabled = true;
            Center.GetComponent<Collider>().enabled = true;
            Right.GetComponent<Collider>().enabled = true;
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
