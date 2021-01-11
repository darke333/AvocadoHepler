using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleLand : MonoBehaviour
{

    [SerializeField] Transform Player;

    [SerializeField] Animator AvocadoAnim;
    [SerializeField] Animator LeftHand;
    [SerializeField] Animator RightHand;
    [SerializeField] Dialogue_script dialogue;
    [SerializeField] Puzzle Puzzle;
    public MemoGame game;
    public GameObject CurrentGame;

    [SerializeField] GameObject GameUI;
    [SerializeField] GameObject DiffUI;
    [SerializeField] GameObject EndBoxButton;



    public enum MemoGame
    {
        Puzzle
    }


    public void FirstHello()
    {
        AvocadoAnim.Play("Jump");
        dialogue.DialogueText = "Nice to meet you. Choose game you want to play!";
        dialogue.HappyVoise = true;
        dialogue.Go = true;
        Invoke("ChestStartGame", 7);
    }

    public void GamePicked(int memoGame)
    {
        game = (MemoGame)memoGame;

        CurrentGame = Instantiate(Puzzle.gameObject);

        GameUI.SetActive(false);
        DiffUI.SetActive(true);

    }

    public void FinishCurrentLevel()
    {
        if (GameUI.activeSelf)
        {
            EndBoxButton.SetActive(false);
            GameUI.SetActive(false);
            Invoke("EndPickGame", 2);
        }
        else
        {
            dialogue.DialogueText = "Lets play again!";
            dialogue.HappyVoise = true;
            dialogue.Go = true;
            RestartGames();
        }
    }

    void RestartGames()
    {
        if (CurrentGame)
        {
            Destroy(CurrentGame);
        }
        DiffUI.SetActive(false);

        GameUI.SetActive(true);
    }

    public void DifficultyPicked(int diff)
    {
        Game.Difficulty difficulty = (Game.Difficulty)diff;
        CurrentGame.SetActive(true);

        StartCoroutine(PuzzleGame(difficulty));


        DiffUI.SetActive(false);

    }


    public void PickedCorrect()
    {
        dialogue.DialogueText = "Correct! Well done!";
        dialogue.YesVoise = true;
        dialogue.Go = true;
        LeftHand.Play("ThumbsUp");
    }

    public void PickedWrong()
    {
        dialogue.DialogueText = "Hmm, I'm not sure";
        dialogue.NoVoise = true;
        dialogue.Go = true;
        //LeftHand.Play("FingerNO");
    }

    IEnumerator PuzzleGame(Game.Difficulty difficulty)
    {
        dialogue.DialogueText = "Place pieces of puzzle at their correct spots.";
        dialogue.HappyVoise = true;
        dialogue.Go = true;

        yield return new WaitForSeconds(10);

        CurrentGame.GetComponent<Puzzle>().Diff = difficulty;
    }


    public void GameFinished()
    {
        AvocadoAnim.Play("Jump");
        dialogue.DialogueText = "Well Done! Lets play again!";
        dialogue.HappyVoise = true;
        dialogue.Go = true;
        Invoke("FinishEmote", 5);
    }

    void FinishEmote()
    {
        RestartGames();
    }

    public void EndPickGame()
    {
        dialogue.DialogueText = "Thanks for the help! See you later!!!";
        dialogue.HappyVoise = true;
        dialogue.Go = true;
        RightHand.Play("Bye");
        LoadNextScene();
    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadLevel(0));
    }

    IEnumerator LoadLevel(int level)
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(level);
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("FirstHello", 2);
    }




}
