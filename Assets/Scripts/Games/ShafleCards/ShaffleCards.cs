using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShaffleCards : ChooseGame
{

    List<Transform> CardsToRotate = new List<Transform>();
    Transform LastPicked;

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
                left = 4;
            }

            if (value == Difficulty.Normal)
            {
                SpreadCards(4, 4);
                left = 16;
            }

            if (value == Difficulty.Hard)
            {
                SpreadCards(6, 6);
                left = 36;
            }
        }
    }

    void SpreadCards(int Ynum, int Xnum)
    {
        List<GameObject> Temp = new List<GameObject>();

        Temp = PickCards(Ynum * Xnum / 2);

        int index = 0;

        for (int i = 0; i < Ynum; i++)
        {
            float YShift = i * Yspace;

            for (int j = 0; j < Xnum; j++)
            {
                float XShift = j * Xspace;
                InGameObjs.Add( Instantiate(Temp[index], new Vector3(XShift, YShift, 0), Quaternion.Euler(0,180,0), transform) );
                index++;
            }
        }
    }

    List<GameObject> PickCards(int Count)
    {
        List<GameObject> GameCards = new List<GameObject>();
        List<GameObject> Temp = new List<GameObject>();
        Temp.AddRange(GameObjs);

        //Random selection
        for (int i = 0; i < Count; i++)
        {
            int index = rng.Next(Temp.Count);
            GameCards.Add(Temp[index]);
            GameCards.Add(Temp[index]);
            Temp.RemoveAt(index);
        }

        //Shaffle cards
        Shuffle(GameCards);

        return GameCards;
    }



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

    public void CardPicked(Transform card)
    {
        
        RotateCard(card);


        if (!LastPicked)
            LastPicked = card;
        else
        {
            if (card.name == LastPicked.name)
            {
                left -= 2;
                RightPicked.Invoke();
            }
            else
            {
                WrongPicked.Invoke();
                CardsToRotate.Add(card);
                CardsToRotate.Add(LastPicked);
                RotateCard();
                card.GetComponent<Collider>().enabled = true;
                LastPicked.GetComponent<Collider>().enabled = true;
            }
            LastPicked = null;
        }
        if (left == 0)
        {
            EndGameEvent.Invoke();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartFunc());
    }

    IEnumerator StartFunc()
    {

        foreach (Transform card in ObjsParent)
        {
            GameObjs.Add(card.gameObject);
        }

        diff = Difficulty.Easy;
        //CardsToRotate = InGameObjs;

        foreach (GameObject card in InGameObjs)
        {
            CardsToRotate.Add(card.transform);
        }
        

        yield return new WaitForSeconds(5);

        
        foreach (GameObject card in InGameObjs)
            card.GetComponent<Collider>().enabled = true;
        RotateCard();
    }

    void RotateCard()
    {
       // foreach (GameObject card in InGameObjs)
       //     card.GetComponent<Collider>().enabled = false;
        //yield return new WaitForSeconds(5);

        foreach (Transform card in CardsToRotate)
        {
            card.rotation *= Quaternion.AngleAxis(180, card.transform.up);
        }
        foreach (Transform card in CardsToRotate)
            card.GetComponent<Collider>().enabled = true;
        CardsToRotate = new List<Transform>();

        
    }

    void RotateCard(Transform Card)
    {
        Card.GetComponent<Collider>().enabled = false;

        Card.rotation *= Quaternion.AngleAxis(180, Card.up);        
    }

    // Update is called once per frame
    void Update()
    {
       // RotateCard();
    }
}
