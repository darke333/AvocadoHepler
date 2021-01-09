using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShaffleCards : ChooseGame
{

    List<Transform> CardsToRotate = new List<Transform>();
    Transform LastPicked;

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
                StartCoroutine(StartFunc(2, 2));
                left = 4;
            }

            if (value == Difficulty.Normal)
            {
                StartCoroutine(StartFunc(4, 4));
                left = 16;
            }

            if (value == Difficulty.Hard)
            {
                StartCoroutine(StartFunc(6, 6));
                left = 36;
            }
        }
    }

    void SpreadCards(int Ynum, int Xnum)
    {
        List<GameObject> Temp = new List<GameObject>();

        Temp = PickCards(Ynum * Xnum / 2);
        float YStart = -(Ynum - 1) * Yspace  ;
        float XStart = -(Xnum-1) * Xspace  ;
        //float YStart = 0;
        //float XStart = 0;
        int index = 0;

        for (int i = 0; i < Ynum; i++)
        {
            float YShift = i * Yspace + YStart;
            YShift += i * Yspace;
            for (int j = 0; j < Xnum; j++)
            {
                float XShift = j * Xspace + XStart;
                XShift += j * Xspace;
                GameObject card = Instantiate(Temp[index], transform);
                card.transform.localPosition = new Vector3(XShift, YShift, 0);
                card.transform.Rotate(0, 180, 0);
                InGameObjs.Add(card);
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
                InGameObjs.Remove(card.gameObject);
                InGameObjs.Remove(LastPicked.gameObject);
            }
            else
            {
                WrongPicked.Invoke();
                CardsToRotate.Add(card);
                CardsToRotate.Add(LastPicked);                

                foreach (GameObject Card in InGameObjs)
                {
                    Card.GetComponent<Collider>().enabled = false;
                }

                Invoke("RotateCard", 3);

                
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
        // = Difficulty.Normal;
    }

    IEnumerator StartFunc(int Ynum, int Xnum)
    {

        foreach (Transform card in ObjsParent)
        {
            GameObjs.Add(card.gameObject);
        }

        SpreadCards(Ynum, Xnum);

        foreach (GameObject card in InGameObjs)
        {
            CardsToRotate.Add(card.transform);
        }
        float timer = 0;
        switch (Diff)
        {
            case Difficulty.Easy:
                timer = 5;
                break;
            case Difficulty.Normal:
                timer = 7.5f;
                break;
            case Difficulty.Hard:
                timer = 10;
                break;
        }
        yield return new WaitForSeconds(timer);


        //foreach (GameObject card in InGameObjs)
        //    card.GetComponent<Collider>().enabled = true;
        RotateCard();
    }

    void RotateCard()
    {
        foreach (Transform card in CardsToRotate)
        {
            card.rotation *= Quaternion.AngleAxis(180, card.transform.up);
        }
        foreach (Transform card in CardsToRotate)
            card.GetComponent<Collider>().enabled = true;
        CardsToRotate = new List<Transform>();

        foreach (GameObject card in InGameObjs)
            card.GetComponent<Collider>().enabled = true;

    }

    void RotateCard(Transform Card)
    {
        Card.GetComponent<Collider>().enabled = false;

        Card.rotation *= Quaternion.AngleAxis(180, Card.up);
    }

}
