using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour
{

    public Animator animator;
    public AnimationCurve curve;
    int Inverse;
    Vector3 end;
    [HideInInspector] public bool Moving;
    public bool Correct;
    PickOfThree controller;

    Vector3 start;
    float time;

    void Start()
    {
        controller = transform.parent.GetComponent<PickOfThree>();
        animator = GetComponent<Animator>();
        start = transform.localPosition;
        end = start;
        Moving = false;
    }

    public void AssignAndStart(Vector3 endPos, int inverse)
    {
        Inverse = inverse;
        end = endPos;
        start = transform.localPosition;
        time = 0;
        Moving = true;
    }

    void Move()
    {
        if (Moving)
        {
            time += Time.deltaTime * controller.Speed;
            Vector3 pos = Vector3.Lerp(start, end, time);
            pos.z += curve.Evaluate(time) * Inverse;
            transform.localPosition = pos;
        }
        
    }

    public void Show()
    {
        animator.enabled = true;
        animator.SetBool("Show", true);
        animator.Play("Show");
    }

    void Update()
    {
        Move();
    }


}
