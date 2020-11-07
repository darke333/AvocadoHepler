using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSandroid : MonoBehaviour
{
    private int FramesPerSec;
    private float frequency = 1.0f;
    private string fps;
    
    void Start()
    {
        Application.targetFrameRate = 240;
        StartCoroutine(FPS());
    }

    private IEnumerator FPS()
    {
        for (; ; )
        {
            // Capture frame-per-second
            int lastFrameCount = Time.frameCount;
            float lastTime = Time.realtimeSinceStartup;
            yield return new WaitForSeconds(frequency);
            float timeSpan = Time.realtimeSinceStartup - lastTime;
            int frameCount = Time.frameCount - lastFrameCount;

            // Display it

            fps = string.Format("FPS: {0}", Mathf.RoundToInt(frameCount / timeSpan));
        }
    }

    void OnGUI()
    {
        GUIStyle g = GUI.skin.GetStyle("label");
        g.fontSize = 30;                                          // размер шрифта
        GUI.Label(new Rect(Screen.width - 300, 5, 200, 60), fps);
    }
}
