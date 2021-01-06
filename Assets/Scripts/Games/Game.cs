using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Game : MonoBehaviour
{

    public UnityEvent WrongPicked;

    public UnityEvent RightPicked;

    public UnityEvent EndGameEvent;

    [HideInInspector]
    public enum Difficulty
    {
        Easy,
        Normal,
        Hard
    }

    Difficulty _diff;
    //public Difficulty diff;
    /*{
        /get
        {
            return _diff;
        }
        set
        {
            _diff = value;
            if (value == Difficulty.Easy)
            {
                SpreadCards(2, 2);
                CardsLeft = 4;
            }

            if (value == Difficulty.Normal)
            {
                SpreadCards(4, 4);
                CardsLeft = 16;
            }

            if (value == Difficulty.Hard)
            {
                SpreadCards(6, 6);
                CardsLeft = 36;
            }
        }
    }*/
    
}


