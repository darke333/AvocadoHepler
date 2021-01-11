using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : Game
{
    [SerializeField] List<GameObject> Animals;
    [SerializeField] GameObject Common;
    [SerializeField] GameObject Map;

    public Transform CurrentPiece;
    public Transform CurrentPlace;

    [SerializeField] GameObject Pointer;
    [SerializeField] Material Wait;
    [SerializeField] Material Selected;

    List<Piece> pieces = new List<Piece>();

    protected static System.Random rng = new System.Random();


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
                //Random selection
                int index = rng.Next(Animals.Count);
                StartGame(Animals[index]);

            }

            if (value == Difficulty.Normal)
            {
                StartGame(Common);
            }

            if (value == Difficulty.Hard)
            {
                StartGame(Map);
            }
        }
    }

    void StartGame(GameObject puzzle)
    {
        GameObject currentPuzzle = Instantiate(puzzle, puzzle.transform.parent);
        currentPuzzle.SetActive(true);
        foreach (Transform pos in currentPuzzle.transform)
        {
            if (pos.tag == "Place")
            {
                Instantiate(Pointer, pos.position, pos.rotation).SetActive(true);
            }
            if (pos.tag == "Piece")
            {
                //pos.GetComponent<Piece>().Placed = true;

                pos.GetComponent<Piece>().CurrPlace = Instantiate(Pointer, pos.position, pos.rotation);
                pieces.Add(pos.GetComponent<Piece>());
            }

        }


    }

    public void PiecePicked(Transform piece)
    {
        if (CurrentPiece)
        {
            //CurrentPiece.GetComponent<MeshRenderer>().material = Wait;
        }

        CurrentPiece = piece;
        //CurrentPiece.GetComponent<MeshRenderer>().material = Selected;

        TryPlace();
    }

    public void PlaceSelected(Transform place)
    {

        if (CurrentPlace)
        {
            CurrentPlace.GetComponent<MeshRenderer>().material = Wait;
        }

        CurrentPlace = place;
        CurrentPlace.GetComponent<MeshRenderer>().material = Selected;

        TryPlace();
    }

    void TryPlace()
    {
        if(CurrentPiece && CurrentPlace)
        {
            CurrentPlace.GetComponent<MeshRenderer>().material = Wait;
            CurrentPlace.gameObject.SetActive(false);
            CurrentPiece.GetComponent<Piece>().CurrPlace.SetActive(true);
            CurrentPiece.GetComponent<Piece>().CurrPlace = CurrentPlace.gameObject;
            CurrentPiece.position = CurrentPlace.position;

            Piece Piece = CurrentPiece.GetComponent<Piece>();

            if (CurrentPiece.position == Piece.RightPlace.transform.position)
            {
                Piece.Placed = true;
                print("right");

                RightPicked.Invoke();
            }
            else
            {
                Piece.Placed = false;
            }

            int count = 0;
            foreach (Piece piece in pieces)
                if (piece.Placed)
                    count++;
            if (count == pieces.Count)
            {
                EndGameEvent.Invoke();
                print("finish");
            }

            CurrentPiece = null;
            CurrentPlace = null;





        }
    }
        

    // Start is called before the first frame update
    void Start()
    {
        Diff = Difficulty.Easy;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
