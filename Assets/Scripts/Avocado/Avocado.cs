using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avocado : MonoBehaviour
{
    [SerializeField] Transform Player;
    public BezierCurveScript bezier;
    public List<Vector3> path;
    Vector3 nextPoint;
    bool IsMoveAlongPath = false;
    [SerializeField] float speed = 1;
    [SerializeField] Animator AvocadoAnim;
    [SerializeField] Animator LeftHand;
    [SerializeField] Animator RightHand;
    [SerializeField] Dialogue_script dialogue;
    [SerializeField] Magic_Box_controller chestGame;
    [SerializeField] GameObject Camera;
    [SerializeField] List<GameObject> Bushes;



    public void FirstHello()
    {
        AvocadoAnim.Play("Jump");
        dialogue.DialogueText = "Nice to meet you. Now let's play";
        dialogue.HappyVoise = true;
        dialogue.Go = true;
        Invoke("ChestStartGame", 7);
    }

    public void ChestStartGame()
    {
        RightHand.Play("Chest");
        dialogue.DialogueText = "I have a magic bag, can you guess what’s in it?";
        dialogue.HappyVoise = true;
        dialogue.Go = true;
        chestGame.Go = true;
    }



    public void ChestGameEnd(bool T)
    {
        RightHand.Play("ChestEnd");
        if (T)
        {
            dialogue.DialogueText = "Correct! Well done!";            
            dialogue.YesVoise = true;
            dialogue.Go = true;
            LeftHand.Play("ThumbsUp");

        }
        else
        {
            dialogue.DialogueText = "Hmm, I don’t know what it is, let's see!";
            dialogue.NoVoise = true;
            dialogue.Go = true;
            LeftHand.Play("FingerNO");

        }
        Invoke("Transition",7);
    }

    public void Transition()
    {
        RightHand.Play("SweatOff");        
        dialogue.DialogueText = "I guess we are done! Fuh, I’m tired, let's go have some tea!";
        dialogue.HappyVoise = true;
        dialogue.Go = true;
        Invoke("WhereBike", 7);
    }

    public void WhereBike()
    {
        AvocadoAnim.Play("WhereBike");
        dialogue.DialogueText = "Where did I leave my bike? Can you help me find him?";
        dialogue.SadVoise = true;
        dialogue.Go = true;
        foreach(GameObject bush in Bushes)
        {
            bush.GetComponent<Collider>().enabled = true;
        }
    }

    public void BushRemoved(GameObject sender)
    {
        if (sender.name == "Bush_Bike")
        {
            RightHand.StopPlayback();

            RightHand.Play("YesBike");
            dialogue.DialogueText = "There it is! Let's go get it!";
            dialogue.HappyVoise = true;
            dialogue.Go = true;
            Invoke("WelcomHome", 7);

        }
        else
        {
            RightHand.Play("NoBike");
        }
        sender.GetComponent<Animator>().enabled = true;


    }

    public void WelcomHome()
    {
        GetComponent<Animator>().enabled = true;
        Camera.GetComponent<Animator>().enabled = true;

    }

    public void PlayJumpAnim()
    {
        AvocadoAnim.Play("Jump");

    }

    public void AfterEnter()
    {
        AvocadoAnim.Play("Happy Jump");
        dialogue.DialogueText = "Here we are at my house";
        dialogue.HappyVoise = true;
        dialogue.Go = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("FirstHello", 2);
        nextPoint = transform.position;
    }

    public void AssignPath(BezierCurveScript bezier)
    {
        path = bezier.path.pathPoints;
        IsMoveAlongPath = true;
        nextPoint = bezier.path.pathPoints[0];
    }

    void MoveAlongPath()
    {
        if (transform.position != nextPoint)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPoint, speed * Time.deltaTime);

        }
        else if (IsMoveAlongPath)
        {
            int i = path.LastIndexOf(nextPoint);
            if (i != path.Count - 1)
            {
                nextPoint = path[i + 1];
            }
            else
            {
                IsMoveAlongPath = false;
            }
        }

        if (IsMoveAlongPath)
        {
            Quaternion eulure = Quaternion.LookRotation(nextPoint);
            eulure = Quaternion.Inverse(eulure);
            eulure.x = 0;
            eulure.z = 0;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, eulure, Time.deltaTime * 100);
        }
        else
        {
            Quaternion eulure = Quaternion.LookRotation(Player.position);

            //eulure.x = 0;
            //eulure.z = 0;
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, eulure, Time.deltaTime * 60);
        }
    }



    // Update is called once per frame
    void Update()
    {
       // if (Input.GetKeyDown(KeyCode.Space))
       // {
            //AssignPath(bezier);

        //}
        //MoveAlongPath();
    }
}
