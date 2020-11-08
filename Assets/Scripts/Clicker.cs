using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Clicker : MonoBehaviour
{
    public Image imgGaze;

    public float totalTime = 2;
    private bool gvrStatus;
    private float gvrTimer;

    public int distanceOfRay = 10;
    private RaycastHit _hit;
    private Camera camera;

    public GameObject canvas;

    public UnityEvent unityEvent;

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
        }

        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out _hit, distanceOfRay))
        {
            if (imgGaze.fillAmount == 1)
            {
                //Write actions here
                _hit.collider.gameObject.GetComponent<ClickObject>().unityEvent.Invoke();
            }

            //fixes canvas cursor position
            canvas.transform.localPosition = new Vector3(canvas.transform.localPosition.x,
                canvas.transform.localPosition.y, (_hit.distance + 0.15f));

            if (_hit.distance > -100 && _hit.distance < 100)
            {
                float tmp = _hit.distance * 0.00012308f;
                canvas.transform.localScale = new Vector3(tmp, tmp, tmp);
            }

        }
    }

    public void GVROn()
    {
        gvrStatus = true;
    }
    
    public void GVROff()
    {
        gvrStatus = false;
        gvrTimer = 0;
        imgGaze.fillAmount = 0;
    }
}
