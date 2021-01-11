using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MemoVillage : MonoBehaviour
{


    [SerializeField] Transform Player;

    [SerializeField] Animator AvocadoAnim;
    [SerializeField] Animator LeftHand;
    [SerializeField] Animator RightHand;
    [SerializeField] Dialogue_script dialogue;
    [SerializeField] PickOfThree PickOfThree;
    [SerializeField] ShaffleCards ShaffleCards;
    [SerializeField] RememberObj RememberObj;
    public MemoGame game;
    public GameObject CurrentGame;

    [SerializeField] GameObject GameUI;
    [SerializeField] GameObject DiffUI;
    [SerializeField] GameObject EndBoxButton;



    public enum MemoGame
    {
        Cups,
        Cards,
        Gems
    }


    public void FirstHello()
    {
        AvocadoAnim.Play("Jump");
        dialogue.DialogueText = "Nice to meet you. Choose the game you want to play!";
        dialogue.HappyVoise = true;
        dialogue.Go = true;
        Invoke("ChestStartGame", 7);
    }

    public void GamePicked(int memoGame)
    {
        game = (MemoGame)memoGame;
        switch (game)
        {
            case MemoGame.Cups:
                CurrentGame = Instantiate(PickOfThree.gameObject);
                break;
            case MemoGame.Cards:
                CurrentGame = Instantiate(ShaffleCards.gameObject);
                break;
            case MemoGame.Gems:
                CurrentGame = Instantiate(RememberObj.gameObject);
                break;
        }
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
        switch (game)
        {
            case MemoGame.Cups:
                StartCoroutine(CupGame(difficulty));
                break;
            case MemoGame.Cards:
                StartCoroutine(CardGame(difficulty));
                break;
            case MemoGame.Gems:
                StartCoroutine(GemGame(difficulty));
                break;
        }
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

    IEnumerator CupGame(Game.Difficulty difficulty)
    {
        dialogue.DialogueText = "Remember, where the cup with the ball is. Point at it after they are moved.";
        dialogue.HappyVoise = true;
        dialogue.Go = true;

        yield return new WaitForSeconds(10);

        CurrentGame.GetComponent<PickOfThree>().Diff = difficulty;
    }

    IEnumerator CardGame(Game.Difficulty difficulty)
    {
        dialogue.DialogueText = "Remember cards positions. After they are turned over, select identical pairs.";
        dialogue.HappyVoise = true;
        dialogue.Go = true;

        yield return new WaitForSeconds(10);

        CurrentGame.GetComponent<ShaffleCards>().Diff = difficulty;
    }

    IEnumerator GemGame(Game.Difficulty difficulty)
    {
        dialogue.DialogueText = "Remember gems at the start. Select the right ones after they all are shuffled.";
        dialogue.HappyVoise = true;
        dialogue.Go = true;

        yield return new WaitForSeconds(10);

        CurrentGame.GetComponent<RememberObj>().Diff = difficulty;
    }

    public void GameFinished()
    {
        AvocadoAnim.Play("Jump");
        dialogue.DialogueText = "Well Done! Let's play again!";
        dialogue.HappyVoise = true;
        dialogue.Go = true;
        Invoke("FinishEmote",5);
    }

    void FinishEmote()
    {
        RestartGames();
    }

    public void EndPickGame()
    {
        dialogue.DialogueText = "Thanks for help! See you later!!!";
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
