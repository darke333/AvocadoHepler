using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject gameObject;
    
    public void SetColor(Material material)
    {
        gameObject.GetComponent<MeshRenderer>().material = material;
    }
}
