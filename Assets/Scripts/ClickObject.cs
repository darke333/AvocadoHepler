using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickObject : MonoBehaviour
{
    public UnityEvent unityEvent;
    Clicker camera;
    private void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Clicker>();
    }

    private void OnDestroy()
    {
        if (transform == camera._hit.transform)
        {
            camera.GVROff();
            
        }
    }

}
