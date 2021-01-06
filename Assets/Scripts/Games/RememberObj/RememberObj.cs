using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RememberObj : Game
{
    [SerializeField] List<GameObject> Gems = new List<GameObject>();

    List<GameObject> InGameGems = new List<GameObject>();

    private static System.Random rng = new System.Random();
    public Transform GemsParent;
    int GemsLeft;

    public float Yspace;
    public float Xspace;

    // Start is called before the first frame update
    void Start()
    {

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
                ShowGems(2, 6);
                GemsLeft = 2;
            }

            if (value == Difficulty.Normal)
            {
                ShowGems(4, 10);
                GemsLeft = 4;
            }

            if (value == Difficulty.Hard)
            {
                ShowGems(6, 15);
                GemsLeft = 6;
            }
        }
    }

    IEnumerator ShowGems(int CorrectGemsCount, int TotalGemsCount)
    {

        List<GameObject> TotalGems = PickGems(TotalGemsCount, Gems);

        List<GameObject> CorrectGems = PickGems(CorrectGemsCount, TotalGems);

        int index = 0;
        for (int i = 0; i < CorrectGemsCount / 2; i++)
        {
            float YShift = i * Yspace;

            for (int j = 0; j < CorrectGemsCount / 2; j++)
            {
                float XShift = j * Xspace;
                Instantiate(CorrectGems[index], new Vector3(XShift, YShift, 0), Quaternion.Euler(0, 180, 0), transform);
                index++;

            }
            
        }
        yield return new WaitForSeconds(0);
    } 
        


    List<GameObject> PickGems(int Count, List<GameObject> GemsPool)
    {
        List<GameObject> GameGems = new List<GameObject>();
        List<GameObject> Temp = GemsPool;

        //Random selection
        for (int i = 0; i < Count; i++)
        {
            int index = rng.Next(Temp.Count);
            GameGems.Add(Temp[index]);
            Temp.RemoveAt(index);
        }

        return GameGems;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
