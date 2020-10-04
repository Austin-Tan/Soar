using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    private RectTransform needle;
    private Text reading;
    private PlayerController playerController;

    private const float MIN_ANGLE = 0;
    public float MAX_ANGLE = -270;



    // Start is called before the first frame update
    void Start()
    {
        needle = this.transform.Find("Needle").GetComponent<RectTransform>();
        reading = this.transform.GetComponentInChildren<Text>();
        
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        float speed = playerController.Speed;
        needle.eulerAngles = new Vector3(0, 0, speed * -5);
        reading.text = ((int) speed).ToString();
    }
}
