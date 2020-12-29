using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour
{

    Animator animator;
    public AnimationCurve curve;
    int Inverse;
    Vector3 end;
    [HideInInspector] public bool Moving;
    public bool Correct;

    Vector3 start;
    float time;

    void Start()
    {
        animator = GetComponent<Animator>();
        start = transform.position;
        end = start;
        Moving = false;
    }

    public void AssignAndStart(Vector3 endPos, int inverse)
    {
        Inverse = inverse;
        end = endPos;
        start = transform.position;
        time = 0;
        Moving = true;
    }

    void Move()
    {
        if (Moving)
        {
            time += Time.deltaTime;
            Vector3 pos = Vector3.Lerp(start, end, time);
            pos.z += curve.Evaluate(time) * Inverse;
            transform.localPosition = pos;
        }
        
    }

    public void Show()
    {
        animator.SetBool("Show", true);
    }

    void Update()
    {
        Move();
    }


}
