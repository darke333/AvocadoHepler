using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaffleCards : MonoBehaviour
{
    public float Yspace;
    public float Xspace;
    //public int Count;
    public Transform CardsParent;
    public List<GameObject> Cards = new List<GameObject>();
    List<GameObject> GameCards = new List<GameObject>();

    [HideInInspector]
    public enum Difficulty
    {
        Easy,
        Normal,
        Hard
    }

    Difficulty _diff;
    public Difficulty diff
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
                SpreadCards(2, 2);
            }

            if (value == Difficulty.Normal)
            {
                SpreadCards(4, 4);
            }

            if (value == Difficulty.Hard)
            {
                SpreadCards(6, 6);
            }
        }
    }

    void SpreadCards(int Ynum, int Xnum)
    {
        PickCards(Ynum * Xnum / 2);

        int index = 0;

        for (int i = 0; i < Ynum; i++)
        {
            float YShift = i * Yspace;

            for (int j = 0; j < Xnum; j++)
            {
                float XShift = j * Xspace;
                Instantiate(GameCards[index], new Vector3(XShift, YShift, 0), Quaternion.identity, transform);
                index++;
            }
        }
    }

    List<GameObject> PickCards(int Count)
    {
        GameCards = new List<GameObject>();
        List<GameObject> Temp = Cards;

        //Random selection
        for (int i = 0; i < Count; i++)
        {
            int index = rng.Next(Temp.Count);
            GameCards.Add(Temp[index]);
            GameCards.Add(Temp[index]);
            Temp.RemoveAt(index);
        }

            
        //var random = new System.Random();
        
        

        //Shaffle cards
        Shuffle(GameCards);

        return GameCards;
    }

    private static System.Random rng = new System.Random();


    void Shuffle(List<GameObject> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            GameObject value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform card in CardsParent)
        {
            Cards.Add(card.gameObject);
        }
        
        diff = Difficulty.Hard; 
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
