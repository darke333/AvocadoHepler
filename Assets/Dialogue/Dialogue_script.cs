using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue_script : MonoBehaviour
{
    public string DialogueText;
    public bool SadVoise;
    public bool HappyVoise;
    public bool YesVoise;
    public bool NoVoise;
    public bool Go;
    public AudioClip Sad;
    public AudioClip Happy;
    public AudioClip Yes;
    public AudioClip No;
    int Voise = 0;


    void Update()
    {
        if (SadVoise)
        {
            Voise = 1;
            SadVoise = false;
        }
        if (HappyVoise)
        {
            Voise = 2;
            HappyVoise = false;
        }
        if (NoVoise)
        {
            Voise = 3;
            NoVoise = false;
        }
        if (YesVoise)
        {
            Voise = 4;
            YesVoise = false;
        }
        if (Go)
        {
            Go = false;
            switch (Voise) 
            {
                case 1:
                    gameObject.GetComponent<AudioSource>().clip = Sad;
                    break;
                case 2:
                    gameObject.GetComponent<AudioSource>().clip = Happy;
                    break;
                case 3:
                    gameObject.GetComponent<AudioSource>().clip = No;
                    break;
                case 4:
                    gameObject.GetComponent<AudioSource>().clip = Yes;
                    break;
            }
            GameObject.Find("Text").GetComponent<TextMeshPro>().text = "";
            gameObject.GetComponent<AudioSource>().Play();
             StartCoroutine(Printer());
        }
    }

    IEnumerator Printer()
    {
        for (int i=0; i< DialogueText.Length; i++)
        {
            GameObject.Find("Text").GetComponent<TextMeshPro>().text += DialogueText[i];
            yield return new WaitForSeconds(0.05f);
        }
    }


}
