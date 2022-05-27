using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltPlayerController : MonoBehaviour
{

    public float gravity, speed, airSpeed, groundSpeed, movementMultiplier, jumpForce, drag;
    private float tempGravity;
    private bool grounded, wallRun, spring;
    private RaycastHit hit, groundHit;
    public LayerMask floor, wall;
    private GlobalData globalData;
    public GameObject blackScreen;
    private Rigidbody rb;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        globalData = GameObject.FindGameObjectWithTag("GlobalData").GetComponent<GlobalData>();
        globalData.blackScreen = blackScreen;
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, out groundHit,  1f, floor);
        
        if (grounded)
        {
            if (groundHit.collider.GetComponent<MovePlatform>() != null)
            {
                transform.parent = groundHit.collider.gameObject.transform;
            }
            if (groundHit.collider.CompareTag("Ramp"))
            {
                speed += (Time.deltaTime) * 2;
            }
            else
            {
                if (groundHit.collider.CompareTag("FinishPlatform"))
                {
                    Debug.Log("FinishPlatform");
                }
                else
                {
                    if (groundHit.collider.CompareTag("Spring"))
                    {
                        spring = true;
                    }
                    else
                    {
                        spring = false;
                    }
                    speed = groundSpeed;
                }
            }
            if (groundHit.collider.gameObject != null)
            {
                if (groundHit.collider.gameObject.transform.rotation.z != transform.rotation.z)
                {
                    //transform.rotation = Quaternion.Lerp(transform.rotation, groundHit.collider.gameObject.transform.rotation, speed * Time.deltaTime);
                    //timeCount = timeCount + Time.deltaTime; 
                }
            }
        }
        if (Input.GetButtonUp("Horizontal") && grounded)
        {
            Vector3 stop = new(0, rb.velocity.y, 0);
            rb.velocity = stop;
        }
        
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position, Vector3.right, Color.red);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, 0.6f, wall))
        {
            tempGravity = 0;
            Vector3 stop = new(0, 0, 0);
            rb.velocity = stop;
            wallRun = true;
            transform.parent = hit.collider.gameObject.transform;
        }
        else 
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, 0.6f, wall))
            {
                tempGravity = 0;
                Vector3 stop = new(0, 0, 0);
                rb.velocity = stop;
                wallRun = true;
                transform.parent = hit.collider.gameObject.transform;
            }
            else
            {
                wallRun = false;
                tempGravity = gravity;
                transform.parent = null;
            }
        }
       
        Movement();
        ControlDrag();
        if (Input.GetButton("Jump") && grounded)
        {
            transform.parent = null;
            Jump();
        }
        else if(Input.GetButton("Jump") && wallRun)
        {
            transform.parent = null;
            WallJump();
        }
    }
    private void WallJump()
    {
        Vector3 jump;
        float tempY = jumpForce * gravity;
        float tempX = jumpForce * gravity;
        speed = airSpeed;
        if (hit.collider.gameObject.transform.position.x > transform.position.x)
        {
            jump = new Vector3(-tempX, tempY, 0);
        }
        else
        {
            jump = new Vector3(tempX, tempY, 0);
        }
        rb.AddForce(jump, ForceMode.Impulse);
    }
    private void Jump()
    {
        float temp;
        if (spring)
        {
            temp = (jumpForce * 2.5f) * gravity;
        }
        else
        {
            temp = jumpForce * gravity;
        }

        speed = airSpeed;
        Vector3 jump = new(0, temp, 0);
        rb.AddForce(jump, ForceMode.Impulse);

    }

    private void Movement()
    {
        if (!wallRun)
        {
            float x = Input.GetAxisRaw("Horizontal");
            Vector3 move = transform.right * x;
            if (!grounded)
            {
                move.y -= tempGravity;
            }
            rb.AddForce(movementMultiplier * speed * move.normalized, ForceMode.Acceleration);
        }
    }

    void ControlDrag()
    {
        rb.drag = drag;
    }
}

