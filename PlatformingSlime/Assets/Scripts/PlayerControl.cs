using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public LayerMask groundMask;
    private bool grounded;
    public Transform ground;
    private Vector3 movement;
    private float speed = 5f;
    private Rigidbody rb;
    public float gravity;
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        
        movement.x = Input.GetAxisRaw("Horizontal");

        if (Physics.Raycast(ground.position, Vector3.down, 1, groundMask))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.AddForce(new Vector3(0, 10, 0), ForceMode.Impulse);
            Debug.Log("hi");
        }

        Debug.DrawRay(ground.position, Vector3.down, Color.red);
        Debug.Log(Physics.Raycast(ground.position, Vector3.down, 1, groundMask));
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
