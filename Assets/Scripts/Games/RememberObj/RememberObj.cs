using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RememberObj : ChooseGame
{


    public List<GameObject> CorrectGems;

    Transform Poses;

    public Transform PosesParent;

    [SerializeField]
    GameObject GoodPoof;
    [SerializeField]
    GameObject BadPoof;

    // Start is called before the first frame update
    void Start()
    {
        //diff = Difficulty.Easy;
        //diff = Difficulty.Normal;
        //diff = Difficulty.Hard;

    }



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
                Poses = PosesParent.transform.Find("Easy");
                StartCoroutine(ShowGems(2, 5));
                left = 2;
            }

            if (value == Difficulty.Normal)
            {
                Poses = PosesParent.transform.Find("Medium");
                StartCoroutine(ShowGems(5, 10));
                left = 4;
            }

            if (value == Difficulty.Hard)
            {
                Poses = PosesParent.transform.Find("Hard");
                StartCoroutine(ShowGems(9, 15));
                left = 6;
            }
        }
    }

    IEnumerator ShowGems(int CorrectGemsCount, int TotalGemsCount)
    {
        foreach (Transform gem in ObjsParent)
        {
            GameObjs.Add(gem.gameObject);
        }

        List<GameObject> TotalGems = PickGems(TotalGemsCount, GameObjs);

        CorrectGems = PickGems(CorrectGemsCount, TotalGems);
        
        SpreadGems(CorrectGems, InGameObjs, Poses.transform.GetChild(0));

        yield return new WaitForSeconds(5);

        foreach (GameObject gem in InGameObjs)
        {
            Destroy(gem);
        }
        InGameObjs = new List<GameObject>();

        SpreadGems(TotalGems, InGameObjs, Poses.transform.GetChild(1));

        foreach (GameObject gem in InGameObjs)
        {
            gem.GetComponent<Collider>().enabled = true;
        }
        
    } 

    void SpreadGems(List<GameObject> OutGems, List<GameObject> InGems, Transform poses)        
    {
        int index = 0;
        foreach(Transform pos in poses)
        {
            GameObject gem = Instantiate(OutGems[index], pos.position, pos.rotation, ObjsParent);
            gem.SetActive(true);
            InGems.Add(gem);
            index++;
        }      
        
    }

    public void GemPicked(GameObject selectedGem)
    {
        bool Correct = false;
        int index = 0;
        foreach (GameObject gem in CorrectGems)
        {
            if (gem.name + "(Clone)" == selectedGem.name)
            {
                Correct = true;
                index = CorrectGems.IndexOf(gem);
            }
        }
        if (Correct)
        {
            Instantiate(GoodPoof, selectedGem.transform.position, selectedGem.transform.rotation).SetActive(true);
            CorrectGems.RemoveAt(index);
            RightPicked.Invoke();
        }
        else
        {
            Instantiate(BadPoof, selectedGem.transform.position, selectedGem.transform.rotation).SetActive(true);
            WrongPicked.Invoke();
        }
        Destroy(selectedGem);
        InGameObjs.Remove(selectedGem);
        if (CorrectGems.Count == 0)
            EndGame();
        

    }

    void EndGame()
    {
        for(int i = 0; i < InGameObjs.Count; i++)
        {
            GameObject gem = InGameObjs[i];
            Instantiate(GoodPoof, gem.transform.position, gem.transform.rotation).SetActive(true);
            //InGameObjs.RemoveAt(i);
            Destroy(gem);
        }
        InGameObjs = new List<GameObject>();
        EndGameEvent.Invoke();
    }

    List<GameObject> PickGems(int Count, List<GameObject> GemsPool)
    {
        List<GameObject> GameGems = new List<GameObject>();
        List<GameObject> Temp = new List<GameObject>();
        Temp.AddRange(GemsPool);

        //Random selection
        for (int i = 0; i < Count; i++)
        {
            int index = rng.Next(Temp.Count);
            GameGems.Add(Temp[index]);
            Temp.RemoveAt(index);
        }

        return GameGems;
    }

}
