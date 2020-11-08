using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VRGaze : MonoBehaviour
{
    public Image imgGaze;

    public float totalTime = 2;
    public float totalTimeExpand = 0.5f;
    private bool gvrStatus;
    private float gvrTimer;

    public int distanceOfRay = 10;
    private RaycastHit _hit;
    private Camera camera;

    public GameObject canvas;
    public Animator animator;
    public Animator transitionAnimator;
    public Animator cameraAnimator;
    public ParticleSystem particleSystem;
    
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gvrStatus)
        {
            gvrTimer += Time.deltaTime;
            imgGaze.fillAmount = gvrTimer / totalTime;
          
            // float size = gvrTimer / totalTimeExpand;
            // if (size <= 1)
            //     imgGaze.transform.localScale = new Vector3(size,size,size);
        }

        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

         if (Physics.Raycast(ray, out _hit, distanceOfRay))
         {
             if (imgGaze.fillAmount == 1)
             {
                 animator.SetBool("IsSelected", true);
                 particleSystem.Play();
                 
                 // if (_hit.transform.CompareTag("Level1"))
                 //     SceneManager.LoadScene(1);
                 // else if (_hit.transform.CompareTag("Level2"))
                 //     SceneManager.LoadScene(2);
                 // else if (_hit.transform.CompareTag("Level3"))
                 //     SceneManager.LoadScene(3);

                 LoadNextScene();
             }
             
             canvas.transform.localPosition = new Vector3(canvas.transform.localPosition.x, canvas.transform.localPosition.y, (_hit.distance + 0.15f));
             // if (_hit.distance > -100 && _hit.distance < 100)
             // canvas.transform.localScale *= _hit.distance / 2.3f;
         }
         
         if (!animator.GetBool("IsShaking"))
             animator.SetBool("IsSelected", false);

    }

    public void GVROn()
    {
        gvrStatus = true;
        animator.SetBool("IsShaking", true);
    }
    
    public void GVROff()
    {
        gvrStatus = false;
        gvrTimer = 0;
        imgGaze.fillAmount = 0;
        animator.SetBool("IsShaking", false);
        
        // imgGaze.transform.localScale = new Vector3(0,0,0);
    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadLevel(1));
    }

    IEnumerator LoadLevel(int level)
    {
        transitionAnimator.SetTrigger("Start");
        cameraAnimator.SetTrigger("Start");
        
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(level);
    }
}
