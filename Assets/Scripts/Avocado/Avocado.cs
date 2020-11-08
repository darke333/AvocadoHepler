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


    public void FirstHello()
    {

    }


    // Start is called before the first frame update
    void Start()
    {
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //AssignPath(bezier);

        }
        MoveAlongPath();
    }
}
