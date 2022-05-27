using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public Vector3[] positions;
    public Vector3 target, start;
    private int index = 0;
    private float speed = 0.25f;
    public bool xOrY;

    private float fraction = 0;

    private void Start()
    {
        Vector3 temp = gameObject.GetComponent<Transform>().position;
        start = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        if (xOrY)
        {
            positions[0] = new Vector3(temp.x - 10, temp.y, temp.z);
            positions[1] = new Vector3(temp.x + 10, temp.y, temp.z);
        }
        else
        {
            positions[0] = new Vector3(temp.x, temp.y - 10, temp.z);
            positions[1] = new Vector3(temp.x, temp.y + 10, temp.z);
        }
        target = positions[0];

    }

    void Update()
    {
        if(fraction < 1)
            {
            fraction += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(start, target, fraction);
        }
        else
        {
            if (index == positions.Length - 1)
            {
                index = 0;
            }
            else
            {
                index++;
            }
            start = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            target = positions[index];
            fraction = 0;
        }
    }
}
