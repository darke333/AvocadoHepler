using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class VRHandRecord : MonoBehaviour
{
    /*public AnimationClip clip;

    [SerializeField] SteamVR_Input_Sources Hand;    

    private GameObjectRecorder m_Recorder;

    GameObject HandObj;

    //For Fingers recording
    [SerializeField] Transform handParent;

    public bool Recording;

    void Start()
    {
        if (Hand == SteamVR_Input_Sources.LeftHand || Hand == SteamVR_Input_Sources.RightHand)
        {
            if (Hand == SteamVR_Input_Sources.LeftHand)
            {
                HandObj = Player.instance.leftHand.gameObject;
            }
            else if (Hand == SteamVR_Input_Sources.RightHand)
            {
                HandObj = Player.instance.rightHand.gameObject;
            }
            handParent = transform.GetChild(0);
        }
        else
        {
            handParent = transform;
        }

        

        // Create recorder and record the script GameObject.
        //m_Recorder = new GameObjectRecorder(handParent.gameObject);

        // Bind all the Transforms on the GameObject and all its children.
       // m_Recorder.BindComponentsOfType<Transform>(handParent.gameObject, true);
    }

    void LateUpdate()
    {
        if (clip == null)
            return;

        if (Recording)
        {
            m_Recorder = new GameObjectRecorder(handParent.gameObject);

            // Bind all the Transforms on the GameObject and all its children.
             m_Recorder.BindComponentsOfType<Transform>(handParent.gameObject, true);
            if (HandObj)
            {                
                transform.position = HandObj.transform.position;
                HandObj.transform.parent = handParent;
            }
            else
            {
                //return;
            }
            m_Recorder.TakeSnapshot(Time.deltaTime);

        }
        else if (m_Recorder)
        {
            if (m_Recorder.isRecording)
            {
                // Save the recorded session to the clip.
                m_Recorder.SaveToClip(clip);
            }  

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
        }
    }*/

    public AnimationClip clip;

    private GameObjectRecorder m_Recorder;

    public bool Recording;

    GameObject HandObj;

    [SerializeField] Transform GrabbingObject;

    [SerializeField] GameObject Animator;

    [SerializeField] SteamVR_Input_Sources Hand;


    void Start()
    {
        if (Hand == SteamVR_Input_Sources.LeftHand || Hand == SteamVR_Input_Sources.RightHand)
        {
            if (Hand == SteamVR_Input_Sources.LeftHand)
            {
                HandObj = Player.instance.leftHand.gameObject;
            }
            else if (Hand == SteamVR_Input_Sources.RightHand)
            {
                HandObj = Player.instance.rightHand.gameObject;
            }
        }

        if (HandObj)
        {
            // Create recorder and record the script GameObject.
            m_Recorder = new GameObjectRecorder(Animator);

            // Bind all the Transforms on the GameObject and all its children.
            m_Recorder.BindComponentsOfType<Transform>(Animator, true);
        }
        else
        {
            // Create recorder and record the script GameObject.
            m_Recorder = new GameObjectRecorder(gameObject);

            // Bind all the Transforms on the GameObject and all its children.
            m_Recorder.BindComponentsOfType<Transform>(gameObject, true);
        }


    }

    void LateUpdate()
    {
        if (clip == null)
            return;

        if (Recording)
        {
            if (HandObj && HandObj.transform.parent != Animator.transform)
            {
                transform.parent = HandObj.transform.parent;
                transform.position = HandObj.transform.position;
                transform.rotation = HandObj.transform.parent.rotation;
                transform.localScale = HandObj.transform.localScale;

                HandObj.transform.parent = Animator.transform;
                m_Recorder.BindComponentsOfType<Transform>(Animator, true);

                if (GrabbingObject)
                {
                    GrabbingObject.parent = Animator.transform;
                }
            }


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
