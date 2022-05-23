using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public LayerMask groundMask;
    private bool grounded;
    public Transform ground;
    private Vector2 movement;
    private float speed = 5f;
    private Rigidbody2D rb;
    public float gravity;
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        
        movement.x = Input.GetAxisRaw("Horizontal");
        
        

        if (!grounded)
        {
            if (Physics.Raycast(ground.position, Vector3.down, 1, groundMask))
            {
                grounded = true;
            }
        }
        Debug.Log(Physics.Raycast(ground.position, Vector3.down, 10, groundMask));
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
