using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public int SCROLLSPEED;
    public float MINCAMERADISTANCE;
    public float MAXCAMERADISTANCE;

    private Camera mainCam;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        float targetDistance = mainCam.orthographicSize - Input.mouseScrollDelta.y * SCROLLSPEED * Time.deltaTime;
        if (targetDistance < MINCAMERADISTANCE) {
            targetDistance = MINCAMERADISTANCE;
        } else if (targetDistance > MAXCAMERADISTANCE) {
            targetDistance = MAXCAMERADISTANCE;
        }
        mainCam.orthographicSize = targetDistance;
    }
}
