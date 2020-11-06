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
    public bool Go;
    public AudioClip Sad;
    public AudioClip Happy;
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
            }
            GameObject.Find("Canvas/Text").GetComponent<TextMeshProUGUI>().text = "";
            gameObject.GetComponent<AudioSource>().Play();
             StartCoroutine(Printer());
        }
    }

    IEnumerator Printer()
    {
        for (int i=0; i< DialogueText.Length; i++)
        {
            GameObject.Find("Canvas/Text").GetComponent<TextMeshProUGUI>().text += DialogueText[i];
            yield return new WaitForSeconds(0.1f);
        }
    }
}
