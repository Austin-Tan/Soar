using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int ACCELFACTOR;
    private Rigidbody2D rb;
    public float Speed;

    public float MaxSpeed;

    public bool Grounded = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        float vertAccel = ACCELFACTOR * Input.GetAxis("Vertical") * Time.deltaTime;
        float horizAccel = ACCELFACTOR * Input.GetAxis("Horizontal") * Time.deltaTime;

        rb.AddForce(new Vector2(horizAccel, vertAccel));

        if (Input.GetKey(KeyCode.Space)) {
            rb.AddForce(new Vector2(0.5f, -1) * (ACCELFACTOR * Time.deltaTime));
        }
        
        if (Mathf.Abs(rb.velocity.x) > MaxSpeed) {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * MaxSpeed, rb.velocity.y);
        }
        

        this.Speed = GetSpeed(rb.velocity);
    }

    public static float GetSpeed(Vector2 velocity) {
        return Mathf.Abs(velocity.x);
    }
}
