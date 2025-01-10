using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeppelinMove : MonoBehaviour
{
    public float flySpeed = 1.2f;
    public bool moveLeft;
    void Start()
    {
        Destroy(gameObject, 25f);
    }

    void FixedUpdate()
    {
        if (moveLeft)
        {
            transform.position += Vector3.left * flySpeed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.right * flySpeed * Time.deltaTime;
        }
    }
}
