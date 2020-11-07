using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

public class Record : MonoBehaviour
{
    public AnimationClip clip;

    private GameObjectRecorder m_Recorder;

    public bool Recording;

    void Start()
    {
        // Create recorder and record the script GameObject.
        m_Recorder = new GameObjectRecorder(gameObject);

        // Bind all the Transforms on the GameObject and all its children.
        m_Recorder.BindComponentsOfType<Transform>(gameObject, true);
    }

    void LateUpdate()
    {
        if (clip == null)
            return;

        if (Recording)
        {
            m_Recorder.TakeSnapshot(Time.deltaTime);

        }
        else if (m_Recorder.isRecording)
        {
            // Save the recorded session to the clip.
            m_Recorder.SaveToClip(clip);
        }

        // Take a snapshot and record all the bindings values for this frame.
    }

    void OnDisable()
    {
        /*
        if (clip == null)
            return;


        if (m_Recorder.isRecording)
        {
            // Save the recorded session to the clip.
            m_Recorder.SaveToClip(clip);
        }*/
    }
}
