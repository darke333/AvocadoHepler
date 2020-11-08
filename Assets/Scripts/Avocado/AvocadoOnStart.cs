using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvocadoOnStart : MonoBehaviour
{

    [SerializeField] Transform Player;

    [SerializeField] Animator AvocadoAnim;
    [SerializeField] Animator LeftHand;
    [SerializeField] Animator RightHand;
    [SerializeField] Dialogue_script dialogue;

    public void FirstHello()
    {
        LeftHand.Play("Hello");
        dialogue.HappyVoise = true;
        dialogue.Go = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        Invoke("FirstHello", 2);
    }



    void MoveAlongPath()
    {
        //Quaternion eulure = Quaternion.Loo(Player.position);
        transform.LookAt(Player.position);
        //eulure.x = 0;
        //eulure.z = 0;
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, eulure, Time.deltaTime * 60);
    }



    // Update is called once per frame
    void Update()
    {
        MoveAlongPath();
    }
}
