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

}

public class ChooseGame : Game
{
    protected List<GameObject> InGameObjs = new List<GameObject>();

    public float Yspace;
    public float Xspace;

    public Transform ObjsParent;
    protected List<GameObject> GameObjs = new List<GameObject>();
    protected static System.Random rng = new System.Random();
    protected int left;

   
}


